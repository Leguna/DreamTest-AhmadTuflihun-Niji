using System;
using System.Collections;
using Constant;
using DamageModule.Interfaces;
using DG.Tweening;
using EventStruct;
using Facing;
using PauseSystem;
using TurnBasedCombat;
using TurnBasedCombat.SO;
using UnityEngine;
using Utilities;

namespace EnemyTopDown
{
    public partial class AIEnemy : MonoBehaviour, IDamageable<TurnBaseActorSo>, IPauseAble
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private Transform player;
        [SerializeField] private float chaseRange = 5f;

        [SerializeField] private float attackRange = 1f;
        [SerializeField] private float tryAttackRange = 1.5f;
        [SerializeField] private float attackDelay = .5f;
        [SerializeField] private float attackCooldown = 2f;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Transform faceObject;

        public TurnBaseActorSo enemySo;

        private Vector3 _startPosition;
        private EnemyState _currentState;
        private FacingDirection _facingDirection;

        private Coroutine _attackCoroutine;
        public Action onDeath;

        public void Init(TurnBaseActorSo enemySo, Vector2 enemySpawnPoint)
        {
            transform.position = enemySpawnPoint;
            _startPosition = enemySpawnPoint;
            this.enemySo = enemySo;
            _currentState = EnemyState.Idle;
            if (!spriteRenderer) TryGetComponent(out spriteRenderer);
            player = GameConst.playerObject.transform;
        }

        private void FixedUpdate()
        {
            DetermineState(transform, player);

            switch (_currentState)
            {
                case EnemyState.Idle:
                    break;
                case EnemyState.Roam:
                    ReturnToStart();
                    break;
                case EnemyState.Chase:
                    ChasePlayer();
                    break;
                case EnemyState.CanAttack:
                    _currentState = EnemyState.TryAttack;
                    Attacking();
                    break;
                case EnemyState.TryAttack:
                case EnemyState.Stun:
                case EnemyState.Paused:
                    break;
            }
        }

        private void Attacking()
        {
            _currentState = EnemyState.TryAttack;
            _attackCoroutine = StartCoroutine(AttackSequence());
        }

        IEnumerator AttackSequence()
        {
            rb.linearVelocity = Vector2.zero;
            UpdateFacingDirection(player.position - transform.position);
            // ReSharper disable Unity.InefficientPropertyAccess
            Vector3 targetAttackPosition = player.position;
            yield return new WaitForSeconds(attackDelay);
            rb.DOMove(targetAttackPosition, attackDelay).onComplete = () => { spriteRenderer.color = Color.red; };
            spriteRenderer.color = new Color(1, 0.5f, 0);
            yield return new WaitForSeconds(attackCooldown);
            ResetAttack();
        }

        private void ResetAttack()
        {
            StopCoroutine(_attackCoroutine);
            DOTween.Kill(spriteRenderer);
            DOTween.Kill(transform);
            DOTween.Kill(rb);
            _currentState = EnemyState.Idle;
            spriteRenderer.color = Color.red;
            _currentState = EnemyState.Idle;
        }

        private void DetermineState(Transform currentTransform, Transform targetTransform)
        {
            var cannotDetectPlayer = _currentState is EnemyState.Stun or EnemyState.Paused or EnemyState.CanAttack
                or EnemyState.TryAttack;
            if (cannotDetectPlayer) return;
            var distanceToPlayer = Vector2.Distance(currentTransform.position, targetTransform.position);

            if (distanceToPlayer <= tryAttackRange)
            {
                _currentState = EnemyState.CanAttack;
                return;
            }

            if (distanceToPlayer <= chaseRange)
            {
                _currentState = EnemyState.Chase;
                return;
            }

            _currentState = EnemyState.Roam;
        }

        void ChasePlayer()
        {
            var currentPosition = transform.position;
            var playerPosition = player.position;
            UpdateFacingDirection(playerPosition - currentPosition);
            transform.position = Vector2.MoveTowards(currentPosition, playerPosition, moveSpeed * Time.deltaTime);
        }

        void ReturnToStart()
        {
            transform.position = Vector2.MoveTowards(transform.position, _startPosition, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, _startPosition) < 0.1f)
            {
                _currentState = EnemyState.Idle;
            }
        }

        private void UpdateFacingDirection(Vector2 direction)
        {
            _facingDirection = direction.ToFacingDirection();
            if (faceObject)
                faceObject.rotation = _facingDirection.ToRotation();
        }

        void OnDrawGizmosSelected()
        {
            var position = transform.position;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(position, chaseRange);
            Gizmos.color = new Color(1, 0.5f, 0);
            Gizmos.DrawWireSphere(position, tryAttackRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position, attackRange);
        }

        public void TryTakeDamage(TurnBaseActorSo attacker, Transform attackerTransform)
        {
            ResetAttack();
            if (_currentState == EnemyState.Stun) return;

            var attackerDirection = attackerTransform.position - transform.position;
            var startType = _facingDirection.ToSide(attackerDirection).GetStartTypeBySide();
            EventManager.TriggerEvent(new StartTurnBasedGameEventData(startType, enemySo));
        }

        private void Stun()
        {
            _currentState = EnemyState.Stun;
            spriteRenderer.DOKill();
            transform.DOKill();
            spriteRenderer.color = Color.red;

            Sequence damageSequence = DOTween.Sequence();
            damageSequence.Append(spriteRenderer.DOFade(0, 0.1f))
                .Append(spriteRenderer.DOFade(1, 0.1f))
                .Append(spriteRenderer.DOFade(0, 0.1f))
                .Append(spriteRenderer.DOFade(1, 0.1f))
                .AppendInterval(3)
                .OnComplete(() => { _currentState = EnemyState.Idle; });

            damageSequence.Play();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(GameConst.PlayerObjectName) && _currentState == EnemyState.TryAttack)
            {
                other.gameObject.GetComponent<IDamageable<TurnBaseActorSo>>()
                    ?.TryTakeDamage(enemySo, transform);
            }
        }

        public void Pause()
        {
            DOTween.KillAll();
            StopAllCoroutines();
            _currentState = EnemyState.Paused;
        }

        public void Resume() => _currentState = EnemyState.Idle;

        private void OnStartBattle(StartTurnBasedGameEventData data) => Pause();

        private void OnFinish(FinishTurnBasedGameEventData eventData)
        {
            switch (eventData.finishType)
            {
                case FinishType.Win:
                    onDeath?.Invoke();
                    break;
                case FinishType.Flee:
                    Stun();
                    break;
            }
        }

        private void OnPauseResume(PauseResumeEventData data)
        {
            if (data.isPause) Pause();
            else Resume();
        }

        private void OnEnable()
        {
            EventManager.AddEventListener<FinishTurnBasedGameEventData>(OnFinish);
            EventManager.AddEventListener<StartTurnBasedGameEventData>(OnStartBattle);
            EventManager.AddEventListener<PauseResumeEventData>(OnPauseResume);
        }

        private void OnDisable()
        {
            EventManager.RemoveEventListener<StartTurnBasedGameEventData>(OnStartBattle);
            EventManager.RemoveEventListener<FinishTurnBasedGameEventData>(OnFinish);
            EventManager.RemoveEventListener<PauseResumeEventData>(OnPauseResume);
            DOTween.KillAll();
        }
    }
}
using System.Collections;
using Constant;
using DamageModule.Interfaces;
using EventStruct;
using Facing;
using TurnBasedCombat;
using UnityEngine;
using Utilities;

namespace TopDownPlayer
{
    public class TopDownPlayerCombat : MonoBehaviour, IDamageable<TurnBaseActorSo>
    {
        [SerializeField] private TurnBaseActorSo playerSo;
        [SerializeField] private SpriteRenderer slashSpriteRenderer;
        [SerializeField] private Animator animator;

        private FacingDirection _facingDirection;
        private static readonly int AttackAnimTrigger = Animator.StringToHash("Attack");

        private bool _isAttacking;
        private float _attackCooldown;
        private GameState _gameState;
        
        public void TryTakeDamage(TurnBaseActorSo attacker, Transform attackerTransform)
        {
            EventManager.TriggerEvent(new StartTurnBasedGameEventData(CombatStartType.Ambush, attacker));
        }

        private void OnFinish(FinishType obj)
        {
        }

        public void Attack(FacingDirection facingDirection)
        {
            if (_isAttacking || _gameState != GameState.Combat) return;
            StartCoroutine(AttackRoutine(facingDirection));
        }

        private IEnumerator AttackRoutine(FacingDirection facingDirection)
        {
            _isAttacking = true;
            UpdateAnimation(facingDirection);

            Vector2 attackPosition = (Vector2)transform.position + facingDirection.ToUnityVector2();
            Collider2D[] overlapBox = Physics2D.OverlapBoxAll(attackPosition, Vector2.one, 0);

            foreach (var collider2d in overlapBox)
            {
                if (collider2d.CompareTag(GameConst.PlayerObjectName)) continue;
                if (collider2d.TryGetComponent<IDamageable<TurnBaseActorSo>>(out var damageable))
                {
                    damageable.TryTakeDamage(playerSo, transform);
                    break;
                }
            }

            yield return new WaitForSeconds(_attackCooldown);
            _isAttacking = false;
        }


        private void UpdateAnimation(FacingDirection facingDirection)
        {
            animator.SetTrigger(AttackAnimTrigger);
            slashSpriteRenderer.enabled = true;
            slashSpriteRenderer.transform.rotation = facingDirection.ToRotation(90);
        }

        public void UpdateFacingDirection(FacingDirection facingDirection) => _facingDirection = facingDirection;
        private void OnGameStateChange(StateGameChanges data) => _gameState = data.gameState;

        private void OnEnable()
        {
            EventManager.AddEventListener<FinishType>(OnFinish);
            EventManager.AddEventListener<StateGameChanges>(OnGameStateChange);
        }

        private void OnDisable()
        {
            EventManager.RemoveEventListener<FinishType>(OnFinish);
            EventManager.RemoveEventListener<StateGameChanges>(OnGameStateChange);
        }

        public void Init(GameState gameState)
        {
            _gameState = gameState;
        }
    }
}
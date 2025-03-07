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

        public void TryTakeDamage(int damage, GameObject attacker, TurnBaseActorSo attackerSo)
        {
            var attackerDirection = attacker.transform.position - transform.position;
            var attackerSide = _facingDirection.GetSide(attackerDirection);
            Debug.Log($"Player took {damage} damage from{attackerSo.name} on {attackerSide}");
            EventManager.TriggerEvent(new StartTurnBasedGameEventData(CombatStartType.Ambush, attackerSo, playerSo));
        }

        public void Attack(FacingDirection facingDirection)
        {
            if (_isAttacking) return;
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
                    damageable.TryTakeDamage(playerSo.damage, gameObject, playerSo);
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

        public void UpdateFacingDirection(FacingDirection facingDirection)
        {
            _facingDirection = facingDirection;
        }
    }
}
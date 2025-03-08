using System;
using System.Collections.Generic;
using DG.Tweening;
using TurnBasedCombat.SO;
using TurnBasedCombat.UI;
using Random = UnityEngine.Random;

namespace TurnBasedCombat
{
    public class TbPlayerController : TbCharacterController
    {
        private TurnBasedActionViewController _actionViewController;
        public Action onPlayerDeath;
        private List<TbEnemyController> _enemyControllerList;

        public void Init(TurnBaseActorSo actor, TurnBasedActionViewController actionViewController,
            List<TbEnemyController> enemyControllerList)
        {
            base.Init(actor);
            _actionViewController = actionViewController;
            _actionViewController.onButtonClicked += OnActionSelected;
            _enemyControllerList = enemyControllerList;
        }

        public void Attack(TbEnemyController target)
        {
            if (target != null && !target.IsDead)
            {
                target.TakeDamage(actorData.damage);
            }
        }

        public void Spell()
        {
        }

        public void Defend()
        {
        }

        public void Flee()
        {
        }

        private void OnActionSelected(TurnBasedActionType obj)
        {
            switch (obj)
            {
                case TurnBasedActionType.Attack:
                    if (_enemyControllerList.Count > 0)
                    {
                        var randomIndex = Random.Range(0, _enemyControllerList.Count);
                        var enemyController = _enemyControllerList[randomIndex];
                        Attack(enemyController);
                    }
                    break;
                case TurnBasedActionType.Spell:
                    Spell();
                    break;
                case TurnBasedActionType.Defend:
                    Defend();
                    break;
                case TurnBasedActionType.Run:
                    Flee();
                    break;
            }

            isActionSelected = true;
            _actionViewController.Hide();
        }

        public void TakeDamage(int actorDataDamage)
        {
            DOTween.Kill(transform);
            DOTween.Sequence()
                .Append(transform.DOShakePosition(0.5f, 0.5f, 10, 90, false, true))
                .AppendCallback(() => { })
                .Play();
            CurrentHealth -= actorDataDamage;
            actorData.health = CurrentHealth;
            healthBar.UpdateBar(CurrentHealth);
            if (IsDead)
            {
                onPlayerDeath?.Invoke();
            }
        }
    }
}
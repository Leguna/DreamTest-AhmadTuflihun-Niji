﻿using System.Collections.Generic;
using DG.Tweening;
using TurnBasedCombat.SO;
using UnityEngine;

namespace TurnBasedCombat
{
    public class TbEnemyController : TbCharacterController
    {
        public List<TbPlayerController> playerControllerList;

        public void Init(TurnBaseActorSo actor, List<TbPlayerController> playerControllerList)
        {
            base.Init(actor);
            this.playerControllerList = playerControllerList;
        }

        public void PerformAction()
        {
            DOTween.KillAll();

            if (playerControllerList.Count == 0) return;

            transform.position = new Vector3(0, 0, 0);
            DOTween.Sequence()
                .Append(transform.DOMoveX(0.5f, 0.5f))
                .Append(transform.DOMoveX(0, 0.5f))
                .onComplete += AttackRandomPlayer;
        }

        private void AttackRandomPlayer()
        {
            var randomIndex = Random.Range(0, playerControllerList.Count);
            var playerController = playerControllerList[randomIndex];
            playerController.TakeDamage(actorData.damage);
        }

        public void TakeDamage(int actorDataDamage)
        {
            CurrentHealth -= actorDataDamage;
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
            }

            healthBar.UpdateBar(CurrentHealth);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using EventStruct;
using TurnBasedCombat.SO;
using TurnBasedCombat.UI;
using UnityEngine;
using Utilities;

namespace TurnBasedCombat
{
    public class TurnBasedCombatController : SingletonMonoBehaviour<TurnBasedCombatController>
    {
        [SerializeField] private TurnBasedActionViewController actionViewController;
        [SerializeField] private TbQueueViewController queueViewController;

        [SerializeField] private TbPlayerController playerControllerPrefab;
        [SerializeField] private TbEnemyController enemyControllerPrefab;
        [SerializeField] private Transform[] enemyPosition;
        [SerializeField] private Transform playerPosition;

        [SerializeField] private GameObject battleScene;
        [SerializeField] private TurnBaseActorSo playerData;

        private readonly List<TurnBaseActorSo> _allCharacterData = new();
        private readonly Queue<TbCharacterController> _turnQueue = new();

        public void Init()
        {
            playerData.health = playerData.maxHealth;
            actionViewController.Init();
            queueViewController.Init();
            HideBattleScene();
        }

        private void StartCombat(StartTurnBasedGameEventData eventData)
        {
            Debug.Log("Combat started: " + eventData);

            actionViewController.Hide();
            queueViewController.Hide();

            var allCharacterController = new List<TbCharacterController>();

            switch (eventData.startType)
            {
                case CombatStartType.Advantage:
                    playerData.speedMultiplier = playerData.speed * 1.5f;
                    eventData.attackerData.health = eventData.attackerData.maxHealth - playerData.damage;
                    break;
                case CombatStartType.Ambush:
                    playerData.speedMultiplier = playerData.speed * 0.5f;
                    playerData.health -= eventData.attackerData.damage;
                    break;
                case CombatStartType.Neutral:
                    playerData.speedMultiplier = playerData.speed;
                    break;
            }

            var playerController = Instantiate(playerControllerPrefab, playerPosition);

            eventData.attackerData.speedMultiplier = eventData.attackerData.speed;
            eventData.attackerData.health = eventData.attackerData.maxHealth - playerData.damage;

            var allEnemyController = new List<TbEnemyController>();
            var random = Random.Range(1, 3);
            for (var i = 0;
                 i < random;
                 i++)
            {
                _allCharacterData.Add(eventData.attackerData);
                var enemyController = Instantiate(enemyControllerPrefab, enemyPosition[i]);
                enemyController.Init(eventData.attackerData, new List<TbPlayerController> { playerController });
                allCharacterController.Add(enemyController);
                allEnemyController.Add(enemyController);
            }

            playerController.Init(playerData, actionViewController, allEnemyController);
            playerController.onPlayerDeath += PlayerDeath;
            allCharacterController.Add(playerController);

            // Sort all character by speed
            allCharacterController.Sort((a, b) => b.actorData.speedMultiplier.CompareTo(a.actorData.speedMultiplier));

            // Set all character to queue
            _turnQueue.Clear();
            foreach (var character in allCharacterController)
            {
                _turnQueue.Enqueue(character);
            }

            ShowBattleScene();

            StartCoroutine(TurnLoop());
        }

        private void PlayerDeath()
        {
            EventManager.TriggerEvent(new GameOverEventData(onTryAgain: () =>
            {
                playerData.health = playerData.maxHealth;
            }));
        }

        public IEnumerator TurnLoop()
        {
            while (_turnQueue.Count > 0)
            {
                var actor = _turnQueue.Dequeue();

                if (actor is TbPlayerController player)
                {
                    player.isActionSelected = false;
                    actionViewController.Show();
                    yield return new WaitUntil(() => player.isActionSelected);
                    actionViewController.Hide();
                }
                else
                {
                    yield return new WaitForSeconds(1f);
                    (actor as TbEnemyController)?.PerformAction();
                }

                if (!actor.IsDead)
                {
                    _turnQueue.Enqueue(actor);
                }
            }
        }


        private void OnActionSelected(TurnBasedActionType obj)
        {
            Debug.Log("Action selected: " + obj);
            if (obj == TurnBasedActionType.Run)
            {
                EndCombatFlee();
            }
        }

        private void EndCombatFlee()
        {
            battleScene.SetActive(false);
            EventManager.TriggerEvent(new FinishTurnBasedGameEventData(FinishType.Flee));
            EventManager.TriggerEvent(new StateGameChanges(GameState.Combat));
        }


        private void ShowBattleScene()
        {
            actionViewController.Show();
            queueViewController.Show();
            battleScene.SetActive(true);
        }

        private void HideBattleScene()
        {
            battleScene.SetActive(false);
            actionViewController.Hide();
        }

        private void OnEnable()
        {
            EventManager.AddEventListener<StartTurnBasedGameEventData>(StartCombat);
        }

        private void OnDisable()
        {
            EventManager.RemoveEventListener<StartTurnBasedGameEventData>(StartCombat);
        }
    }
}
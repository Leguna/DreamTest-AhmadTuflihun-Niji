using System.Collections.Generic;
using EventStruct;
using UnityEngine;
using Utilities;

namespace TurnBasedCombat
{
    public class TurnBasedCombatController: MonoBehaviour
    {
        public Queue<TurnBaseActorSo> turnQueue;
        public void StartCombat(StartTurnBasedGameEventData eventData)
        {
            Debug.Log("Combat started: " + eventData);
        }

        public void EndCombat()
        {
            Debug.Log("Combat ended");
        }
        
        public void CheckWinCondition()
        {
        }
        
        public void EndTurn()
        {
        }

        public void ShowBattleScene()
        {
            
        }

        private void OnEnable()
        {
            EventManager.AddEventListener<StartTurnBasedGameEventData>(StartCombat);
        }

        private void OnDisable()
        {
            EventManager.RemoveEventListener<StartTurnBasedGameEventData>(StartCombat);
        }

        public void Init()
        {
        }
    }
}
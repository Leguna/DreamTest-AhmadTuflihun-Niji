using EventStruct;
using UnityEngine;
using Utilities;

namespace TopDownMap.Interactable
{
    public class ChangeMapInteractableDoor : InteractableDoorController
    {
         [SerializeField] private GameState gameState;
        public override void Interact()
        {
            base.Interact();
            EventManager.TriggerEvent(new StateGameChanges(gameState));
        }
    }
}
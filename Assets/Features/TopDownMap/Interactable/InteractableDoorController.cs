using Constant;
using Core.Interactable;
using EventStruct;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace TopDownMap.Interactable
{
    public class InteractableDoorController : InteractableBase
    {
        [SerializeField] private SpriteRenderer doorSprite;

        public UnityEvent<SceneIndexEnum> onInteractCallback;

        [SerializeField] private SceneIndexEnum sceneToLoad;

        private void Awake()
        {
            Init(onInteractCallback);
        }

        private void Init(UnityEvent<SceneIndexEnum> onInteractCallback = null)
        {
            this.onInteractCallback = onInteractCallback;
            if (doorSprite == null) TryGetComponent(out doorSprite);
        }

        public override void Interact()
        {
            Debug.Log("Interacting with door");
            EventManager.TriggerEvent(new LoadSceneEventData(sceneToLoad, true));
            onInteractCallback?.Invoke(sceneToLoad);
        }

        public override void ShowPrompt()
        {
            if (doorSprite == null) return;
            doorSprite.color = Color.yellow;
        }

        public override void HidePrompt()
        {
            if (doorSprite == null) return;
            doorSprite.color = new Color(0.5f, 0.25f, 0);
        }
    }
}
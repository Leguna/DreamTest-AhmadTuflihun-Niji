using System;
using Core.Interactable;
using UnityEngine;

namespace NPC.Interactable
{
    public class InteractableNpcController : InteractableBase
    {
        [SerializeField] private SpriteRenderer npcSprite;
        [SerializeField] private SpriteRenderer promptBubble;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            if (promptBubble != null) promptBubble.gameObject.SetActive(false);
        }

        public override void Interact()
        {
            Debug.Log("Interacting with NPC: " + name);
        }

        public override void ShowPrompt()
        {
            if (promptBubble != null) promptBubble.gameObject.SetActive(true);
        }

        public override void HidePrompt()
        {
            if (promptBubble != null)
                promptBubble.gameObject.SetActive(false);
        }
        
    }
}
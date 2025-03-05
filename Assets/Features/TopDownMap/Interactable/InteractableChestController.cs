using Core.Interactable;
using DG.Tweening;
using EventStruct;
using Features.Core.Item;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace TopDownMap.Interactable
{
    public class InteractableChestController : InteractableBase
    {
        [SerializeField] private SpriteRenderer borderSprite;
        [SerializeField] private GameObject chestRender;
        [SerializeField] private SpriteRenderer itemSprite;
        [SerializeField] private GameObject uiCanvas;
        [SerializeField] private Collider2D interactableCollider;
        [SerializeField] private Image chestOpenImage;

        [SerializeField] private BaseItemSo item;
        private bool ChestOpened { get; set; }
        private bool ChestClaimed { get; set; }

        private void Awake()
        {
            uiCanvas.SetActive(false);
            itemSprite.sprite = item.baseItem.icon;
            chestOpenImage.sprite = item.baseItem.icon;
        }

        public override void Interact()
        {
            if (ChestClaimed)
            {
                Debug.Log("Chest already claimed");
                return;
            }

            if (ChestOpened) ClaimChest();
            else OpenChest();
        }

        private void OpenChest()
        {
            Debug.Log("Open chest");
            ChestOpened = true;
            uiCanvas.SetActive(true);
        }

        private void ClaimChest()
        {
            ChestOpened = false;
            ChestClaimed = true;
            uiCanvas.gameObject.SetActive(false);
            chestRender.gameObject.SetActive(false);
            itemSprite.gameObject.SetActive(true);
            borderSprite.enabled = false;
            interactableCollider.enabled = false;
            EventManager.TriggerEvent(new ItemGetNotificationEventData(item.baseItem));
            Debug.Log("Chest claimed");
            itemSprite.DOFade(0, 5).SetDelay(5).OnComplete(() => { Destroy(gameObject); });
        }

        public override void ShowPrompt()
        {
            if (ChestClaimed) return;
            borderSprite.enabled = true;
        }

        public override void HidePrompt()
        {
            if (ChestClaimed) return;
            borderSprite.enabled = false;
            uiCanvas.SetActive(false);
            ChestOpened = false;
        }
    }
}
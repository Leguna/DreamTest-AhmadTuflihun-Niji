using System;
using Constant;
using Features.Core.Item;
using Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.InventoryModule
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private BaseItem baseItem;

        [SerializeField] private TMP_Text itemName;
        [SerializeField] private Image itemImage;
        [SerializeField] private Button itemButton;

        [SerializeField] private GameObject claimedPanel;
        private bool _isClaimed;
        private bool _isClickable;

        public bool IsClaimed
        {
            set
            {
                _isClaimed = value;
                claimedPanel.SetActive(_isClaimed);
                if (_isClaimed) itemName.text = StringLocalization.Claimed;
                IsClickable = !_isClaimed;
            }
        }

        public bool IsClickable
        {
            set
            {
                _isClickable = value;
                itemButton.interactable = _isClickable;
            }
        }

        private Action OnClickAction { get; set; }

        public void Init(BaseItem newBaseItem)
        {
            baseItem = newBaseItem;
            itemName.text = baseItem?.itemName;
            itemImage.sprite = Resources.Load<Sprite>(baseItem?.IconPath());
            itemButton.onClick.RemoveAllListeners();
            itemButton.onClick.AddListener(OnClick);
            IsClaimed = false;
        }

        public void SetListener(Action onClickAction) => OnClickAction = onClickAction;

        private void OnClick()
        {
            if (_isClaimed) return;
            OnClickAction?.Invoke();
        }
    }
}
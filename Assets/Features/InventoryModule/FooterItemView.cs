using Features.Core.Item;
using Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryModule
{
    public class FooterItemView : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text quantity;

        private BaseItem _item;

        public void Init(BaseItem item)
        {
            _item = item;
            _item.onUpdateAction += UpdateItem;
            UpdateItem(item);
        }

        private void UpdateItem(BaseItem item)
        {
            icon.sprite = Resources.Load<Sprite>(item.IconPath());
            quantity.text = item.Quantity.ToString();
        }
    }
}
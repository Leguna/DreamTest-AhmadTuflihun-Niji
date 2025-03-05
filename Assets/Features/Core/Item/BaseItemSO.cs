using System;
using Features.Utilities;
using Item;
using UnityEngine;

namespace Features.Core.Item
{
    [CreateAssetMenu(fileName = "BaseItem", menuName = "Item/BaseItem", order = 0)]
    [Serializable]
    public class BaseItemSo : BaseSo
    {
        public BaseItem baseItem = new();
    }
}
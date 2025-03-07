using System;
using Features.Utilities;
using UnityEngine;
using Utilities;

namespace Item
{
    [CreateAssetMenu(fileName = "BaseItem", menuName = "Item/BaseItem", order = 0)]
    [Serializable]
    public class BaseItemSo : BaseSo
    {
        public BaseItem baseItem = new();
    }
}
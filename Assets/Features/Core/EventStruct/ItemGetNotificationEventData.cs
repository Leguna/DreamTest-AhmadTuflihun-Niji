using Item;
using UnityEngine;

namespace EventStruct
{
    public struct ItemGetNotificationEventData
    {
        public BaseItem item;

        public ItemGetNotificationEventData(BaseItem item)
        {
            this.item = item;
        }
    }
}
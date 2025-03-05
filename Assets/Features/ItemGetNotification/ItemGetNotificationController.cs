using EventStruct;
using UnityEngine;
using Utilities;

namespace ItemGetNotification
{
    public class ItemGetNotificationController : MonoBehaviour
    {
        [SerializeField] private ItemGetNotificationView itemGetNotificationPrefab;
        [SerializeField] private Transform notificationParent;

        private void OnItemGetNotification(ItemGetNotificationEventData eventData)
        {
            var itemGetNotification = Instantiate(itemGetNotificationPrefab, notificationParent);
            itemGetNotification.Init(eventData);
        }

        private void OnEnable()
        {
            EventManager.AddEventListener<ItemGetNotificationEventData>(OnItemGetNotification);
        }

        private void OnDisable()
        {
            EventManager.RemoveEventListener<ItemGetNotificationEventData>(OnItemGetNotification);
        }
    }
}
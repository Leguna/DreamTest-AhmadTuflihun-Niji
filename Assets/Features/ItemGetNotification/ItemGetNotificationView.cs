using DG.Tweening;
using EventStruct;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ItemGetNotification
{
    public class ItemGetNotificationView : MonoBehaviour
    {
        [SerializeField] private Image itemIcon;
        [SerializeField] private TMP_Text itemNameText;
        [SerializeField] private float moveDuration = 3;
        [SerializeField] private float delayDuration = 2;
        [SerializeField] private float moveDirection = -300;

        public void Init(ItemGetNotificationEventData eventData)
        {
            itemIcon.sprite = eventData.item.icon;
            itemNameText.text = eventData.item.itemName;
            transform.DOLocalMoveX(moveDirection, moveDuration)
                .SetEase(Ease.OutQuad)
                .SetAutoKill(true)
                .SetDelay(delayDuration)
                .SetAutoKill(true).OnComplete(
                    () => { Destroy(gameObject, 10); });
        }
    }
}
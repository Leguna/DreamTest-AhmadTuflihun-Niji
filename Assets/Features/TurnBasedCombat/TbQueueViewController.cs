using System.Collections.Generic;
using TurnBasedCombat.SO;
using TurnBasedCombat.UI;
using UnityEngine;

namespace TurnBasedCombat
{
    public class TbQueueViewController : MonoBehaviour
    {
        public Transform queueItemParent;
        public List<TbQueueItemView> queueItemViews;
        public Queue<TurnBaseActorSo> queue = new();

        [SerializeField] private TbQueueItemView queueItemPrefab;

        public void Init()
        {
            queueItemViews = new List<TbQueueItemView>();
            foreach (var actor in queue)
            {
                var queueItem = Instantiate(queueItemPrefab, queueItemParent);
                queueItem.Init(actor);
                queueItemViews.Add(queueItem);
            }

            Hide();
        }

        public void Show()
        {
            queueItemParent.gameObject.SetActive(true);
        }

        public void Hide()
        {
            queueItemParent.gameObject.SetActive(false);
        }

        public void SetQueue(Queue<TbCharacterController> allCharacters)
        {
            queue.Clear();
            foreach (var character in allCharacters)
            {
                queue.Enqueue(character.actorData);
            }

            Init();
        }
    }
}
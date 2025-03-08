using TurnBasedCombat.SO;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedCombat.UI
{
    public class TbQueueItemView : MonoBehaviour
    {
        public Image icon;

        public void Init(TurnBaseActorSo actor)
        {
            icon.sprite = actor.iconQueue;
        }
    }
}
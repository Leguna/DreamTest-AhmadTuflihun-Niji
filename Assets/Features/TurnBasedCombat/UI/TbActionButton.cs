using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TurnBasedCombat.UI
{
    public class TurnBasedActionButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        [SerializeField] private Image selectedImage;

        public TurnBasedActionType actionType;
        public Action<TurnBasedActionType> onButtonClicked;
        public Action<TurnBasedActionType> onButtonHovered;
        public Action<TurnBasedActionType> onButtonDeHovered;

        internal void SelectButton()
        {
            selectedImage.enabled = true;
        }

        internal void DeSelectButton()
        {
            selectedImage.enabled = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onButtonHovered?.Invoke(actionType);
            SelectButton();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onButtonClicked?.Invoke(actionType);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onButtonDeHovered?.Invoke(actionType);
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurnBasedActionButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private bool isSelected;
    [SerializeField] private Image selectedImage;
    
    private EventTrigger _eventTrigger;

    public Action onButtonClicked;
    public Action onButtonHovered;

    private void OnButtonClicked()
    {
    }

    private void DeSelectButton()
    {
        isSelected = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onButtonHovered?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onButtonClicked?.Invoke();
    }
}
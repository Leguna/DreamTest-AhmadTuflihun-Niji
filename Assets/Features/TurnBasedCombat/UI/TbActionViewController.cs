using System;
using DG.Tweening;
using UnityEngine;

namespace TurnBasedCombat.UI
{
    public class TurnBasedActionViewController : MonoBehaviour
    {
        [SerializeField] private GameObject buttonsParent;
        [SerializeField] private TurnBasedActionButton[] actionButtons;

        private int _selectedButtonIndex;
        private MainGameInputAction _mainGameInputAction;

        public Action<TurnBasedActionType> onButtonClicked;

        internal void Init(Action<TurnBasedActionType> onButtonClicked = null)
        {
            this.onButtonClicked = onButtonClicked;
            _selectedButtonIndex = 0;
            buttonsParent.SetActive(false);
            actionButtons = buttonsParent.GetComponentsInChildren<TurnBasedActionButton>();
            SetListener();
            if (actionButtons.Length > 0)
                actionButtons[_selectedButtonIndex].SelectButton();
            Hide();
        }

        internal void Show()
        {
            foreach (var actionButton in actionButtons)
                actionButton.DeSelectButton();
            _selectedButtonIndex = 0;
            actionButtons[_selectedButtonIndex].SelectButton();
            _mainGameInputAction.Enable();
            buttonsParent.SetActive(true);
        }

        internal void Hide()
        {
            _mainGameInputAction.Disable();
            buttonsParent.SetActive(false);
        }

        private void SetListener()
        {
            foreach (var actionButton in actionButtons)
            {
                actionButton.onButtonClicked += OnButtonClicked;
                actionButton.onButtonHovered += OnButtonHovered;
                actionButton.onButtonDeHovered += _ => { };
            }
        }


        private void OnButtonHovered(TurnBasedActionType obj)
        {
            foreach (var actionButton in actionButtons)
                if (actionButton.actionType != obj)
                    actionButton.DeSelectButton();
        }

        private void OnButtonClicked(TurnBasedActionType actionType)
        {
            onButtonClicked?.Invoke(actionType);
        }

        private void Interact()
        {
            onButtonClicked?.Invoke(actionButtons[_selectedButtonIndex].actionType);
        }

        private void MoveUI(Vector2 direction)
        {
            if (direction.y < 0 && actionButtons.Length > 0)
            {
                foreach (var actionButton in actionButtons)
                    actionButton.DeSelectButton();
                _selectedButtonIndex = (_selectedButtonIndex + 1) % actionButtons.Length;
                actionButtons[_selectedButtonIndex].SelectButton();
            }
            else if (direction.y > 0 && actionButtons.Length > 0)
            {
                foreach (var actionButton in actionButtons)
                    actionButton.DeSelectButton();
                _selectedButtonIndex = (_selectedButtonIndex - 1 + actionButtons.Length) % actionButtons.Length;
                actionButtons[_selectedButtonIndex].SelectButton();
            }
        }

        private void OnEnable()
        {
            _mainGameInputAction = new MainGameInputAction();
            _mainGameInputAction.UI.Navigate.performed += (ctx) => MoveUI(ctx.ReadValue<Vector2>());
            _mainGameInputAction.UI.Interact.performed += (_) => Interact();
        }
    }
}
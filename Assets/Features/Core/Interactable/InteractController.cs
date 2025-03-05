using System.Collections.Generic;
using Core.Interactable;
using UnityEngine;

namespace Interactable
{
    [RequireComponent(typeof(Collider2D))]
    public class InteractController : MonoBehaviour
    {
        private readonly List<IInteractable> _listInteractable = new();
        public IInteractable currentInteractable;

        [SerializeField] private GameObject interactableHint;


        private MainGameInputAction _inputAction;

        private void Awake()
        {
            Init();
        }

        public void Interact()
        {
            currentInteractable?.Interact();
        }

        private void Init()
        {
            _inputAction = new MainGameInputAction();
            _inputAction.Player.Interact.Enable();
            _inputAction.Player.Interact.performed += _ => Interact();
        }

        private void OnEnable()
        {
            _inputAction?.Player.Interact.Enable();
        }

        private void ChangeCurrentInteractable(IInteractable interactable)
        {
            if (currentInteractable != null) currentInteractable.HidePrompt();
            currentInteractable = interactable;
            currentInteractable.ShowPrompt();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out IInteractable interactable)) return;
            _listInteractable.Add(interactable);
            UpdateCurrentInteractable();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent(out IInteractable interactable)) return;
            _listInteractable.Remove(interactable);
            interactable.HidePrompt();
            currentInteractable = null;
            UpdateCurrentInteractable();
        }

        private void UpdateCurrentInteractable()
        {
            if (_listInteractable.Count == 0)
            {
                if (interactableHint != null)
                    interactableHint.SetActive(false);
                currentInteractable?.HidePrompt();
                currentInteractable = null;
            }
            else
            {
                ChangeCurrentInteractable(_listInteractable[0]);
                if (interactableHint != null)
                    interactableHint.SetActive(true);
            }
        }


        private void OnDisable()
        {
            _inputAction?.Player.Interact.Disable();
        }
    }
}
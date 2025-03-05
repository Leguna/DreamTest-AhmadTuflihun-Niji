using UnityEngine;

namespace Core.Interactable
{
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        public bool IsInteractable { get; set; }
        
        public virtual void Interact()
        {
            throw new System.NotImplementedException();
        }

        public virtual void ShowPrompt()
        {
            throw new System.NotImplementedException();
        }

        public virtual void HidePrompt()
        {
            throw new System.NotImplementedException();
        }
    }
}
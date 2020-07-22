using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Interactable controller script class
    /// </summary>
    public class InteractableControllerScript : MonoBehaviour, IInteractableController
    {
        /// <summary>
        /// On interact
        /// </summary>
        [SerializeField]
        private UnityEvent onInteract = default;

        /// <summary>
        /// Interact
        /// </summary>
        public virtual void Interact()
        {
            if (onInteract != null)
            {
                onInteract.Invoke();
            }
        }
    }
}

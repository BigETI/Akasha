using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Character trigger controller script class
    /// </summary>
    public class CharacterTriggerControllerScript : MonoBehaviour, ICharacterTriggerController
    {
        /// <summary>
        /// On character trigger entered
        /// </summary>
        [SerializeField]
        private UnityEvent onCharacterTriggerEntered = default;

        /// <summary>
        /// On character trigger exited
        /// </summary>
        [SerializeField]
        private UnityEvent onCharacterTriggerExited = default;

        /// <summary>
        /// Entered character IDs
        /// </summary>
        private HashSet<int> enteredCharacterIDs = new HashSet<int>();

        /// <summary>
        /// Animator
        /// </summary>
        public Animator Animator { get; private set; }

        /// <summary>
        /// Set animator boolean false
        /// </summary>
        /// <param name="name">Property name</param>
        public void SetAnimatorBooleanFalse(string name)
        {
            if (Animator != null)
            {
                Animator.SetBool(name, false);
            }
        }

        /// <summary>
        /// Set animator boolean true
        /// </summary>
        /// <param name="name">Property name</param>
        public void SetAnimatorBooleanTrue(string name)
        {
            if (Animator != null)
            {
                Animator.SetBool(name, true);
            }
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            Animator = GetComponent<Animator>();
        }

        /// <summary>
        /// On trigger enter
        /// </summary>
        /// <param name="collider">Collider</param>
        private void OnTriggerEnter(Collider collider)
        {
            GameObject game_object = collider.gameObject;
            while (game_object != null)
            {
                if (game_object.GetComponent<OldCharacterControllerScript>() != null)
                {
                    if ((enteredCharacterIDs.Count <= 0) && (onCharacterTriggerEntered != null))
                    {
                        onCharacterTriggerEntered.Invoke();
                    }
                    enteredCharacterIDs.Add(game_object.GetInstanceID());
                    break;
                }
                if (game_object.transform.parent == null)
                {
                    break;
                }
                game_object = game_object.transform.parent.gameObject;
            }
        }

        /// <summary>
        /// On trigger enter
        /// </summary>
        /// <param name="collider">Collider</param>
        private void OnTriggerExit(Collider collider)
        {
            GameObject game_object = collider.gameObject;
            while (game_object != null)
            {
                if (game_object.GetComponent<OldCharacterControllerScript>() != null)
                {
                    if (enteredCharacterIDs.Remove(game_object.GetInstanceID()) && (enteredCharacterIDs.Count <= 0) && (onCharacterTriggerExited != null))
                    {
                        onCharacterTriggerExited.Invoke();
                    }
                    break;
                }
                if (game_object.transform.parent == null)
                {
                    break;
                }
                game_object = game_object.transform.parent.gameObject;
            }
        }
    }
}

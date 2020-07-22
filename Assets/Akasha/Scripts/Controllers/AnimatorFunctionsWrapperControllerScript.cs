using UnityEngine;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Animator functions wrapper controller script
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class AnimatorFunctionsWrapperControllerScript : MonoBehaviour, IAnimatorFunctionsWrapperController
    {
        /// <summary>
        /// Animator
        /// </summary>
        public Animator Animator { get; private set; }

        /// <summary>
        /// Set animator boolean false
        /// </summary>
        /// <param name="id">Parameter name hash</param>
        public void SetAnimatorBooleanFalse(int id)
        {
            if (Animator != null)
            {
                Animator.SetBool(id, false);
            }
        }

        /// <summary>
        /// Set animator boolean false
        /// </summary>
        /// <param name="name">Parameter name</param>
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
        /// <param name="id">Parameter name hash</param>
        public void SetAnimatorBooleanTrue(int id)
        {
            if (Animator != null)
            {
                Animator.SetBool(id, true);
            }
        }

        /// <summary>
        /// Set animator boolean true
        /// </summary>
        /// <param name="name">Parameter name</param>
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
    }
}

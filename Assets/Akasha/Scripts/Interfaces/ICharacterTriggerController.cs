using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Character trigger controller interface
    /// </summary>
    public interface ICharacterTriggerController : IBehaviour
    {
        /// <summary>
        /// Animator
        /// </summary>
        Animator Animator { get; }

        /// <summary>
        /// Set animator boolean false
        /// </summary>
        /// <param name="name">Property name</param>
        void SetAnimatorBooleanFalse(string name);

        /// <summary>
        /// Set animator boolean true
        /// </summary>
        /// <param name="name">Property name</param>
        void SetAnimatorBooleanTrue(string name);
    }
}

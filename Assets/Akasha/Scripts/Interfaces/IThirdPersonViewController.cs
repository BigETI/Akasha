using Akasha.Controllers;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Third person view controller interface
    /// </summary>
    public interface IThirdPersonViewController : IBehaviour
    {
        /// <summary>
        /// Stiffness
        /// </summary>
        float Stiffness { get; set; }

        /// <summary>
        /// Offset
        /// </summary>
        Vector3 Offset { get; set; }

        /// <summary>
        /// Virtual camera offset
        /// </summary>
        Vector3 VirtualCameraOffset { get; set; }

        /// <summary>
        /// Virtual camera radius
        /// </summary>
        float VirtualCameraRadius { get; set; }

        /// <summary>
        /// Virtual camera transform
        /// </summary>
        Transform VirtualCameraTransform { get; set; }

        /// <summary>
        /// Follow character controller
        /// </summary>
        CharacterControllerScript FollowCharacterController { get; set; }
    }
}

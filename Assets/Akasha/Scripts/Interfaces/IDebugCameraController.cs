using Akasha.InputActions;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Debug camera controller interface
    /// </summary>
    public interface IDebugCameraController : IBehaviour
    {
        /// <summary>
        /// Maximal speed
        /// </summary>
        float MaximalSpeed { get; set; }

        /// <summary>
        /// Maximal acceleration
        /// </summary>
        float MaximalAcceleration { get; set; }

        /// <summary>
        /// Sprint multiplicator
        /// </summary>
        float SprintMultiplicator { get; set; }

        /// <summary>
        /// Movement
        /// </summary>
        Vector3 Movement { get; set; }

        /// <summary>
        /// Minimal horizontal rotation
        /// </summary>
        float MinimalHorizontalRotation { get; set; }

        /// <summary>
        /// Maximal horizontal rotation
        /// </summary>
        float MaximalHorizontalRotation { get; set; }

        /// <summary>
        /// FPS sample time
        /// </summary>
        float FPSSampleTime { get; set; }

        /// <summary>
        /// Rotation
        /// </summary>
        Vector2 Rotation { get; set; }

        /// <summary>
        /// Velocity
        /// </summary>
        Vector3 Velocity { get; }

        /// <summary>
        /// Is sprinting
        /// </summary>
        bool IsSprinting { get; }

        /// <summary>
        /// Game input actions
        /// </summary>
        GameInputActions GameInputActions { get; }
    }
}

using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Character controller interface
    /// </summary>
    public interface ICharacterController : IBehaviour
    {
        /// <summary>
        /// Health
        /// </summary>
        float Health { get; set; }

        /// <summary>
        /// Maximal health
        /// </summary>
        float MaximalHealth { get; set; }

        /// <summary>
        /// Minimal horizontal rotation
        /// </summary>
        float MinimalHorizontalRotation { get; set; }

        /// <summary>
        /// Maximal horizontal rotation
        /// </summary>
        float MaximalHorizontalRotation { get; set; }

        /// <summary>
        /// Gravity magnitude
        /// </summary>
        float GravityMagnitude { get; set; }

        /// <summary>
        /// Ground check offset
        /// </summary>
        Vector3 GroundCheckOffset { get; set; }

        /// <summary>
        /// Ground check offset
        /// </summary>
        float GroundCheckRadius { get; set; }

        /// <summary>
        /// Movement speed
        /// </summary>
        float MovementSpeed { get; set; }

        /// <summary>
        /// Jump height
        /// </summary>
        float JumpHeight { get; set; }

        /// <summary>
        /// Eyes transform
        /// </summary>
        Transform EyesTransform { get; set; }

        /// <summary>
        /// Is alive
        /// </summary>
        bool IsAlive { get; }

        /// <summary>
        /// Rotation
        /// </summary>
        Vector2 Rotation { get; set; }

        /// <summary>
        /// Vertical velocity magnitude
        /// </summary>
        float VerticalVelocityMagnitude { get; }

        /// <summary>
        /// Movement
        /// </summary>
        Vector2 Movement { get; set; }

        /// <summary>
        /// Running mode
        /// </summary>
        ERunningMode RunningMode { get; set; }

        /// <summary>
        /// Character controller
        /// </summary>
        CharacterController CharacterController { get; }

        /// <summary>
        /// Place block
        /// </summary>
        void PlaceBlock();

        /// <summary>
        /// Destroy block
        /// </summary>
        void DestroyBlock();

        /// <summary>
        /// Interact
        /// </summary>
        void Interact();

        /// <summary>
        /// Jump
        /// </summary>
        void Jump();

        /// <summary>
        /// Shoot
        /// </summary>
        void Shoot();

        /// <summary>
        /// Reload
        /// </summary>
        void Reload();
    }
}

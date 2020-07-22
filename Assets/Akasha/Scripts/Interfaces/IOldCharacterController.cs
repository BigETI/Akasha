using Akasha.Objects;
using UnityEngine;

/// <summary>
/// Akasha
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Old character controller interface
    /// </summary>
    public interface IOldCharacterController : IDestructibleController
    {
        /// <summary>
        /// Movement speed
        /// </summary>
        float MovementSpeed { get; set; }

        /// <summary>
        /// Rotation stiffness
        /// </summary>
        float RotationStiffness { get; set; }

        /// <summary>
        /// Movement animation curve
        /// </summary>
        AnimationCurve MovementAnimationCurve { get; set; }

        /// <summary>
        /// Movement time
        /// </summary>
        float MovementTime { get; set; }

        /// <summary>
        /// Minimal horizontal rotation
        /// </summary>
        float MinimalHorizontalRotation { get; set; }

        /// <summary>
        /// Maximal horizontal rotation
        /// </summary>
        float MaximalHorizontalRotation { get; set; }

        /// <summary>
        /// Jump height
        /// </summary>
        float JumpHeight { get; set; }

        /// <summary>
        /// Total jumps
        /// </summary>
        uint TotalJumps { get; set; }

        /// <summary>
        /// Fall acceleration multiplier
        /// </summary>
        float FallAccelerationMultiplier { get; set; }

        /// <summary>
        /// Air movement acceleration
        /// </summary>
        float AirMovementAcceleration { get; set; }

        /// <summary>
        /// Air movement brake deceleration
        /// </summary>
        float AirMovementBrakeDeceleration { get; set; }

        /// <summary>
        /// Minimal fall damage
        /// </summary>
        float MinimalFallDamage { get; set; }

        /// <summary>
        /// Maximal fall damage speed
        /// </summary>
        float MaximalFallDamage { get; set; }

        /// <summary>
        /// Minimal fall damage speed
        /// </summary>
        float MinimalFallDamageSpeed { get; set; }

        /// <summary>
        /// Maximal fall damage speed
        /// </summary>
        float MaximalFallDamageSpeed { get; set; }

        /// <summary>
        /// Height
        /// </summary>
        float Height { get; set; }

        /// <summary>
        /// Radius
        /// </summary>
        float Radius { get; set; }

        /// <summary>
        /// Is alive
        /// </summary>
        bool IsAlive { get; }

        /// <summary>
        /// Maximal ground angle
        /// </summary>
        float MaximalGroundAngle { get; set; }

        /// <summary>
        /// Interaction distance
        /// </summary>
        float InteractionDistance { get; set; }

        /// <summary>
        /// Weapon
        /// </summary>
        WeaponObjectScript Weapon { get; set; }

        /// <summary>
        /// Eyes transform
        /// </summary>
        Transform EyesTransform { get; set; }

        /// <summary>
        /// Movement
        /// </summary>
        Vector2 Movement { get; set; }

        /// <summary>
        /// Rotation
        /// </summary>
        Vector2 Rotation { get; set; }

        /// <summary>
        /// Running mode
        /// </summary>
        ERunningMode RunningMode { get; set; }

        /// <summary>
        /// Is on ground
        /// </summary>
        bool IsOnGround { get; }

        /// <summary>
        /// Is in air or jumping
        /// </summary>
        bool IsInAirOrJumping { get; }

        /// <summary>
        /// Ground normal
        /// </summary>
        Vector3 GroundNormal { get; }

        /// <summary>
        /// Executed jumps
        /// </summary>
        uint ExecutedJumps { get; }

        /// <summary>
        /// Shots fired
        /// </summary>
        uint ShotsFired { get; }

        /// <summary>
        /// Is reloading
        /// </summary>
        bool IsReloading { get; }

        /// <summary>
        /// Character rigidbody
        /// </summary>
        Rigidbody CharacterRigidbody { get; }

        /// <summary>
        /// Weapon and hands animator
        /// </summary>
        Animator WeaponAndHandsAnimator { get; }

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

        /// <summary>
        /// Interact
        /// </summary>
        void Interact();
    }
}

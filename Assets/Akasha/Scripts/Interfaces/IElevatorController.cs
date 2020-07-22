using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Elevator controller interface
    /// </summary>
    public interface IElevatorController : IBehaviour
    {
        /// <summary>
        /// Is active
        /// </summary>
        bool IsActive { get; set; }

        /// <summary>
        /// Elevator type
        /// </summary>
        EElevatorType ElevatorType { get; set; }

        /// <summary>
        /// Finish movement when inactive
        /// </summary>
        bool FinishMovementWhenInactive { get; set; }

        /// <summary>
        /// Target position
        /// </summary>
        Vector3 TargetPosition { get; set; }

        /// <summary>
        /// Use world space
        /// </summary>
        bool UseWorldSpace { get; set; }

        /// <summary>
        /// Movement time
        /// </summary>
        float MovementTime { get; set; }

        /// <summary>
        /// Pause time
        /// </summary>
        float PauseTime { get; set; }

        /// <summary>
        /// Start position
        /// </summary>
        Vector3 StartPosition { get; }

        /// <summary>
        /// Elapsed movement time
        /// </summary>
        float ElapsedMovementTime { get; }

        /// <summary>
        /// Elapsed pause time
        /// </summary>
        float ElapsedPauseTime { get; }
    }
}

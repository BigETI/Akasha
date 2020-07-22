using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Player character detection state interface
    /// </summary>
    public interface IPlayerCharacterDetectionState
    {
        /// <summary>
        /// Character controller
        /// </summary>
        ICharacterController CharacterController { get; }

        /// <summary>
        /// Last seen position
        /// </summary>
        Vector3 LastSeenPosition { get; set; }

        /// <summary>
        /// Detection state
        /// </summary>
        EPlayerCharacterDetectionState DetectionState { get; set; }
    }
}

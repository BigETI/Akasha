using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Player character detection state
    /// </summary>
    public class PlayerCharacterDetectionState : IPlayerCharacterDetectionState
    {
        /// <summary>
        /// Character controller
        /// </summary>
        public ICharacterController CharacterController { get; }

        /// <summary>
        /// Last seen position
        /// </summary>
        public Vector3 LastSeenPosition { get; set; }

        /// <summary>
        /// Detection state
        /// </summary>
        public EPlayerCharacterDetectionState DetectionState { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="characterController">Character controller</param>
        /// <param name="lastSeenPosition">Last seen position</param>
        /// <param name="detectionState">Detection state</param>
        public PlayerCharacterDetectionState(ICharacterController characterController, Vector3 lastSeenPosition, EPlayerCharacterDetectionState detectionState)
        {
            CharacterController = characterController;
            LastSeenPosition = lastSeenPosition;
            DetectionState = detectionState;
        }
    }
}

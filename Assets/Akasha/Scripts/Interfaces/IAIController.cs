using System.Collections.Generic;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// AI controller
    /// </summary>
    public interface IAIController : IBehaviour
    {
        /// <summary>
        /// Visibility angle
        /// </summary>
        float VisibilityAngle { get; set; }

        /// <summary>
        /// Visibility distance
        /// </summary>
        float VisibilityDistance { get; set; }

        /// <summary>
        /// Detected player characters
        /// </summary>
        IReadOnlyDictionary<int, IPlayerCharacterDetectionState> DetectedPlayerCharacters { get; }

        /// <summary>
        /// Character controller
        /// </summary>
        ICharacterController CharacterController { get; }
    }
}

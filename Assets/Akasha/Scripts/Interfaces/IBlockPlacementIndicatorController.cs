using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Block placement indicator controller interface
    /// </summary>
    public interface IBlockPlacementIndicatorController
    {
        /// <summary>
        /// Origin game object
        /// </summary>
        GameObject OriginGameObject { get; set; }

        /// <summary>
        /// Player character controller
        /// </summary>
        ICharacterController PlayerCharacterController { get; }
    }
}

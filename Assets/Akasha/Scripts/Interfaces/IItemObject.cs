using UnityEngine;

/// <summary>
/// Akasha
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Item object
    /// </summary>
    public interface IItemObject : IScriptableObject
    {
        /// <summary>
        /// Item name
        /// </summary>
        string ItemName { get; }

        /// <summary>
        /// Description
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Icon sprite
        /// </summary>
        Sprite IconSprite { get; }

        /// <summary>
        /// Weight
        /// </summary>
        uint Weight { get; }

        /// <summary>
        /// Maximal hit cooldown time
        /// </summary>
        float MaximalHitCooldownTime { get; }
    }
}

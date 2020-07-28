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
        /// Maximal health
        /// </summary>
        uint MaximalHealth { get; }

        /// <summary>
        /// Maximal stack size
        /// </summary>
        uint MaximalStackSize { get; }

        /// <summary>
        /// Weight
        /// </summary>
        uint Weight { get; }

        /// <summary>
        /// Maximal hit cooldown time
        /// </summary>
        float MaximalHitCooldownTime { get; }

        /// <summary>
        /// Item and hands asset
        /// </summary>
        GameObject ItemAndHandsAsset { get; }

        /// <summary>
        /// Key
        /// </summary>
        string Key { get; }
    }
}

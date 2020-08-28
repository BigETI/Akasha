using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Entity object interface
    /// </summary>
    public interface IEntityObject : IScriptableObject
    {
        /// <summary>
        /// Asset
        /// </summary>
        GameObject Asset { get; }

        /// <summary>
        /// Key
        /// </summary>
        string Key { get; }
    }
}

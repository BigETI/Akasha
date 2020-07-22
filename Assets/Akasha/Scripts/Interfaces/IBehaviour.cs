using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Behaviour interface
    /// </summary>
    public interface IBehaviour
    {
        /// <summary>
        /// Name
        /// </summary>
        string name { get; set; }

        /// <summary>
        /// Transform
        /// </summary>
        Transform transform { get; }

        /// <summary>
        /// Game object
        /// </summary>
        GameObject gameObject { get; }
    }
}

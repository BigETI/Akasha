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
#pragma warning disable IDE1006 // Naming Styles
        string name { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// Transform
        /// </summary>
#pragma warning disable IDE1006 // Naming Styles
        Transform transform { get; }
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// Game object
        /// </summary>
#pragma warning disable IDE1006 // Naming Styles
        GameObject gameObject { get; }
#pragma warning restore IDE1006 // Naming Styles
    }
}

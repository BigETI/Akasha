using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Transform and parent interface
    /// </summary>
    public interface ITransformAndOldParent
    {
        /// <summary>
        /// Transform
        /// </summary>
        Transform Transform { get; }

        /// <summary>
        /// Old parent transform
        /// </summary>
        Transform OldParent { get; }
    }
}

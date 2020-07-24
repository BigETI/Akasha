using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Block object interface
    /// </summary>
    public interface IBlockObject : IScriptableObject
    {
        /// <summary>
        /// Block material
        /// </summary>
        Material Material { get; }

        /// <summary>
        /// Mesh variants
        /// </summary>
        IBlockMeshVariantsObject MeshVariants { get; }

        /// <summary>
        /// Collision bounds
        /// </summary>
        IReadOnlyList<Bounds> CollisionBounds { get; }
    }
}

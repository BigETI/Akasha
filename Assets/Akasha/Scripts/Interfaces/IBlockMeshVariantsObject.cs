using Akasha.Objects;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Block mesh variants object interface
    /// </summary>
    public interface IBlockMeshVariantsObject : IScriptableObject
    {
        /// <summary>
        /// Block assets
        /// </summary>
        IReadOnlyList<GameObject> BlockAssets { get; }

        /// <summary>
        /// Close to mesh variants
        /// </summary>
        IReadOnlyList<BlockMeshVariantsObjectScript> CloseToMeshVariants { get; }
    }
}

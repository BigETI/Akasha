using Akasha.Data;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AKasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Blocks prefab object interface
    /// </summary>
    public interface IBlocksPrefabObject : IBlockObject
    {
        /// <summary>
        /// Set blocks operation
        /// </summary>
        ESetBlocksOperation SetBlocksOperation { get; }

        /// <summary>
        /// Size
        /// </summary>
        Vector3Int Size { get; }

        /// <summary>
        /// Offset
        /// </summary>
        Vector3Int Offset { get; }

        /// <summary>
        /// Blocks
        /// </summary>
        IReadOnlyList<BlockData> Blocks { get; }

        /// <summary>
        /// Initialize
        /// </summary>
        void Initialize();
    }
}

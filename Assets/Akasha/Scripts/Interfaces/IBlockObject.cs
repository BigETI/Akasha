using Akasha.Data;
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
    public interface IBlockObject : IItemObject
    {
        /// <summary>
        /// Has random orientation
        /// </summary>
        bool HasRandomOrientation { get; }

        /// <summary>
        /// Initial health
        /// </summary>
        ushort InitialHealth { get; }

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

        /// <summary>
        /// Farming tools
        /// </summary>
        IReadOnlyList<FarmingToolData> FarmingTools { get; }

        /// <summary>
        /// Farming tool lookup
        /// </summary>
        IReadOnlyDictionary<string, IFarmingToolData> FarmingToolLookup { get; }

        /// <summary>
        /// Get farming tool data from farming tool
        /// </summary>
        /// <param name="item">Item</param>
        /// <returns>Farming tool data if successful, otherwise "null"</returns>
        IFarmingToolData GetFarmingToolDataFromFarmingToolItem(IItemObject item);
    }
}

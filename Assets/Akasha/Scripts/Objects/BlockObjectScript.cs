using Akasha.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Akasha objects namespace
/// </summary>
namespace Akasha.Objects
{
    /// <summary>
    /// Block object script class
    /// </summary>
    [CreateAssetMenu(fileName = "Block", menuName = "Akasha/Block")]
    public class BlockObjectScript : ItemObjectScript, IBlockObject
    {
        /// <summary>
        /// Has random orientation
        /// </summary>
        [SerializeField]
        private bool hasRandomOrientation = default;

        /// <summary>
        /// Initial health
        /// </summary>
        [SerializeField]
        private ushort initialHealth = 1024;

        /// <summary>
        /// Block material
        /// </summary>
        [SerializeField]
        private Material material = default;

        /// <summary>
        /// Mesh variants
        /// </summary>
        [SerializeField]
        private BlockMeshVariantsObjectScript meshVariants = default;

        /// <summary>
        /// Collision bounds
        /// </summary>
        [SerializeField]
        private Bounds[] collisionBounds = new Bounds[]
        {
            new Bounds(Vector3.zero, Vector3.one)
        };

        /// <summary>
        /// Farming tools
        /// </summary>
        [SerializeField]
        private FarmingToolData[] farmingTools = Array.Empty<FarmingToolData>();

        /// <summary>
        /// Farming tool lookup
        /// </summary>
        private Dictionary<string, IFarmingToolData> farmingToolLookup;

        /// <summary>
        /// Has random orientation
        /// </summary>
        public bool HasRandomOrientation => hasRandomOrientation;

        /// <summary>
        /// Initial health
        /// </summary>
        public ushort InitialHealth => initialHealth;

        /// <summary>
        /// Block material
        /// </summary>
        public Material Material => material;

        /// <summary>
        /// Mesh variants
        /// </summary>
        public IBlockMeshVariantsObject MeshVariants => meshVariants;

        /// <summary>
        /// Collision bounds
        /// </summary>
        public IReadOnlyList<Bounds> CollisionBounds
        {
            get
            {
                if (collisionBounds == null)
                {
                    collisionBounds = Array.Empty<Bounds>();
                }
                return collisionBounds;
            }
        }

        /// <summary>
        /// Farming tools
        /// </summary>
        public IReadOnlyList<FarmingToolData> FarmingTools
        {
            get
            {
                if (farmingTools == null)
                {
                    farmingTools = Array.Empty<FarmingToolData>();
                }
                return farmingTools;
            }
        }

        /// <summary>
        /// Farming tool lookup
        /// </summary>
        public IReadOnlyDictionary<string, IFarmingToolData> FarmingToolLookup
        {
            get
            {
                if (farmingToolLookup == null)
                {
                    farmingToolLookup = new Dictionary<string, IFarmingToolData>();
                    foreach (IFarmingToolData farming_tool in FarmingTools)
                    {
                        string key = (farming_tool.FarmingToolItem ? farming_tool.FarmingToolItem.Key : string.Empty);
                        if (farmingToolLookup.ContainsKey(key))
                        {
                            Debug.LogError($"Skipping duplicate farming tool entry \"{ key }\" in block \"{ name }\".");
                        }
                        else
                        {
                            farmingToolLookup.Add(key, farming_tool);
                        }
                    }
                }
                return farmingToolLookup;
            }
        }

        /// <summary>
        /// Key
        /// </summary>
        public override string Key => $"Blocks/{ name }";

        /// <summary>
        /// Get farming tool data from farming tool
        /// </summary>
        /// <param name="item">Item</param>
        /// <returns>Farming tool data if successful, otherwise "null"</returns>
        public IFarmingToolData GetFarmingToolDataFromFarmingToolItem(IItemObject item)
        {
            string key = ((item == null) ? string.Empty : item.Key);
            if (!(FarmingToolLookup.ContainsKey(key)))
            {
                key = string.Empty;
            }
            return (FarmingToolLookup.ContainsKey(key) ? FarmingToolLookup[key] : null);
        }
    }
}

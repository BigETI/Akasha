using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Akasha objects namespace
/// </summary>
namespace Akasha.Objects
{
    /// <summary>
    /// Block mesh variants object script class
    /// </summary>
    [CreateAssetMenu(fileName = "BlockMeshVariants", menuName = "Akasha/Block mesh variants")]
    public class BlockMeshVariantsObjectScript : ScriptableObject, IBlockMeshVariantsObject
    {
        /// <summary>
        /// Block assets
        /// </summary>
        [SerializeField]
        private GameObject[] blockAssets = new GameObject[64];

        /// <summary>
        /// Close to mesh variants
        /// </summary>
        [SerializeField]
        private BlockMeshVariantsObjectScript[] closeToMeshVariants = Array.Empty<BlockMeshVariantsObjectScript>();

        /// <summary>
        /// Block assets
        /// </summary>
        public IReadOnlyList<GameObject> BlockAssets
        {
            get
            {
                if (blockAssets == null)
                {
                    blockAssets = new GameObject[64];
                }
                else if (blockAssets.Length != 64)
                {
                    Array.Resize(ref blockAssets, 64);
                }
                return blockAssets;
            }
        }

        /// <summary>
        /// Close to mesh variants
        /// </summary>
        public IReadOnlyList<BlockMeshVariantsObjectScript> CloseToMeshVariants
        {
            get
            {
                if (closeToMeshVariants == null)
                {
                    closeToMeshVariants = Array.Empty<BlockMeshVariantsObjectScript>();
                }
                return closeToMeshVariants;
            }
        }
    }
}

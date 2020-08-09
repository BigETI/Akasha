using Akasha.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Akasha objects namespace
/// </summary>
namespace Akasha.Objects
{
    /// <summary>
    /// Blocks prefabs object script class
    /// </summary>
    [CreateAssetMenu(fileName = "BlocksPrefab", menuName = "Akasha/Blocks prefab")]
    public class BlocksPrefabObjectScript : BlockObjectScript, IBlocksPrefabObject
    {
        /// <summary>
        /// Set blocks operation
        /// </summary>
        [SerializeField]
        private ESetBlocksOperation setBlocksOperation = default;

        /// <summary>
        /// Size
        /// </summary>
        [SerializeField]
        private Vector3Int size = Vector3Int.one;

        /// <summary>
        /// Offset
        /// </summary>
        [SerializeField]
        private Vector3Int offset = Vector3Int.zero;

        /// <summary>
        /// Blocks
        /// </summary>
        [SerializeField]
        private BlockObjectScript[] blocks = Array.Empty<BlockObjectScript>();

        /// <summary>
        /// Block data
        /// </summary>
        private BlockData[] blockData;

        /// <summary>
        /// Set blocks operation
        /// </summary>
        public ESetBlocksOperation SetBlocksOperation => setBlocksOperation;

        /// <summary>
        /// Size
        /// </summary>
        public Vector3Int Size => new Vector3Int(Mathf.Max(size.x, 1), Mathf.Max(size.y, 1), Mathf.Max(size.z, 1));

        /// <summary>
        /// Offset
        /// </summary>
        public Vector3Int Offset => offset;

        /// <summary>
        /// Blocks
        /// </summary>
        public IReadOnlyList<BlockData> Blocks
        {
            get
            {
                Initialize();
                return blockData;
            }
        }

        /// <summary>
        /// Key
        /// </summary>
        public override string Key => ("BlocksPrefabs/" + name);

        /// <summary>
        /// Initialize
        /// </summary>
        public void Initialize()
        {
            Vector3Int size = Size;
            int length = size.x * size.y * size.z;
            if ((blocks == null) || (blocks.Length != length))
            {
                blocks = new BlockObjectScript[length];
            }
            if ((blockData == null) || (blockData.Length != length))
            {
                blockData = new BlockData[length];
            }
            Parallel.For(0, length, (index) =>
            {
                BlockObjectScript block = blocks[index];
                blockData[index] = new BlockData(block, block ? block.InitialHealth : (ushort)0);
            });
        }
    }
}

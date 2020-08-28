using System;
using UnityEngine;

/// <summary>
/// Akasha data namespace
/// </summary>
namespace Akasha.Data
{
    /// <summary>
    /// Chunk data struct
    /// </summary>
    public struct ChunkData : IChunkData
    {
        /// <summary>
        /// Chunk ID
        /// </summary>
        [SerializeField]
        private ChunkID chunkID;

        /// <summary>
        /// Blocks
        /// </summary>
        [SerializeField]
        private BlockData[] blocks;

        /// <summary>
        /// Chunk ID
        /// </summary>
        public ChunkID ChunkID
        {
            get => chunkID;
            set => chunkID = value;
        }

        /// <summary>
        /// Blocks
        /// </summary>
        public BlockData[] Blocks
        {
            get => blocks;
            set => blocks = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Is valid
        /// </summary>
        public bool IsValid => (blocks != null);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="chunkID">Chunk ID</param>
        /// <param name="blocks">Blocks</param>
        public ChunkData(ChunkID chunkID, BlockData[] blocks)
        {
            this.chunkID = chunkID;
            this.blocks = blocks ?? throw new ArgumentNullException(nameof(blocks));
        }
    }
}

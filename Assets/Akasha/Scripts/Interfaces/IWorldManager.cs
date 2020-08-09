﻿using Akasha.Controllers;
using Akasha.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// World manager interface
    /// </summary>
    public interface IWorldManager : IBehaviour
    {
        /// <summary>
        /// Block lookup
        /// </summary>
        IReadOnlyDictionary<string, IBlockObject> BlockLookup { get; }

        /// <summary>
        /// Chunk size
        /// </summary>
        Vector3Int ChunkSize { get; set; }

        /// <summary>
        /// Render distances
        /// </summary>
        Vector3Int RenderDistances { get; set; }

        /// <summary>
        /// Chunk grid size
        /// </summary>
        Vector3Int GridSize { get; }

        /// <summary>
        /// Force chunk refresh grid distance
        /// </summary>
        float ForceChunkRefreshGridDistance { get; set; }

        /// <summary>
        /// Clip view angle
        /// </summary>
        float ClipViewAngle { get; set; }

        /// <summary>
        /// Follow transform controller
        /// </summary>
        WorldTransformControllerScript FollowTransformController { get; set; }

        /// <summary>
        /// Biomes
        /// </summary>
        BiomeData[] Biomes { get; set; }

        /// <summary>
        /// Showing blocks prefab
        /// </summary>
        IBlocksPrefabObject ShowingBlocksPrefab { get; set; }

        /// <summary>
        /// Get chunk ID from block ID
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <returns>Chunk ID</returns>
        ChunkID GetChunkIDFDromBlockID(BlockID blockID);

        /// <summary>
        /// Get biome from block ID
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <returns>Biome if successful, otherwise "null"</returns>
        IBiomeData GetBiomeFromBlockID(BlockID blockID);

        /// <summary>
        /// Get generated block
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <returns>Block</returns>
        BlockData GetGeneratedBlock(BlockID blockID);

        /// <summary>
        /// Refresh chunk controller
        /// </summary>
        /// <param name="chunkID">Chunk ID</param>
        void RefreshChunkController(ChunkID chunkID);

        /// <summary>
        /// Get chunk controller
        /// </summary>
        /// <param name="chunkID">Chunk ID</param>
        /// <returns>Chunk controller if available, otherwise "null"</returns>
        ChunkControllerScript GetChunkController(ChunkID chunkID);

        /// <summary>
        /// Get chunk blocks task (asynchronous)
        /// </summary>
        /// <param name="chunkID">Chunk ID</param>
        /// <returns>Blocks task</returns>
        Task<BlockData[]> GetChunkBlocksTask(ChunkID chunkID);

        /// <summary>
        /// Get block
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <returns>Block</returns>
        BlockData GetBlock(BlockID blockID);

        /// <summary>
        /// Set block
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <param name="block">Block</param>
        void SetBlock(BlockID blockID, BlockData block);

        /// <summary>
        /// Set blocks
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <param name="size">Size</param>
        /// <param name="blocks">Blocks</param>
        /// <param name="setBlocksOperation">Set blocks operation</param>
        void SetBlocks(BlockID blockID, Vector3Int size, IReadOnlyList<BlockData> blocks, ESetBlocksOperation setBlocksOperation);

        /// <summary>
        /// Reset chunks
        /// </summary>
        void ResetChunks();
    }
}

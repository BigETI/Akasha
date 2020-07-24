using Akasha.Controllers;
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
        /// Noise layers
        /// </summary>
        NoiseLayerData[] NoiseLayers { get; set; }

        /// <summary>
        /// Get chunk ID from block ID
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <returns>Chunk ID</returns>
        ChunkID GetChunkIDFDromBlockID(BlockID blockID);

        /// <summary>
        /// Get generated block type
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <returns>Block or "null"</returns>
        IBlockObject GetGeneratedBlockType(BlockID blockID);

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
        Task<IBlockObject[]> GetChunkBlocksTask(ChunkID chunkID);

        /// <summary>
        /// Set block type
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <param name="blockType">Block type</param>
        void SetBlockType(BlockID blockID, IBlockObject blockType);

        /// <summary>
        /// Reset
        /// </summary>
        void ResetChunks();
    }
}

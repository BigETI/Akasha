using Akasha.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// World IO interface
    /// </summary>
    public interface IWorldIO : IDisposable
    {
        /// <summary>
        /// World GUID
        /// </summary>
        Guid WorldGUID { get; }

        /// <summary>
        /// World name
        /// </summary>
        string WorldName { get; }

        /// <summary>
        /// World description
        /// </summary>
        string WorldDescription { get; }

        /// <summary>
        /// World seed
        /// </summary>
        int WorldSeed { get; }

        /// <summary>
        /// Chunk size
        /// </summary>
        Vector3Int ChunkSize { get; }

        /// <summary>
        /// World chunks file stream
        /// </summary>
        ReopenableFileStream WorldChunksFileStream { get; }

        /// <summary>
        /// World entities file stream
        /// </summary>
        ReopenableFileStream WorldEntitiesFileStream { get; }

        /// <summary>
        /// World manager
        /// </summary>
        IWorldManager WorldManager { get; }

        /// <summary>
        /// Can read
        /// </summary>
        bool CanRead { get; }

        /// <summary>
        /// Can write
        /// </summary>
        bool CanWrite { get; }

        /// <summary>
        /// Is chunk available
        /// </summary>
        /// <param name="chunkID">Chunk ID</param>
        /// <returns>"true" if chunk is available, otherwise "false"</returns>
        bool IsChunkAvailable(ChunkID chunkID);

        /// <summary>
        /// Create read chunk blocks task
        /// </summary>
        /// <param name="chunkID">Chunk ID</param>
        /// <returns>Read chunk task</returns>
        Task<BlockData[]> CreateReadChunkBlocksTask(ChunkID chunkID);

        /// <summary>
        /// Create read chunk entities task
        /// </summary>
        /// <param name="chunkID">Chunk ID</param>
        /// <returns>Read chunk entities task</returns>
        Task<WorldEntityData[]> CreateReadChunkEntitiesTask(ChunkID chunkID);

        /// <summary>
        /// Create read players task
        /// </summary>
        /// <returns>Read players task</returns>
        Task<IReadOnlyDictionary<string, WorldPlayerData>> CreateReadPlayersTask();

        /// <summary>
        /// Create write chunks task
        /// </summary>
        /// <param name="chunks">Chunks</param>
        /// <param name="deleteChunks">Delete chunks</param>
        /// <returns>Result task contains "true" if successful, otherwise "false"</returns>
        Task<bool> CreateWriteChunksTask(IEnumerable<ChunkData> chunks, IEnumerable<ChunkID> deleteChunks);

        /// <summary>
        /// Create write entities task
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <param name="deleteEntities">Delete entities</param>
        /// <returns>Result task contains "true" if successful, otherwise "false"</returns>
        Task<bool> CreateWriteEntitiesTask(IEnumerable<WorldEntityData> entities, IEnumerable<string> deleteEntities);

        /// <summary>
        /// Create save task
        /// </summary>
        /// <returns>Result task contains "true" if successful, otherwise "false"</returns>
        Task<bool> CreateSaveTask();

        /// <summary>
        /// Create write players task
        /// </summary>
        /// <param name="players">Players</param>
        /// <returns>Result task contains "true" if successful, otherwise "false"</returns>
        Task<bool> CreateWritePlayersTask(IEnumerable<WorldPlayerData> players);
    }
}

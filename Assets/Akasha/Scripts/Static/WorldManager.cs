using Akasha.Managers;
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
    /// World manager class
    /// </summary>
    public static class WorldManager
    {
        /// <summary>
        /// Chunk block types tasks lookup
        /// </summary>
        private static readonly Dictionary<ChunkID, Task<IBlockObject[]>> chunkBlocksTasksLookup = new Dictionary<ChunkID, Task<IBlockObject[]>>();

        /// <summary>
        /// Get generated blocks
        /// </summary>
        /// <param name="worldManager">World manager</param>
        /// <param name="chunkID">Chunk ID</param>
        /// <returns>Generated block types</returns>
        private static IBlockObject[] GetGeneratedBlockTypes(WorldManagerScript worldManager, ChunkID chunkID)
        {
            Vector3Int chunk_size = worldManager.ChunkSize;
            IBlockObject[] ret = new IBlockObject[chunk_size.x * chunk_size.y * chunk_size.z];
            Parallel.For(0, ret.Length, (index) => ret[index] = worldManager.GetGeneratedBlockType(new BlockID((index % chunk_size.x) + (chunkID.X * chunk_size.x), ((index / chunk_size.x) % chunk_size.y) + (chunkID.Y * chunk_size.y), (index / (chunk_size.x * chunk_size.y)) + (chunkID.Z * chunk_size.z))));
            return ret;
        }

        /// <summary>
        /// Get chunk blocks task (asynchronous)
        /// </summary>
        /// <param name="chunkID">Chunk ID</param>
        /// <returns>Blocks task</returns>
        public static Task<IBlockObject[]> GetChunkBlocksTask(ChunkID chunkID)
        {
            Task<IBlockObject[]> ret = Task.FromResult(Array.Empty<IBlockObject>());
            WorldManagerScript world_manager = WorldManagerScript.Instance;
            if (world_manager != null)
            {
                int block_count = world_manager.ChunkSize.x * world_manager.ChunkSize.y * world_manager.ChunkSize.z;
                lock (chunkBlocksTasksLookup)
                {
                    if (chunkBlocksTasksLookup.ContainsKey(chunkID))
                    {
                        ret = chunkBlocksTasksLookup[chunkID];
                    }
                    else
                    {
                        ChunkID chunk_id = chunkID;
                        ret = Task.Run(() => GetGeneratedBlockTypes(world_manager, chunk_id));
                        chunkBlocksTasksLookup.Add(chunkID, ret);
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Set block type
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <param name="blockType">Block type</param>
        public static void SetBlockType(BlockID blockID, IBlockObject blockType)
        {
            WorldManagerScript world_manager = WorldManagerScript.Instance;
            if (world_manager != null)
            {
                Vector3Int chunk_size = world_manager.ChunkSize;
                ChunkID chunk_id = world_manager.GetChunkIDFDromBlockID(blockID);
                int block_count = chunk_size.x * chunk_size.y * chunk_size.z;
                // Test if blocks are noticable
                IBlockObject[] block_types = GetChunkBlocksTask(chunk_id).Result;
                if (block_types.Length == block_count)
                {
                    Vector3Int chunk_position = new Vector3Int((int)(blockID.X - ((long)(chunk_id.X) * chunk_size.x)), (int)(blockID.Y - ((long)(chunk_id.Y) * chunk_size.y)), (int)(blockID.Z - ((long)(chunk_id.Z) * chunk_size.z)));
                    int index = chunk_position.x + (chunk_position.y * chunk_size.x) + (chunk_position.z * chunk_size.x * chunk_size.y);
                    block_types[index] = blockType;
                    world_manager.RefreshChunkController(chunk_id);
                    if ((chunk_position.y + 1) >= chunk_size.y)
                    {
                        world_manager.RefreshChunkController(chunk_id + ChunkID.Up);
                    }
                    if (chunk_position.y <= 0)
                    {
                        world_manager.RefreshChunkController(chunk_id + ChunkID.Down);
                    }
                    if (chunk_position.x <= 0)
                    {
                        world_manager.RefreshChunkController(chunk_id + ChunkID.Left);
                    }
                    if ((chunk_position.x + 1) >= chunk_size.x)
                    {
                        world_manager.RefreshChunkController(chunk_id + ChunkID.Right);
                    }
                    if ((chunk_position.z + 1) >= chunk_size.z)
                    {
                        world_manager.RefreshChunkController(chunk_id + ChunkID.Forward);
                    }
                    if (chunk_position.z <= 0)
                    {
                        world_manager.RefreshChunkController(chunk_id + ChunkID.Back);
                    }
                }
            }
        }

        /// <summary>
        /// Reset
        /// </summary>
        public static void Reset()
        {
            foreach (Task<IBlockObject[]> chunk_block_types_task in chunkBlocksTasksLookup.Values)
            {
                try
                {
                    chunk_block_types_task.Dispose();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
            chunkBlocksTasksLookup.Clear();
        }
    }
}

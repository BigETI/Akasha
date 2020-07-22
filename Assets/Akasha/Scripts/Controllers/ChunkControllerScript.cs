using Akasha.Managers;
using Akasha.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Chunk controller script class
    /// </summary>
    public class ChunkControllerScript : MonoBehaviour, IChunkController
    {
        /// <summary>
        /// Chunk ID
        /// </summary>
        private ChunkID chunkID;

        /// <summary>
        /// Refreesh on update
        /// </summary>
        private bool refreshOnUpdate = false;

        /// <summary>
        /// Chunk blocks task
        /// </summary>
        private Task<IBlockObject[]> chunkBlocksTask = null;

        /// <summary>
        /// Instantiated blocks
        /// </summary>
        private InstantiatedGameObject[] instantiatedBlocks = Array.Empty<InstantiatedGameObject>();

        /// <summary>
        /// Change blocks
        /// </summary>
        private readonly Dictionary<int, BlockChange> changeBlocks = new Dictionary<int, BlockChange>();

        /// <summary>
        /// Chunk ID
        /// </summary>
        public ChunkID ChunkID
        {
            get => chunkID;
            set => chunkID = value;
        }

        /// <summary>
        /// Top chunk controller
        /// </summary>
        public ChunkControllerScript TopChunkController { get; set; }

        /// <summary>
        /// Bottom chunk controller
        /// </summary>
        public ChunkControllerScript BottomChunkController { get; set; }

        /// <summary>
        /// Left chunk controller
        /// </summary>
        public ChunkControllerScript LeftChunkController { get; set; }

        /// <summary>
        /// Right chunk controller
        /// </summary>
        public ChunkControllerScript RightChunkController { get; set; }

        /// <summary>
        /// Front chunk controller
        /// </summary>
        public ChunkControllerScript FrontChunkController { get; set; }

        /// <summary>
        /// Behind chunk controller
        /// </summary>
        public ChunkControllerScript BehindChunkController { get; set; }

        /// <summary>
        /// Refresh
        /// </summary>
        public void Refresh() => refreshOnUpdate = true;

        /// <summary>
        /// Get chunk blocks task from chunk
        /// </summary>
        /// <param name="targetChunkController">Target chunk controller</param>
        /// <param name="targetChunkID">Target chunk ID</param>
        /// <returns>Chunk block types</returns>
        private Task<IBlockObject[]> GetChunkBlocksTaskFromChunk(ChunkControllerScript targetChunkController, ChunkID targetChunkID)
        {
            Task<IBlockObject[]> ret = ((targetChunkController == null) ? null : targetChunkController.chunkBlocksTask);
            if (ret == null)
            {
                ret = WorldManager.GetChunkBlocksTask(targetChunkID);
            }
            return ret;
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            WorldManagerScript world_manager = WorldManagerScript.Instance;
            if (world_manager != null)
            {
                Vector3Int chunk_size = world_manager.ChunkSize;
                ChunkID chunk_id = chunkID;
                if (refreshOnUpdate)
                {
                    int block_count = chunk_size.x * chunk_size.y * chunk_size.z;
                    Task<IBlockObject[]> top_chunk_blocks_task = GetChunkBlocksTaskFromChunk(TopChunkController, chunkID + ChunkID.Up);
                    Task<IBlockObject[]> bottom_chunk_blocks_task = GetChunkBlocksTaskFromChunk(BottomChunkController, chunkID + ChunkID.Down);
                    Task<IBlockObject[]> left_chunk_blocks_task = GetChunkBlocksTaskFromChunk(LeftChunkController, chunkID + ChunkID.Left);
                    Task<IBlockObject[]> right_chunk_blocks_task = GetChunkBlocksTaskFromChunk(RightChunkController, chunkID + ChunkID.Right);
                    Task<IBlockObject[]> front_chunk_blocks_task = GetChunkBlocksTaskFromChunk(FrontChunkController, chunkID + ChunkID.Forward);
                    Task<IBlockObject[]> behind_chunk_blocks_task = GetChunkBlocksTaskFromChunk(BehindChunkController, chunkID + ChunkID.Back);
                    if (instantiatedBlocks.Length != block_count)
                    {
                        foreach (InstantiatedGameObject instantiated_block in instantiatedBlocks)
                        {
                            if (instantiated_block.Instance != null)
                            {
                                Destroy(instantiated_block.Instance);
                            }
                        }
                        chunkBlocksTask = null;
                        instantiatedBlocks = new InstantiatedGameObject[block_count];
                    }
                    chunkBlocksTask = WorldManager.GetChunkBlocksTask(chunk_id);
                    if
                    (
                        (chunkBlocksTask.Status == TaskStatus.RanToCompletion) &&
                        (top_chunk_blocks_task.Status == TaskStatus.RanToCompletion) &&
                        (bottom_chunk_blocks_task.Status == TaskStatus.RanToCompletion) &&
                        (left_chunk_blocks_task.Status == TaskStatus.RanToCompletion) &&
                        (right_chunk_blocks_task.Status == TaskStatus.RanToCompletion) &&
                        (front_chunk_blocks_task.Status == TaskStatus.RanToCompletion) &&
                        (behind_chunk_blocks_task.Status == TaskStatus.RanToCompletion)
                    )
                    {
                        IBlockObject[] chunk_blocks = chunkBlocksTask.Result;
                        IBlockObject[] top_chunk_blocks = top_chunk_blocks_task.Result;
                        IBlockObject[] bottom_chunk_blocks = bottom_chunk_blocks_task.Result;
                        IBlockObject[] left_chunk_blocks = left_chunk_blocks_task.Result;
                        IBlockObject[] right_chunk_blocks = right_chunk_blocks_task.Result;
                        IBlockObject[] front_chunk_blocks = front_chunk_blocks_task.Result;
                        IBlockObject[] behind_chunk_blocks = behind_chunk_blocks_task.Result;
                        if
                        (
                            (chunk_blocks.Length == block_count) &&
                            (top_chunk_blocks.Length == block_count) &&
                            (bottom_chunk_blocks.Length == block_count) &&
                            (left_chunk_blocks.Length == block_count) &&
                            (right_chunk_blocks.Length == block_count) &&
                            (front_chunk_blocks.Length == block_count) &&
                            (behind_chunk_blocks.Length == block_count)
                        )
                        {
                            changeBlocks.Clear();
                            Parallel.For(0, instantiatedBlocks.Length, (index) =>
                            {
                                Vector3Int chunk_position = new Vector3Int(index % chunk_size.x, (index / chunk_size.x) % chunk_size.y, index / (chunk_size.x * chunk_size.y));
                                BlockID block_position = new BlockID(chunk_position.x + (chunk_id.X * chunk_size.x), chunk_position.y + (chunk_id.Y * chunk_size.y), chunk_position.z + (chunk_id.Z * chunk_size.z));
                                IBlockObject block = chunk_blocks[index];
                                IBlockObject top_block = ((chunk_position.y + 1) < chunk_size.y) ? chunk_blocks[chunk_position.x + ((chunk_position.y + 1) * chunk_size.x) + (chunk_position.z * chunk_size.x * chunk_size.y)] : ((top_chunk_blocks == null) ? null : top_chunk_blocks[chunk_position.x + (chunk_position.z * chunk_size.x * chunk_size.y)]);
                                IBlockObject bottom_block = ((chunk_position.y - 1) >= 0) ? chunk_blocks[chunk_position.x + ((chunk_position.y - 1) * chunk_size.x) + (chunk_position.z * chunk_size.x * chunk_size.y)] : ((bottom_chunk_blocks == null) ? null : bottom_chunk_blocks[chunk_position.x + ((chunk_size.y - 1) * chunk_size.x) + (chunk_position.z * chunk_size.x * chunk_size.y)]);
                                IBlockObject left_block = (((chunk_position.x - 1) >= 0) ? chunk_blocks[(chunk_position.x - 1) + (chunk_position.y * chunk_size.x) + (chunk_position.z * chunk_size.x * chunk_size.y)] : ((left_chunk_blocks == null) ? null : left_chunk_blocks[(chunk_size.x - 1) + (chunk_position.y * chunk_size.x) + (chunk_position.z * chunk_size.x * chunk_size.y)]));
                                IBlockObject right_block = (((chunk_position.x + 1) < chunk_size.x) ? chunk_blocks[(chunk_position.x + 1) + (chunk_position.y * chunk_size.x) + (chunk_position.z * chunk_size.x * chunk_size.y)] : ((right_chunk_blocks == null) ? null : right_chunk_blocks[(chunk_position.y * chunk_size.x) + (chunk_position.z * chunk_size.x * chunk_size.y)]));
                                IBlockObject front_block = (((chunk_position.z + 1) < chunk_size.z) ? chunk_blocks[chunk_position.x + (chunk_position.y * chunk_size.x) + ((chunk_position.z + 1) * chunk_size.x * chunk_size.y)] : ((front_chunk_blocks == null) ? null : front_chunk_blocks[chunk_position.x + (chunk_position.y * chunk_size.x)]));
                                IBlockObject behind_block = (((chunk_position.z - 1) >= 0) ? chunk_blocks[chunk_position.x + (chunk_position.y * chunk_size.x) + ((chunk_position.z - 1) * chunk_size.x * chunk_size.y)] : ((behind_chunk_blocks == null) ? null : behind_chunk_blocks[chunk_position.x + (chunk_position.y * chunk_size.x) + ((chunk_size.z - 1) * chunk_size.x * chunk_size.y)]));
                                IBlockMeshVariantsObject block_mesh_variants = ((block == null) ? null : block.MeshVariants);
                                IBlockMeshVariantsObject top_block_mesh_variants = ((top_block == null) ? null : top_block.MeshVariants);
                                IBlockMeshVariantsObject bottom_block_mesh_variants = ((bottom_block == null) ? null : bottom_block.MeshVariants);
                                IBlockMeshVariantsObject left_block_mesh_variants = ((left_block == null) ? null : left_block.MeshVariants);
                                IBlockMeshVariantsObject right_block_mesh_variants = ((right_block == null) ? null : right_block.MeshVariants);
                                IBlockMeshVariantsObject front_block_mesh_variants = ((front_block == null) ? null : front_block.MeshVariants);
                                IBlockMeshVariantsObject behind_block_mesh_variants = ((behind_block == null) ? null : behind_block.MeshVariants);
                                EDirectionFlags direction_flags = EDirectionFlags.Nothing;
                                if ((block_mesh_variants != null) && (block_mesh_variants.CloseToMeshVariants != null))
                                {
                                    foreach (IBlockMeshVariantsObject close_to_block_mesh_variants in block_mesh_variants.CloseToMeshVariants)
                                    {
                                        if (close_to_block_mesh_variants != null)
                                        {
                                            direction_flags |=
                                            (
                                                ((close_to_block_mesh_variants == top_block_mesh_variants) ? EDirectionFlags.Top : EDirectionFlags.Nothing) |
                                                ((close_to_block_mesh_variants == bottom_block_mesh_variants) ? EDirectionFlags.Bottom : EDirectionFlags.Nothing) |
                                                ((close_to_block_mesh_variants == left_block_mesh_variants) ? EDirectionFlags.Left : EDirectionFlags.Nothing) |
                                                ((close_to_block_mesh_variants == right_block_mesh_variants) ? EDirectionFlags.Right : EDirectionFlags.Nothing) |
                                                ((close_to_block_mesh_variants == front_block_mesh_variants) ? EDirectionFlags.Front : EDirectionFlags.Nothing) |
                                                ((close_to_block_mesh_variants == behind_block_mesh_variants) ? EDirectionFlags.Behind : EDirectionFlags.Nothing)
                                            );
                                        }
                                    }
                                }
                                InstantiatedGameObject instantiated_block = instantiatedBlocks[index];
                                if ((instantiated_block.Block != block) || (instantiated_block.DirectionFlags != direction_flags))
                                {
                                    lock (changeBlocks)
                                    {
                                        if (changeBlocks.ContainsKey(index))
                                        {
                                            changeBlocks[index] = new BlockChange(block, direction_flags);
                                        }
                                        else
                                        {
                                            changeBlocks.Add(index, new BlockChange(block, direction_flags));
                                        }
                                    }
                                }
                            });
                            refreshOnUpdate = false;
                        }
                    }
                }
                IReadOnlyDictionary<string, IBlockObject> block_lookup = world_manager.BlockLookup;
                foreach (KeyValuePair<int, BlockChange> change_block in changeBlocks)
                {
                    ref InstantiatedGameObject instantiated_block = ref instantiatedBlocks[change_block.Key];
                    if (instantiated_block.Instance != null)
                    {
                        Destroy(instantiated_block.Instance);
                    }
                    GameObject asset = null;
                    GameObject game_object = null;
                    Material material = null;
                    Vector3Int chunk_position = new Vector3Int(change_block.Key % chunk_size.x, (change_block.Key / chunk_size.x) % chunk_size.y, change_block.Key / (chunk_size.x * chunk_size.y));
                    if ((change_block.Value.Block != null) && (change_block.Value.Block.MeshVariants != null))
                    {
                        asset = change_block.Value.Block.MeshVariants.BlockAssets[(int)(change_block.Value.DirectionFlags)];
                        material = change_block.Value.Block.Material;
                    }
                    if (asset != null)
                    {
                        game_object = Instantiate(asset, (new Vector3((change_block.Key % chunk_size.x) - (chunk_size.x * 0.5f) + 0.5f, ((change_block.Key / chunk_size.x) % chunk_size.y) - (chunk_size.y * 0.5f) + 0.5f, (change_block.Key / (chunk_size.x * chunk_size.y)) - (chunk_size.z * 0.5f) + 0.5f)) + transform.position, Quaternion.identity, transform);
                        if (game_object != null)
                        {
                            MeshRenderer mesh_renderer = game_object.GetComponentInChildren<MeshRenderer>();
                            if (mesh_renderer != null)
                            {
                                mesh_renderer.sharedMaterial = material;
                            }
                            BlockID block_id = new BlockID((change_block.Key % chunk_size.x) + (chunk_id.X * chunk_size.x), ((change_block.Key / chunk_size.x) % chunk_size.y) + (chunk_id.Y * chunk_size.y), (change_block.Key / (chunk_size.x * chunk_size.y)) + (chunk_id.Z * chunk_size.z));
                            game_object.name = "Block " + block_id;
                        }
                    }
                    instantiated_block = new InstantiatedGameObject(change_block.Value.Block, change_block.Value.DirectionFlags, game_object);
                }
                changeBlocks.Clear();
            }
        }

        /// <summary>
        /// On draw gizmos
        /// </summary>
        private void OnDrawGizmos()
        {
            WorldManagerScript world_manager = WorldManagerScript.Instance;
            if (world_manager != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(transform.position, world_manager.ChunkSize);
            }
        }
    }
}

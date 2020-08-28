using Akasha.Data;
using Akasha.Managers;
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
        private Task<BlockData[]> chunkBlocksTask = null;

        /// <summary>
        /// Instantiated blocks
        /// </summary>
        private InstantiatedBlock[] instantiatedBlocks = Array.Empty<InstantiatedBlock>();

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
        /// <param name="worldManager">World manager</param>
        /// <returns>Chunk block types</returns>
        private Task<BlockData[]> GetChunkBlocksTaskFromChunk(ChunkControllerScript targetChunkController, ChunkID targetChunkID, WorldManagerScript worldManager)
        {
            Task<BlockData[]> ret = (targetChunkController ? targetChunkController.chunkBlocksTask : null);
            if (ret == null)
            {
                ret = worldManager.GetChunkBlocksTask(targetChunkID);
            }
            return ret;
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            WorldManagerScript world_manager = WorldManagerScript.Instance;
            if (world_manager)
            {
                Vector3Int chunk_size = world_manager.ChunkSize;
                ChunkID chunk_id = chunkID;
                if (refreshOnUpdate)
                {
                    int block_count = chunk_size.x * chunk_size.y * chunk_size.z;
                    Task<BlockData[]> top_chunk_blocks_task = GetChunkBlocksTaskFromChunk(TopChunkController, chunkID + ChunkID.Up, world_manager);
                    Task<BlockData[]> bottom_chunk_blocks_task = GetChunkBlocksTaskFromChunk(BottomChunkController, chunkID + ChunkID.Down, world_manager);
                    Task<BlockData[]> left_chunk_blocks_task = GetChunkBlocksTaskFromChunk(LeftChunkController, chunkID + ChunkID.Left, world_manager);
                    Task<BlockData[]> right_chunk_blocks_task = GetChunkBlocksTaskFromChunk(RightChunkController, chunkID + ChunkID.Right, world_manager);
                    Task<BlockData[]> front_chunk_blocks_task = GetChunkBlocksTaskFromChunk(FrontChunkController, chunkID + ChunkID.Forward, world_manager);
                    Task<BlockData[]> behind_chunk_blocks_task = GetChunkBlocksTaskFromChunk(BehindChunkController, chunkID + ChunkID.Back, world_manager);
                    if (instantiatedBlocks.Length != block_count)
                    {
                        foreach (InstantiatedBlock instantiated_block in instantiatedBlocks)
                        {
                            if (instantiated_block.Instance != null)
                            {
                                Destroy(instantiated_block.Instance);
                            }
                        }
                        chunkBlocksTask = null;
                        instantiatedBlocks = new InstantiatedBlock[block_count];
                    }
                    chunkBlocksTask = world_manager.GetChunkBlocksTask(chunk_id);
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
                        BlockData[] chunk_blocks = chunkBlocksTask.Result;
                        BlockData[] top_chunk_blocks = top_chunk_blocks_task.Result;
                        BlockData[] bottom_chunk_blocks = bottom_chunk_blocks_task.Result;
                        BlockData[] left_chunk_blocks = left_chunk_blocks_task.Result;
                        BlockData[] right_chunk_blocks = right_chunk_blocks_task.Result;
                        BlockData[] front_chunk_blocks = front_chunk_blocks_task.Result;
                        BlockData[] behind_chunk_blocks = behind_chunk_blocks_task.Result;
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
                                BlockData block = chunk_blocks[index];
                                BlockData top_block = ((chunk_position.y + 1) < chunk_size.y) ? chunk_blocks[chunk_position.x + ((chunk_position.y + 1) * chunk_size.x) + (chunk_position.z * chunk_size.x * chunk_size.y)] : ((top_chunk_blocks == null) ? default : top_chunk_blocks[chunk_position.x + (chunk_position.z * chunk_size.x * chunk_size.y)]);
                                BlockData bottom_block = ((chunk_position.y - 1) >= 0) ? chunk_blocks[chunk_position.x + ((chunk_position.y - 1) * chunk_size.x) + (chunk_position.z * chunk_size.x * chunk_size.y)] : ((bottom_chunk_blocks == null) ? default : bottom_chunk_blocks[chunk_position.x + ((chunk_size.y - 1) * chunk_size.x) + (chunk_position.z * chunk_size.x * chunk_size.y)]);
                                BlockData left_block = (((chunk_position.x - 1) >= 0) ? chunk_blocks[(chunk_position.x - 1) + (chunk_position.y * chunk_size.x) + (chunk_position.z * chunk_size.x * chunk_size.y)] : ((left_chunk_blocks == null) ? default : left_chunk_blocks[(chunk_size.x - 1) + (chunk_position.y * chunk_size.x) + (chunk_position.z * chunk_size.x * chunk_size.y)]));
                                BlockData right_block = (((chunk_position.x + 1) < chunk_size.x) ? chunk_blocks[(chunk_position.x + 1) + (chunk_position.y * chunk_size.x) + (chunk_position.z * chunk_size.x * chunk_size.y)] : ((right_chunk_blocks == null) ? default : right_chunk_blocks[(chunk_position.y * chunk_size.x) + (chunk_position.z * chunk_size.x * chunk_size.y)]));
                                BlockData front_block = (((chunk_position.z + 1) < chunk_size.z) ? chunk_blocks[chunk_position.x + (chunk_position.y * chunk_size.x) + ((chunk_position.z + 1) * chunk_size.x * chunk_size.y)] : ((front_chunk_blocks == null) ? default : front_chunk_blocks[chunk_position.x + (chunk_position.y * chunk_size.x)]));
                                BlockData behind_block = (((chunk_position.z - 1) >= 0) ? chunk_blocks[chunk_position.x + (chunk_position.y * chunk_size.x) + ((chunk_position.z - 1) * chunk_size.x * chunk_size.y)] : ((behind_chunk_blocks == null) ? default : behind_chunk_blocks[chunk_position.x + (chunk_position.y * chunk_size.x) + ((chunk_size.z - 1) * chunk_size.x * chunk_size.y)]));
                                IBlockMeshVariantsObject block_mesh_variants = (block.IsABlock ? block.Block.MeshVariants : null);
                                IBlockMeshVariantsObject top_block_mesh_variants = (top_block.IsABlock ? top_block.Block.MeshVariants : null);
                                IBlockMeshVariantsObject bottom_block_mesh_variants = (bottom_block.IsABlock ? bottom_block.Block.MeshVariants : null);
                                IBlockMeshVariantsObject left_block_mesh_variants = (left_block.IsABlock ? left_block.Block.MeshVariants : null);
                                IBlockMeshVariantsObject right_block_mesh_variants = (right_block.IsABlock ? right_block.Block.MeshVariants : null);
                                IBlockMeshVariantsObject front_block_mesh_variants = (front_block.IsABlock ? front_block.Block.MeshVariants : null);
                                IBlockMeshVariantsObject behind_block_mesh_variants = (behind_block.IsABlock ? behind_block.Block.MeshVariants : null);
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
                                InstantiatedBlock instantiated_block = instantiatedBlocks[index];
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
                foreach (KeyValuePair<int, BlockChange> change_block in changeBlocks)
                {
                    ref InstantiatedBlock instantiated_block = ref instantiatedBlocks[change_block.Key];
                    if (instantiated_block.Instance)
                    {
                        Destroy(instantiated_block.Instance);
                    }
                    GameObject asset = null;
                    GameObject game_object = null;
                    Material material = null;
                    Vector3Int chunk_position = new Vector3Int(change_block.Key % chunk_size.x, (change_block.Key / chunk_size.x) % chunk_size.y, change_block.Key / (chunk_size.x * chunk_size.y));
                    bool has_random_orientation = false;
                    if (change_block.Value.Block.IsABlock)
                    {
                        if (change_block.Value.Block.Block is IBlocksPrefabObject blocks_prefab)
                        {
                            BlockID block_id = new BlockID((chunkID.X * chunk_size.x) + chunk_position.x, (chunkID.Y * chunk_size.y) + chunk_position.y, (chunkID.Z * chunk_size.z) + chunk_position.z);
                            if (blocks_prefab.SetBlocksOperation != ESetBlocksOperation.ReplaceFull)
                            {
                                world_manager.SetBlock(block_id, default);
                            }
                            world_manager.SetBlocks(new BlockID(block_id.X + blocks_prefab.Offset.x, block_id.Y + blocks_prefab.Offset.y, block_id.Z + blocks_prefab.Offset.z), blocks_prefab.Size, blocks_prefab.Blocks, blocks_prefab.SetBlocksOperation);
                        }
                        else if (change_block.Value.Block.Block.MeshVariants != null)
                        {
                            asset = change_block.Value.Block.Block.MeshVariants.BlockAssets[(int)(change_block.Value.DirectionFlags)];
                            material = change_block.Value.Block.Block.Material;
                            has_random_orientation = change_block.Value.Block.Block.HasRandomOrientation;
                        }
                    }
                    if (asset != null)
                    {
                        game_object = Instantiate(asset, (new Vector3((change_block.Key % chunk_size.x) - (chunk_size.x * 0.5f) + 0.5f, ((change_block.Key / chunk_size.x) % chunk_size.y) - (chunk_size.y * 0.5f) + 0.5f, (change_block.Key / (chunk_size.x * chunk_size.y)) - (chunk_size.z * 0.5f) + 0.5f)) + transform.position, Quaternion.identity, transform);
                        if (game_object != null)
                        {
                            BlockID block_id = new BlockID((change_block.Key % chunk_size.x) + (chunk_id.X * chunk_size.x), ((change_block.Key / chunk_size.x) % chunk_size.y) + (chunk_id.Y * chunk_size.y), (change_block.Key / (chunk_size.x * chunk_size.y)) + (chunk_id.Z * chunk_size.z));
                            if (has_random_orientation)
                            {
                                Quaternion rotation = Quaternion.AngleAxis(Mathf.Repeat((block_id.X * 6154.48562f).GetHashCode() + (block_id.Z * 895652.854712f).GetHashCode(), 360.0f - float.Epsilon), Vector3.up);
                                for (int index = 0, child_count = game_object.transform.childCount; index < child_count; index++)
                                {
                                    Transform child_transform = game_object.transform.GetChild(index);
                                    if (child_transform)
                                    {
                                        child_transform.localRotation = rotation;
                                    }
                                }
                            }
                            MeshRenderer[] mesh_renderers = game_object.GetComponentsInChildren<MeshRenderer>();
                            if (mesh_renderers != null)
                            {
                                foreach (MeshRenderer mesh_renderer in mesh_renderers)
                                {
                                    if (mesh_renderer)
                                    {
                                        mesh_renderer.sharedMaterial = material;
                                    }
                                }
                            }
                            game_object.name = $"Block { block_id }";
                        }
                    }
                    instantiated_block = new InstantiatedBlock(change_block.Value.Block, change_block.Value.DirectionFlags, game_object);
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

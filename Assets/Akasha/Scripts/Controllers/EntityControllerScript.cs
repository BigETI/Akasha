using Akasha.Data;
using Akasha.Managers;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Entity controller script class
    /// </summary>
    [RequireComponent(typeof(WorldTransformControllerScript))]
    public class EntityControllerScript : MonoBehaviour, IEntityController
    {
        /// <summary>
        /// Bounds
        /// </summary>
        [SerializeField]
        private Bounds bounds = new Bounds(Vector3.zero, Vector3.one);

        /// <summary>
        /// Maximal sample collision count
        /// </summary>
        [SerializeField]
        private uint maximalSampleCollisionCount = 16U;

        /// <summary>
        /// Maximal sample collision count
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float sampleCollisionDistanceMultiplier = 0.5f;

        /// <summary>
        /// Chunk blocks tasks
        /// </summary>
        private Task<BlockData[]>[] chunkBlocksTasks;

        /// <summary>
        /// Chunk blocks
        /// </summary>
        private BlockData[][] chunkBlocks = null;

        /// <summary>
        /// Initialize chunk blocks tasks
        /// </summary>
        private bool initializeChunkBlocksTasks = true;

        /// <summary>
        /// Last bounds chunk ID
        /// </summary>
        private ChunkID lastBoundsChunkID;

        /// <summary>
        /// Last chunk blocks size
        /// </summary>
        private Vector3Int lastChunkBlocksSize;

        /// <summary>
        /// Bounds
        /// </summary>
        public Bounds Bounds
        {
            get => bounds;
            set => bounds = value;
        }

        /// <summary>
        /// Maximal sample collision count
        /// </summary>
        public uint MaximalSampleCollisionCount
        {
            get => maximalSampleCollisionCount;
            set => maximalSampleCollisionCount = value;
        }

        /// <summary>
        /// Maximal sample collision count
        /// </summary>
        public float SampleCollisionDistanceMultiplier
        {
            get => Mathf.Clamp(sampleCollisionDistanceMultiplier, 0.0f, 1.0f);
            set => sampleCollisionDistanceMultiplier = Mathf.Clamp(value, 0.0f, 1.0f);
        }

        /// <summary>
        /// Bounds chunk blocks size
        /// </summary>
        public Vector3Int BoundsChunkBlocksSize
        {
            get
            {
                Vector3Int ret = Vector3Int.one * 3;
                WorldManagerScript world_manager = WorldManagerScript.Instance;
                if (world_manager)
                {
                    Vector3 bounds_size = bounds.size;
                    Vector3Int chunk_size = world_manager.ChunkSize;
                    ret = new Vector3Int((((Mathf.FloorToInt(bounds_size.x / chunk_size.x) + 1) / 2) * 2) + 3, (((Mathf.FloorToInt(bounds_size.y / chunk_size.y) + 1) / 2) * 2) + 3, (((Mathf.FloorToInt(bounds_size.z / chunk_size.z) + 1) / 2) * 2) + 3);
                }
                return ret;
            }
        }

        /// <summary>
        /// World transform controller
        /// </summary>
        public IWorldTransformController WorldTransformController { get; private set; }

        /// <summary>
        /// Update chunk block tasks
        /// </summary>
        /// <param name="boundsChunkID">Bounds chunk ID</param>
        /// <param name="boundsChunkBlocksSize">Bounds chunk block size</param>
        /// <param name="worldManager">World manager</param>
        private void UpdateChunkBlocksTask(ChunkID boundsChunkID, Vector3Int boundsChunkBlocksSize, WorldManagerScript worldManager)
        {
            Vector3 bounds_center = bounds.center;
            int bounds_chunk_blocks_count = boundsChunkBlocksSize.x * boundsChunkBlocksSize.y * boundsChunkBlocksSize.z;
            if ((chunkBlocks == null) || (chunkBlocks.Length != bounds_chunk_blocks_count))
            {
                chunkBlocks = new BlockData[bounds_chunk_blocks_count][];
            }
            if ((chunkBlocksTasks == null) || (chunkBlocksTasks.Length != chunkBlocks.Length))
            {
                chunkBlocksTasks = new Task<BlockData[]>[chunkBlocks.Length];
            }
            Parallel.For(0, chunkBlocksTasks.Length, (index) => chunkBlocksTasks[index] = worldManager.GetChunkBlocksTask(new ChunkID(boundsChunkID.X + (index % boundsChunkBlocksSize.x) - ((boundsChunkBlocksSize.x - 1) / 2), boundsChunkID.Y + ((index / 3) % 3) - ((boundsChunkBlocksSize.y - 1) / 2), boundsChunkID.Z + (index / 9) - ((boundsChunkBlocksSize.z - 1) / 2))));
            initializeChunkBlocksTasks = true;
        }

        /// <summary>
        /// Get bounds center block ID
        /// </summary>
        /// <param name="worldPosition">World position</param>
        /// <returns>Bounds center block ID</returns>
        private BlockID GetBoundsCenterBlockID(Vector3 worldPosition) => ((WorldTransformController == null) ? BlockID.Zero : WorldTransformController.GetBlockID(worldPosition + bounds.center));

        /// <summary>
        /// Get bounds center chunk ID
        /// </summary>
        /// <param name="worldPosition">World position</param>
        /// <returns>Bounds center chunk ID</returns>
        private ChunkID GetBoundsCenterChunkID(Vector3 worldPosition)
        {
            WorldManagerScript world_manager = WorldManagerScript.Instance;
            return (world_manager ? world_manager.GetChunkIDFromBlockID(GetBoundsCenterBlockID(worldPosition)) : ChunkID.Zero);
        }

        /// <summary>
        /// Test collision
        /// </summary>
        /// <param name="worldPosition">World position</param>
        /// <returns>"true" if colliding, otherwise "false"</returns>
        public bool TestCollision(Vector3 worldPosition)
        {
            bool ret = false;
            WorldManagerScript world_manager = WorldManagerScript.Instance;
            if (world_manager)
            {
                ChunkID bounds_center_chunk_id = GetBoundsCenterChunkID(worldPosition);
                Vector3Int bounds_chunk_blocks_size = BoundsChunkBlocksSize;
                if ((lastBoundsChunkID != bounds_center_chunk_id) || (lastChunkBlocksSize != bounds_chunk_blocks_size))
                {
                    lastBoundsChunkID = bounds_center_chunk_id;
                    lastChunkBlocksSize = bounds_chunk_blocks_size;
                    UpdateChunkBlocksTask(bounds_center_chunk_id, bounds_chunk_blocks_size, world_manager);
                }
                if (chunkBlocksTasks != null)
                {
                    if (initializeChunkBlocksTasks)
                    {
                        initializeChunkBlocksTasks = false;
                        for (int index = 0; index < chunkBlocksTasks.Length; index++)
                        {
                            Task<BlockData[]> chunk_blocks_task = chunkBlocksTasks[index];
                            if (chunk_blocks_task.IsCompleted)
                            {
                                chunkBlocks[index] = chunk_blocks_task.Result;
                            }
                            else
                            {
                                initializeChunkBlocksTasks = true;
                                ret = true;
                                break;
                            }
                        }
                    }
                    if (!ret)
                    {
                        Vector3Int chunk_size = world_manager.ChunkSize;
                        Vector3 bounds_center = bounds.center;
                        Vector3 bounds_size = bounds.size;
                        Vector3Int check_blocks_size = new Vector3Int((((Mathf.FloorToInt(bounds_size.x) + 1) / 2) * 2) + 3, (((Mathf.FloorToInt(bounds_size.y) + 1) / 2) * 2) + 3, (((Mathf.FloorToInt(bounds_size.z) + 1) / 2) * 2) + 3);
                        BlockID bounds_center_block_id = GetBoundsCenterBlockID(worldPosition);
                        BlockID local_space_bounds_center_block_id = new BlockID(bounds_center_block_id.X - (bounds_center_chunk_id.X * chunk_size.x) + ((bounds_chunk_blocks_size.x / 2) * chunk_size.x), bounds_center_block_id.Y - (bounds_center_chunk_id.Y * chunk_size.y) + ((bounds_chunk_blocks_size.y / 2) * chunk_size.y), bounds_center_block_id.Z - (bounds_center_chunk_id.Z * chunk_size.z) + ((bounds_chunk_blocks_size.z / 2) * chunk_size.z));
                        Bounds check_space_bounds = new Bounds(new Vector3(Mathf.Repeat(worldPosition.x + bounds_center.x, 1.0f - float.Epsilon) - 0.5f + (check_blocks_size.x / 2), Mathf.Repeat(worldPosition.y + bounds_center.y, 1.0f - float.Epsilon) - 0.5f + (check_blocks_size.y / 2), Mathf.Repeat(worldPosition.z + bounds_center.z, 1.0f - float.Epsilon) - 0.5f + (check_blocks_size.z / 2)), bounds_size);
                        //Parallel.For(0, check_blocks_size.x * check_blocks_size.y * check_blocks_size.z, (index, parallelLoopState) =>
                        for (int index = 0, length = check_blocks_size.x * check_blocks_size.y * check_blocks_size.z; index < length; index++)
                        {
                            Vector3Int check_space_block_position = new Vector3Int(index % check_blocks_size.x, (index / check_blocks_size.x) % check_blocks_size.y, index / (check_blocks_size.x * check_blocks_size.y));
                            BlockID local_space_block_id = new BlockID(check_space_block_position.x - (check_blocks_size.x / 2) + local_space_bounds_center_block_id.X, check_space_block_position.y - (check_blocks_size.y / 2) + local_space_bounds_center_block_id.Y, check_space_block_position.z - (check_blocks_size.z / 2) + local_space_bounds_center_block_id.Z);
                            ChunkID local_space_chunk_id = world_manager.GetChunkIDFromBlockID(local_space_block_id);
                            Vector3Int block_chunk_position = new Vector3Int((int)(local_space_block_id.X % chunk_size.x), (int)(local_space_block_id.Y % chunk_size.y), (int)(local_space_block_id.Z % chunk_size.z));
                            BlockData[] chunk_blocks = chunkBlocks[local_space_chunk_id.X + (local_space_chunk_id.Y * bounds_chunk_blocks_size.x) + (local_space_chunk_id.Z * bounds_chunk_blocks_size.x * bounds_chunk_blocks_size.y)];
                            if (chunk_blocks == null)
                            {
                                ret = true;
                                break;
                            }
                            else
                            {
                                BlockData block = chunk_blocks[block_chunk_position.x + (block_chunk_position.y * chunk_size.x) + (block_chunk_position.z * chunk_size.x * chunk_size.y)];
                                if (block.IsABlock)
                                {
                                    foreach (Bounds collision_bounds in block.Block.CollisionBounds)
                                    {
                                        if (check_space_bounds.Intersects(new Bounds(check_space_block_position + collision_bounds.center, collision_bounds.size)))
                                        {
                                            ret = true;
                                            break;
                                        }
                                    }
                                    if (ret)
                                    {
                                        //parallelLoopState.Break();
                                        break;
                                    }
                                }
                            }
                        }//);
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Move
        /// </summary>
        /// <param name="motion">Motion</param>
        /// <returns>"true" if collision happened, otherwise "false"</returns>
        public bool Move(Vector3 motion)
        {
            bool ret;
            Vector3 delta_motion = motion;
            Vector3 position = transform.position;
            Vector3 new_position;
            uint count = 0U;
            float sample_collision_distance_multiplier = SampleCollisionDistanceMultiplier;
            do
            {
                new_position = position + delta_motion;
                ret = TestCollision(new_position);
                if (ret)
                {
                    if ((Mathf.Abs(delta_motion.x) > float.Epsilon) || (Mathf.Abs(delta_motion.z) > float.Epsilon))
                    {
                        new_position = new Vector3(position.x + delta_motion.x, position.y, position.z + delta_motion.z);
                        ret = TestCollision(new_position);
                    }
                    if (ret)
                    {
                        if ((Mathf.Abs(delta_motion.x) > float.Epsilon) || (Mathf.Abs(delta_motion.y) > float.Epsilon))
                        {
                            new_position = new Vector3(position.x + delta_motion.x, position.y + delta_motion.y, position.z);
                            ret = TestCollision(new_position);
                        }
                        if (ret)
                        {
                            if ((Mathf.Abs(delta_motion.y) > float.Epsilon) || (Mathf.Abs(delta_motion.z) > float.Epsilon))
                            {
                                new_position = new Vector3(position.x, position.y + delta_motion.y, position.z + delta_motion.z);
                                ret = TestCollision(new_position);
                            }
                            if (ret)
                            {
                                if (Mathf.Abs(delta_motion.x) > float.Epsilon)
                                {
                                    new_position = new Vector3(position.x + delta_motion.x, position.y, position.z);
                                    ret = TestCollision(new_position);
                                }
                                if (ret)
                                {
                                    if (Mathf.Abs(delta_motion.z) > float.Epsilon)
                                    {
                                        new_position = new Vector3(position.x, position.y, position.z + delta_motion.z);
                                        ret = TestCollision(new_position);
                                    }
                                    if (ret)
                                    {
                                        if (Mathf.Abs(delta_motion.y) > float.Epsilon)
                                        {
                                            new_position = new Vector3(position.x, position.y + delta_motion.y, position.z);
                                            ret = TestCollision(new_position);
                                        }
                                        if (ret)
                                        {
                                            delta_motion *= sample_collision_distance_multiplier;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (count >= maximalSampleCollisionCount)
                {
                    new_position = position;
                    break;
                }
                ++count;
            }
            while (ret);
            if (!ret)
            {
                transform.position = new_position;
            }
            return ret;
        }

        /// <summary>
        /// Start
        /// </summary>
        protected virtual void Start()
        {
            WorldTransformController = GetComponent<WorldTransformControllerScript>();
        }

        /// <summary>
        /// On draw gizmos
        /// </summary>
        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(bounds.center + transform.position, bounds.size);
        }
    }
}

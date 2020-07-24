using Akasha.Managers;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// World transform controller script class
    /// </summary>
    public class WorldTransformControllerScript : MonoBehaviour, IWorldTransformController
    {
        /// <summary>
        /// Chunk ID
        /// </summary>
        [SerializeField]
        private ChunkID chunkID = ChunkID.Zero;

        /// <summary>
        /// Last follow chunk ID
        /// </summary>
        private ChunkID lastFollowChunkID;

        /// <summary>
        /// Grid position
        /// </summary>
        public ChunkID ChunkID
        {
            get => chunkID;
            set => chunkID = value;
        }

        /// <summary>
        /// Block ID
        /// </summary>
        public BlockID BlockID
        {
            get => GetBlockID(transform.position);
            set
            {
                WorldManagerScript world_manager = WorldManagerScript.Instance;
                if (world_manager != null)
                {
                    Vector3Int chunk_size = world_manager.ChunkSize;
                    chunkID = new ChunkID((int)(value.X / chunk_size.x), (int)(value.Y / chunk_size.y), (int)(value.Z / chunk_size.z));
                    if (world_manager.FollowTransformController == this)
                    {
                        transform.position = new Vector3((int)(value.X % chunk_size.x) + 0.5f, (int)(value.Y % chunk_size.y) + 0.5f, (int)(value.Z % chunk_size.z) + 0.5f);
                    }
                    else
                    {
                        ChunkID delta_chunk_id = chunkID - world_manager.FollowTransformController.chunkID;
                        transform.position = new Vector3((int)(value.X % chunk_size.x) + 0.5f + (delta_chunk_id.X * chunk_size.x), (int)(value.Y % chunk_size.y) + 0.5f + (delta_chunk_id.Y * chunk_size.y), (int)(value.Z % chunk_size.z) + 0.5f + (delta_chunk_id.Z * chunk_size.z));
                    }
                }
            }
        }

        /// <summary>
        /// Get block ID
        /// </summary>
        /// <param name="worldPosition">World position</param>
        /// <returns>Block ID</returns>
        public BlockID GetBlockID(Vector3 worldPosition)
        {
            BlockID ret = BlockID.Zero;
            WorldManagerScript world_manager = WorldManagerScript.Instance;
            if (world_manager)
            {
                Vector3Int chunk_size = world_manager.ChunkSize;
                if (world_manager.FollowTransformController == this)
                {
                    ret = new BlockID(Mathf.RoundToInt(worldPosition.x - 0.5f) + (chunkID.X * chunk_size.x), Mathf.RoundToInt(worldPosition.y - 0.5f) + (chunkID.Y * chunk_size.y), Mathf.RoundToInt(worldPosition.z - 0.5f) + (chunkID.Z * chunk_size.z));
                }
                else
                {
                    ChunkID delta_chunk_id = chunkID - world_manager.FollowTransformController.chunkID;
                    ret = new BlockID(Mathf.RoundToInt(worldPosition.x - 0.5f) + (delta_chunk_id.X * chunk_size.x), Mathf.RoundToInt(worldPosition.y - 0.5f) + (delta_chunk_id.Y * chunk_size.y), Mathf.RoundToInt(worldPosition.z - 0.5f) + (delta_chunk_id.Z * chunk_size.z));
                }
            }
            return ret;
        }

        /// <summary>
        /// On enable
        /// </summary>
        private void OnEnable()
        {
            RenderPipelineManager.endFrameRendering += OnEndFrameRendering;
        }

        /// <summary>
        /// On disable
        /// </summary>
        private void OnDisable()
        {
            RenderPipelineManager.endFrameRendering -= OnEndFrameRendering;
        }

        /// <summary>
        /// On end Frame rendering
        /// </summary>
        private void OnEndFrameRendering(ScriptableRenderContext scriptableRenderContext, Camera[] cameras)
        {
            WorldManagerScript world_manager = WorldManagerScript.Instance;
            if (world_manager)
            {
                Vector3 chunk_size = world_manager.ChunkSize;
                Vector3 position = transform.position;
                if (world_manager.FollowTransformController == this)
                {
                    if ((position.x < 0.0f) || (position.x >= chunk_size.x))
                    {
                        chunkID.X += Mathf.FloorToInt(position.x / chunk_size.x);
                    }
                    if ((position.y < 0.0f) || (position.y >= chunk_size.y))
                    {
                        chunkID.Y += Mathf.FloorToInt(position.y / chunk_size.y);
                    }
                    if ((position.z < 0.0f) || (position.z >= chunk_size.z))
                    {
                        chunkID.Z += Mathf.FloorToInt(position.z / chunk_size.z);
                    }
                    transform.position = new Vector3
                    (
                        Mathf.Repeat(position.x, chunk_size.x - float.Epsilon),
                        Mathf.Repeat(position.y, chunk_size.y - float.Epsilon),
                        Mathf.Repeat(position.z, chunk_size.z - float.Epsilon)
                    );
                }
                else
                {
                    if (lastFollowChunkID != world_manager.FollowTransformController.chunkID)
                    {
                        ChunkID delta_chunk_id = world_manager.FollowTransformController.chunkID - lastFollowChunkID;
                        position.x += delta_chunk_id.X * chunk_size.x;
                        position.y += delta_chunk_id.Y * chunk_size.y;
                        position.z += delta_chunk_id.Z * chunk_size.z;
                        lastFollowChunkID = world_manager.FollowTransformController.chunkID;
                    }
                    ChunkID offset_chunk_id = chunkID - world_manager.FollowTransformController.chunkID;
                    Vector3 local_position = new Vector3(position.x - (offset_chunk_id.X * chunk_size.x), position.y - (offset_chunk_id.Y * chunk_size.y), position.z - (offset_chunk_id.Z * chunk_size.z));
                    if ((local_position.x < 0.0f) || (local_position.x >= chunk_size.x))
                    {
                        chunkID.X += Mathf.FloorToInt(local_position.x / chunk_size.x);
                    }
                    if ((local_position.y < 0.0f) || (local_position.y >= chunk_size.y))
                    {
                        chunkID.Y += Mathf.FloorToInt(local_position.y / chunk_size.y);
                    }
                    if ((local_position.z < 0.0f) || (local_position.z >= chunk_size.z))
                    {
                        chunkID.Z += Mathf.FloorToInt(local_position.z / chunk_size.z);
                    }
                    offset_chunk_id = chunkID - world_manager.FollowTransformController.chunkID;
                    transform.position = new Vector3
                    (
                        Mathf.Repeat(local_position.x, chunk_size.x - float.Epsilon) + (offset_chunk_id.X * chunk_size.x),
                        Mathf.Repeat(local_position.y, chunk_size.x - float.Epsilon) + (offset_chunk_id.X * chunk_size.x),
                        Mathf.Repeat(local_position.z, chunk_size.x - float.Epsilon) + (offset_chunk_id.X * chunk_size.x)
                    );
                }
            }
        }
    }
}

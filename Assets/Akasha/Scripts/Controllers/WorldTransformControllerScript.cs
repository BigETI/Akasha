using Akasha.Managers;
using UnityEngine;

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
        /// Grid position
        /// </summary>
        public ChunkID ChunkID
        {
            get => chunkID;
            set => chunkID = value;
        }

        /// <summary>
        /// Character controller
        /// </summary>
        public CharacterController CharacterController { get; private set; }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            CharacterController = GetComponent<CharacterController>();
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            WorldManagerScript world_manager = WorldManagerScript.Instance;
            if (world_manager)
            {
                if (world_manager.GameCameraWorldTransformController == this)
                {
                    Vector3 position = transform.position;
                    if ((position.x < 0.0f) || (position.x >= world_manager.ChunkSize.x))
                    {
                        chunkID.X += Mathf.FloorToInt(position.x / world_manager.ChunkSize.x);
                    }
                    if ((position.y < 0.0f) || (position.y >= world_manager.ChunkSize.y))
                    {
                        chunkID.Y += Mathf.FloorToInt(position.y / world_manager.ChunkSize.y);
                    }
                    if ((position.z < 0.0f) || (position.z >= world_manager.ChunkSize.z))
                    {
                        chunkID.Z += Mathf.FloorToInt(position.z / world_manager.ChunkSize.z);
                    }
                    position.x = Mathf.Repeat(position.x, world_manager.ChunkSize.x - float.Epsilon);
                    position.y = Mathf.Repeat(position.y, world_manager.ChunkSize.y - float.Epsilon);
                    position.z = Mathf.Repeat(position.z, world_manager.ChunkSize.z - float.Epsilon);
                    if (CharacterController)
                    {
                        CharacterController.enabled = false;
                        transform.position = position;
                        CharacterController.enabled = true;
                    }
                    else
                    {
                        transform.position = position;
                    }
                }
                else
                {
                    //ChunkID new_chunk_id = 
                }
            }
        }
    }
}

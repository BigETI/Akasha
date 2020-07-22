using Akasha.Managers;
using System;
using UnityEngine;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Block placement indicator controller script class
    /// </summary>
    public class BlockPlacementIndicatorControllerScript : MonoBehaviour, IBlockPlacementIndicatorController
    {
        /// <summary>
        /// Origin game object
        /// </summary>
        [SerializeField]
        private GameObject originGameObject = default;

        /// <summary>
        /// Raycast hits
        /// </summary>
        private RaycastHit[] raycastHits = Array.Empty<RaycastHit>();

        /// <summary>
        /// Origin game object
        /// </summary>
        public GameObject OriginGameObject
        {
            get => originGameObject;
            set => originGameObject = value;
        }

        /// <summary>
        /// Game camera
        /// </summary>
        public Camera GameCamera { get; private set; }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            GameCamera = FindObjectOfType<Camera>();
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            Vector3? position = null;
            if (GameCamera)
            {
                WorldManagerScript world_manager = WorldManagerScript.Instance;
                if (world_manager)
                {
                    int raycast_hit_count = PhysicsUtils.Raycast(GameCamera.transform.position, GameCamera.transform.forward, 20.0f, ref raycastHits);
                    if (raycast_hit_count > 0)
                    {
                        Vector3Int chunk_size = world_manager.ChunkSize;
                        float distance = float.PositiveInfinity;
                        for (int index = 0; index < raycast_hit_count; index++)
                        {
                            RaycastHit raycast_hit = raycastHits[index];
                            if ((raycast_hit.distance < distance) && !(raycast_hit.collider.isTrigger) && (raycast_hit.collider.transform.parent != null))
                            {
                                ChunkControllerScript chunk_controller = raycast_hit.collider.GetComponentInParent<ChunkControllerScript>();
                                if (chunk_controller != null)
                                {
                                    position = raycast_hit.collider.transform.position + new Vector3(Mathf.Round(raycast_hit.normal.x), Mathf.Round(raycast_hit.normal.y), Mathf.Round(raycast_hit.normal.z));
                                    distance = raycast_hit.distance;
                                }
                            }
                        }
                    }
                }
            }
            if (position == null)
            {
                if (originGameObject/* && originGameObject.activeSelf*/)
                {
                    originGameObject.SetActive(false);
                }
            }
            else
            {
                transform.position = position.Value;
                if (originGameObject/* && !(originGameObject.activeSelf)*/)
                {
                    originGameObject.SetActive(true);
                }
            }
        }
    }
}

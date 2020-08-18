using UnityEngine;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Third person view controller script class
    /// </summary>
    [ExecuteInEditMode]
    public class ThirdPersonViewControllerScript : MonoBehaviour, IThirdPersonViewController
    {
        /// <summary>
        /// Stiffness
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float stiffness = 0.5f;

        /// <summary>
        /// Offset
        /// </summary>
        [SerializeField]
        private Vector3 offset;

        /// <summary>
        /// Offset
        /// </summary>
        [SerializeField]
        private Vector3 virtualCameraOffset;

        /// <summary>
        /// Virtual camera radius
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float virtualCameraRadius = 0.125f;

        /// <summary>
        /// Virtual camera transform
        /// </summary>
        [SerializeField]
        private Transform virtualCameraTransform;

        /// <summary>
        /// Follow transform
        /// </summary>
        [SerializeField]
        private CharacterControllerScript followCharacterController;

        /// <summary>
        /// Raycast hits
        /// </summary>
        private RaycastHit[] raycastHits = new RaycastHit[128];

        /// <summary>
        /// Stiffness
        /// </summary>
        public float Stiffness
        {
            get => Mathf.Clamp(stiffness, 0.0f, 1.0f);
            set => stiffness = Mathf.Clamp(value, 0.0f, 1.0f);
        }

        /// <summary>
        /// Offset
        /// </summary>
        public Vector3 Offset
        {
            get => offset;
            set => offset = value;
        }

        /// <summary>
        /// Virtual camera offset
        /// </summary>
        public Vector3 VirtualCameraOffset
        {
            get => virtualCameraOffset;
            set => virtualCameraOffset = value;
        }

        /// <summary>
        /// Virtual camera radius
        /// </summary>
        public float VirtualCameraRadius
        {
            get => Mathf.Max(virtualCameraRadius, 0.0f);
            set => virtualCameraRadius = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Virtual camera transform
        /// </summary>
        public Transform VirtualCameraTransform
        {
            get => virtualCameraTransform;
            set => virtualCameraTransform = value;
        }

        /// <summary>
        /// Follow character controller
        /// </summary>
        public CharacterControllerScript FollowCharacterController
        {
            get => followCharacterController;
            set => followCharacterController = value;
        }

        /// <summary>
        /// Late update
        /// </summary>
        private void FixedUpdate()
        {
            if (followCharacterController != null)
            {
                transform.position = Vector3.Lerp(transform.position, followCharacterController.transform.position + offset, Stiffness);
                transform.rotation = Quaternion.Euler(followCharacterController.Rotation.x, followCharacterController.Rotation.y, 0.0f);
            }
            if (virtualCameraTransform != null)
            {
                float virtual_camera_distance_squared = virtualCameraOffset.sqrMagnitude;
                virtualCameraTransform.localPosition = virtualCameraOffset;
                if (virtual_camera_distance_squared > float.Epsilon)
                {
                    float virtual_camera_distance = Mathf.Sqrt(virtual_camera_distance_squared);
                    Vector3 direction = (virtualCameraTransform.transform.position - transform.position) / virtual_camera_distance;
                    int raycast_hits_count = PhysicsUtils.SphereCast(transform.position, VirtualCameraRadius, direction, virtual_camera_distance, ref raycastHits);
                    float nearest_distance = virtual_camera_distance;
                    for (int raycast_hits_index = 0; raycast_hits_index < raycast_hits_count; raycast_hits_index++)
                    {
                        RaycastHit raycast_hit = raycastHits[raycast_hits_index];
                        bool success = true;
                        GameObject game_object = raycast_hit.collider.gameObject;
                        while (game_object != null)
                        {
                            if ((game_object == gameObject) || (game_object == followCharacterController.gameObject))
                            {
                                success = false;
                                break;
                            }
                            else if (game_object.transform.parent == null)
                            {
                                break;
                            }
                            game_object = game_object.transform.parent.gameObject;
                        }
                        if (success)
                        {
                            if (nearest_distance > raycast_hit.distance)
                            {
                                nearest_distance = raycast_hit.distance;
                            }
                        }
                    }
                    virtualCameraTransform.localPosition = virtualCameraOffset.normalized * nearest_distance;
                }
            }
        }
    }
}

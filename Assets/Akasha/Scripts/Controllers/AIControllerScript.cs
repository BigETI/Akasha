using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// AI controller script class
    /// </summary>
    [RequireComponent(typeof(CharacterControllerScript))]
    public class AIControllerScript : MonoBehaviour, IAIController
    {
        /// <summary>
        /// Number of drawn gizmo rays
        /// </summary>
        private static readonly uint numberOfDrawnGizmoRays = 8U;

        /// <summary>
        /// Visibility angle
        /// </summary>
        [SerializeField]
        [Range(float.Epsilon, 360.0f)]
        private float visibilitiyAngle = 30.0f;

        /// <summary>
        /// Visibility distance
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float visibilityDistance = 10.0f;

        /// <summary>
        /// Detected player characters
        /// </summary>
        private readonly Dictionary<int, IPlayerCharacterDetectionState> detectedPlayerCharacters = new Dictionary<int, IPlayerCharacterDetectionState>();

        /// <summary>
        /// Delete detected player character IDs
        /// </summary>
        private readonly HashSet<int> deleteDetectedPlayerCharacterIDs = new HashSet<int>();

        /// <summary>
        /// Visibility angle
        /// </summary>
        public float VisibilityAngle
        {
            get => Mathf.Clamp(visibilitiyAngle, float.Epsilon, 360.0f);
            set => visibilitiyAngle = Mathf.Clamp(value, float.Epsilon, 360.0f);
        }

        /// <summary>
        /// Visibility distance
        /// </summary>
        public float VisibilityDistance
        {
            get => Mathf.Max(visibilityDistance, 0.0f);
            set => visibilityDistance = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Detected player characters
        /// </summary>
        public IReadOnlyDictionary<int, IPlayerCharacterDetectionState> DetectedPlayerCharacters => detectedPlayerCharacters;

        /// <summary>
        /// Character controller
        /// </summary>
        public ICharacterController CharacterController { get; private set; }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            CharacterController = GetComponent<CharacterControllerScript>();
        }

        /// <summary>
        /// Update
        /// </summary>
        private void FixedUpdate()
        {
            if ((CharacterController != null) && CharacterController.EyesTransform)
            {
                float visibility_distance = VisibilityDistance;
                float visibility_distance_squared = visibility_distance * visibility_distance;
                float visibility_angle = VisibilityAngle;
                Vector3 eyes_forward = CharacterController.EyesTransform.forward;
                deleteDetectedPlayerCharacterIDs.Clear();
                foreach (PlayerCharacterDetectionState player_character_detection_state in detectedPlayerCharacters.Values)
                {
                    deleteDetectedPlayerCharacterIDs.Add(player_character_detection_state.CharacterController.gameObject.GetInstanceID());
                }
                foreach (PlayerControllerScript player_controller in PlayerControllerScript.PlayerControllers)
                {
                    if (player_controller.CharacterController != null)
                    {
                        int id = player_controller.gameObject.GetInstanceID();
                        bool is_seeing = false;
                        deleteDetectedPlayerCharacterIDs.Remove(id);
                        if (player_controller.CharacterController.IsAlive)
                        {
                            Vector3 delta = player_controller.transform.position - CharacterController.EyesTransform.position;
                            if (delta.sqrMagnitude <= float.Epsilon)
                            {
                                is_seeing = true;
                            }
                            else if ((delta.sqrMagnitude < visibility_distance_squared) && (Vector3.Angle(eyes_forward, delta.normalized) < visibility_angle))
                            {
                                is_seeing = true;
                            }
                        }
                        if (is_seeing)
                        {
                            IPlayerCharacterDetectionState player_character_detection_state;
                            if (detectedPlayerCharacters.ContainsKey(id))
                            {
                                player_character_detection_state = detectedPlayerCharacters[id];
                                player_character_detection_state.DetectionState = EPlayerCharacterDetectionState.Seeing;
                                player_character_detection_state.LastSeenPosition = player_controller.transform.position;
                            }
                            else
                            {
                                player_character_detection_state = new PlayerCharacterDetectionState(player_controller.CharacterController, player_controller.transform.position, EPlayerCharacterDetectionState.Seeing);
                                detectedPlayerCharacters.Add(id, player_character_detection_state);
                            }
                        }
                        else
                        {
                            if (detectedPlayerCharacters.ContainsKey(id))
                            {
                                IPlayerCharacterDetectionState player_character_detection_state = detectedPlayerCharacters[id];
                                if (player_character_detection_state.DetectionState == EPlayerCharacterDetectionState.Seeing)
                                {
                                    player_character_detection_state.DetectionState = EPlayerCharacterDetectionState.Lost;
                                }
                            }
                        }
                    }
                }
                foreach (int delete_detected_player_character_id in deleteDetectedPlayerCharacterIDs)
                {
                    detectedPlayerCharacters.Remove(delete_detected_player_character_id);
                }
                //foreach (PlayerCharacterDetectionState player_character_detection_state in detectedPlayerCharacters.Values)
                //{
                //    // TODO
                //}
            }
        }

        /// <summary>
        /// On draw gizmos
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            if (CharacterController == null)
            {
                CharacterController = GetComponent<CharacterControllerScript>();
            }
            if ((CharacterController != null) && CharacterController.EyesTransform)
            {
                Vector3 eyes_position = CharacterController.EyesTransform.position;
                Vector3 eyes_forward = CharacterController.EyesTransform.forward;
                Vector3 eyes_right = CharacterController.EyesTransform.right;
                Quaternion visibility_angle_quaternion = Quaternion.AngleAxis(VisibilityAngle, eyes_right);
                float visibility_distance = VisibilityDistance;
                Gizmos.color = Color.red;
                for (uint i = 0; i != numberOfDrawnGizmoRays; i++)
                {
                    Gizmos.DrawRay(eyes_position, Quaternion.AngleAxis((360.0f * i) / numberOfDrawnGizmoRays, eyes_forward) * (visibility_angle_quaternion * (eyes_forward * visibility_distance)));
                }
                Gizmos.color = Color.cyan;
                Gizmos.DrawRay(eyes_position, eyes_forward * visibility_distance);
            }
        }
    }
}

using Akasha.InputActions;
using Akasha.Managers;
using System;
using UnityEngine;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Debug ccamera controller script
    /// </summary>
    public class DebugCameraControllerScript : MonoBehaviour, IDebugCameraController
    {
        /// <summary>
        /// Maximal speed
        /// </summary>
        [SerializeField]
        [Range(0.0f, 10000.0f)]
        private float maximalSpeed = 5.0f;

        /// <summary>
        /// Maximal acceleration
        /// </summary>
        [SerializeField]
        [Range(0.0f, 10000.0f)]
        private float maximalAcceleration = 50.0f;

        /// <summary>
        /// Sprint multiplicator
        /// </summary>
        [SerializeField]
        [Range(1.0f, 100.0f)]
        private float sprintMultiplicator = 4.0f;

        /// <summary>
        /// Minimal horizontal rotation
        /// </summary>
        [SerializeField]
        [Range(-90.0f, 0.0f)]
        private float minimalHorizontalRotation = -90.0f;

        /// <summary>
        /// Maximal horizontal rotation
        /// </summary>
        [SerializeField]
        [Range(0.0f, 90.0f)]
        private float maximalHorizontalRotation = 90.0f;

        /// <summary>
        /// FPS sample time
        /// </summary>
        [SerializeField]
        [Range(float.Epsilon + float.Epsilon, 10.0f)]
        private float fpsSampleTime = 0.125f;

        /// <summary>
        /// FPS label rectangle
        /// </summary>
        [SerializeField]
        private Rect fpsLabelRectangle = new Rect(40.0f, 40.0f, 640.0f, 480.0f);

        /// <summary>
        /// Movement
        /// </summary>
        private Vector2 movement = Vector2.zero;

        /// <summary>
        /// Delta rotation
        /// </summary>
        private Vector2 deltaRotation = Vector2.zero;

        /// <summary>
        /// Rotation
        /// </summary>
        private Vector2 rotation;

        /// <summary>
        /// Frames time
        /// </summary>
        private float framesTime;

        /// <summary>
        /// Frame count
        /// </summary>
        private uint frameCount;

        /// <summary>
        /// FPS label string
        /// </summary>
        private string fpsLabelString = string.Empty;

        /// <summary>
        /// Maximal speed
        /// </summary>
        public float MaximalSpeed
        {
            get => Mathf.Max(maximalSpeed, 0.0f);
            set => maximalSpeed = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Maximal acceleration
        /// </summary>
        public float MaximalAcceleration
        {
            get => Mathf.Max(maximalAcceleration, 0.0f);
            set => maximalAcceleration = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Sprint multiplicator
        /// </summary>
        public float SprintMultiplicator
        {
            get => Mathf.Max(sprintMultiplicator, 1.0f);
            set => sprintMultiplicator = Mathf.Max(value, 1.0f);
        }

        /// <summary>
        /// Movement
        /// </summary>
        public Vector3 Movement
        {
            get => movement;
            set => movement = ((value.sqrMagnitude > 1.0f) ? value.normalized : value);
        }

        /// <summary>
        /// Minimal horizontal rotation
        /// </summary>
        public float MinimalHorizontalRotation
        {
            get => Mathf.Min(minimalHorizontalRotation, maximalHorizontalRotation);
            set => minimalHorizontalRotation = value;
        }

        /// <summary>
        /// Maximal horizontal rotation
        /// </summary>
        public float MaximalHorizontalRotation
        {
            get => Mathf.Max(minimalHorizontalRotation, maximalHorizontalRotation);
            set => maximalHorizontalRotation = value;
        }

        /// <summary>
        /// FPS sample time
        /// </summary>
        public float FPSSampleTime
        {
            get => Mathf.Max(fpsSampleTime, float.Epsilon + float.Epsilon);
            set => fpsSampleTime = Mathf.Max(value, float.Epsilon + float.Epsilon);
        }

        /// <summary>
        /// Rotation
        /// </summary>
        public Vector2 Rotation
        {
            get => rotation;
            set => rotation = new Vector2(Mathf.Clamp(value.x, MinimalHorizontalRotation, MaximalHorizontalRotation), Mathf.Repeat(value.y, 360.0f - float.Epsilon));
        }

        /// <summary>
        /// Velocity
        /// </summary>
        public Vector3 Velocity { get; private set; }

        /// <summary>
        /// Is sprinting
        /// </summary>
        public bool IsSprinting { get; private set; }

        /// <summary>
        /// Game input actions
        /// </summary>
        public GameInputActions GameInputActions { get; private set; }

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            GameInputActions = new GameInputActions();
            GameInputActions.GameActionMap.Look.performed += (context) =>
            {
                Vector2 input = context.ReadValue<Vector2>();
                ISaveGameData save_game_data = GameManager.SaveGameData;
                deltaRotation = new Vector2(save_game_data.InvertYAxis ? -(input.y) : input.y, save_game_data.InvertXAxis ? -(input.x) : input.x);
            };
            GameInputActions.GameActionMap.Move.performed += (context) => Movement = context.ReadValue<Vector2>();
            GameInputActions.GameActionMap.Sprint.performed += (context) => IsSprinting = context.ReadValueAsButton();
        }

        /// <summary>
        /// On enable
        /// </summary>
        private void OnEnable()
        {
            GameInputActions?.Enable();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        /// <summary>
        /// On disable
        /// </summary>
        private void OnDisable()
        {
            GameInputActions?.Disable();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            framesTime += Time.deltaTime;
            ++frameCount;
            if (framesTime >= FPSSampleTime)
            {
                fpsLabelString = $"{ frameCount / framesTime } FPS";
                framesTime = 0.0f;
                frameCount = 0U;
            }
            Quaternion old_rotation = transform.rotation;
            Rotation += deltaRotation * GameManager.SaveGameData.Sensitivity * Time.deltaTime;
            Quaternion new_rotation = Quaternion.Euler(Rotation.x, Rotation.y, 0.0f);
            transform.rotation = new_rotation;
            Velocity = (new_rotation * Quaternion.Inverse(old_rotation)) * Velocity;
            float sprint_multiplicator = (IsSprinting ? SprintMultiplicator : 1.0f);
            float maximal_speed = MaximalSpeed * Movement.magnitude * sprint_multiplicator;
            float maximal_acceleration = MaximalAcceleration * sprint_multiplicator;
            Vector3 world_movement = (transform.right * Movement.x) + (transform.forward * Movement.y);
            Vector3 acceleration = (world_movement.sqrMagnitude > float.Epsilon) ? (world_movement * maximal_acceleration) : ((Velocity.sqrMagnitude > float.Epsilon) ? (Velocity.normalized * -maximal_acceleration) : Vector3.zero);
            Velocity += acceleration * Time.deltaTime;
            if (Velocity.sqrMagnitude > (maximal_speed * maximal_speed))
            {
                Velocity = Velocity.normalized * maximal_speed;
            }
            transform.position += Velocity * Time.deltaTime;
        }

        /// <summary>
        /// On GUI
        /// </summary>
        private void OnGUI()
        {
            GUI.Label(fpsLabelRectangle, fpsLabelString);
        }
    }
}

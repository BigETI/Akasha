using UnityEngine;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Elevator controller script
    /// </summary>
    public class ElevatorControllerScript : MonoBehaviour, IElevatorController
    {
        /// <summary>
        /// Is active
        /// </summary>
        [SerializeField]
        private bool isActive;

        /// <summary>
        /// Elevator type
        /// </summary>
        [SerializeField]
        private EElevatorType elevatorType;

        /// <summary>
        /// Finish movement when inactive
        /// </summary>
        [SerializeField]
        private bool finishMovementWhenInactive;

        /// <summary>
        /// Target position
        /// </summary>
        [SerializeField]
        private Vector3 targetPosition;

        /// <summary>
        /// Use world space
        /// </summary>
        [SerializeField]
        private bool useWorldSpace;

        /// <summary>
        /// Movement time
        /// </summary>
        [SerializeField]
        private float movementTime;

        /// <summary>
        /// Pause time
        /// </summary>
        [SerializeField]
        private float pauseTime;

        /// <summary>
        /// Start position
        /// </summary>
        private Vector3 startPosition;

        /// <summary>
        /// Is start position initialized
        /// </summary>
        private bool isStartPositionInitialized;

        /// <summary>
        /// Is active
        /// </summary>
        public bool IsActive
        {
            get => isActive;
            set => isActive = value;
        }

        /// <summary>
        /// Elevator type
        /// </summary>
        public EElevatorType ElevatorType
        {
            get => elevatorType;
            set => elevatorType = value;
        }

        /// <summary>
        /// Finish movement when inactive
        /// </summary>
        public bool FinishMovementWhenInactive
        {
            get => finishMovementWhenInactive;
            set => finishMovementWhenInactive = value;
        }

        /// <summary>
        /// Target position
        /// </summary>
        public Vector3 TargetPosition
        {
            get => targetPosition;
            set => targetPosition = value;
        }

        /// <summary>
        /// Use world space
        /// </summary>
        public bool UseWorldSpace
        {
            get => useWorldSpace;
            set => useWorldSpace = value;
        }

        /// <summary>
        /// Movement time
        /// </summary>
        public float MovementTime
        {
            get => Mathf.Max(movementTime, 0.0f);
            set => movementTime = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Pause time
        /// </summary>
        public float PauseTime
        {
            get => Mathf.Max(pauseTime, 0.0f);
            set => pauseTime = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Start position
        /// </summary>
        public Vector3 StartPosition => (isStartPositionInitialized ? startPosition : transform.position);

        /// <summary>
        /// Elapsed movement time
        /// </summary>
        public float ElapsedMovementTime { get; private set; }

        /// <summary>
        /// Elapsed pause time
        /// </summary>
        public float ElapsedPauseTime { get; private set; }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            startPosition = transform.position;
            isStartPositionInitialized = true;
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            // TODO
        }

        /// <summary>
        /// On draw gizmos selected
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            Vector3 start_position = StartPosition;
            Vector3 target_position = (useWorldSpace ? targetPosition : start_position + targetPosition);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(start_position, target_position);
            Gizmos.DrawCube(start_position, Vector3.one * 0.125f);
            Gizmos.DrawSphere(target_position, 0.125f);
        }
    }
}

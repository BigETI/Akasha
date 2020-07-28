using Akasha.InputActions;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Player controller script class
    /// </summary>
    [RequireComponent(typeof(CharacterControllerScript))]
    public class PlayerControllerScript : MonoBehaviour, IPlayerController
    {
        /// <summary>
        /// Player controllers
        /// </summary>
        private static readonly List<PlayerControllerScript> playerControllers = new List<PlayerControllerScript>();

        /// <summary>
        /// View mode
        /// </summary>
        [SerializeField]
        private EViewMode viewMode = EViewMode.FirstPerson;

        /// <summary>
        /// First person view virtual camera game object
        /// </summary>
        [SerializeField]
        private GameObject firstPersonViewVirtualCameraGameObject;

        /// <summary>
        /// Third person view virtual camera game object
        /// </summary>
        [SerializeField]
        private GameObject thirdPersonViewVirtualCameraGameObject;

        /// <summary>
        /// Player controllers
        /// </summary>
        public static IReadOnlyList<PlayerControllerScript> PlayerControllers => playerControllers;

        /// <summary>
        /// View mode
        /// </summary>
        public EViewMode ViewMode
        {
            get => viewMode;
            set => viewMode = value;
        }

        /// <summary>
        /// First person view virtual camera game object
        /// </summary>
        public GameObject FirstPersonViewVirtualCameraGameObject
        {
            get => firstPersonViewVirtualCameraGameObject;
            set => firstPersonViewVirtualCameraGameObject = value;
        }

        /// <summary>
        /// Third person view virtual camera game object
        /// </summary>
        public GameObject ThirdPersonViewVirtualCameraGameObject
        {
            get => thirdPersonViewVirtualCameraGameObject;
            set => thirdPersonViewVirtualCameraGameObject = value;
        }

        /// <summary>
        /// Character controller
        /// </summary>
        public ICharacterController CharacterController { get; private set; }

        /// <summary>
        /// Game input actions
        /// </summary>
        public GameInputActions GameInputActions { get; private set; }

        /// <summary>
        /// Character movement
        /// </summary>
        public Vector2 CharacterMovement { get; private set; }

        /// <summary>
        /// Delta character rotation
        /// </summary>
        public Vector2 DeltaCharacterRotation { get; private set; }

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            GameInputActions = new GameInputActions();
            GameInputActions.GameActionMap.Interact.performed += (context) =>
            {
                if (CharacterController != null)
                {
                    CharacterController.Interact();
                }
            };
            GameInputActions.GameActionMap.Jump.performed += (context) =>
            {
                if (CharacterController != null)
                {
                    CharacterController.Jump();
                }
            };
            GameInputActions.GameActionMap.Look.performed += (context) =>
            {
                Vector2 input = context.ReadValue<Vector2>();
                ISaveGameData save_game_data = GameManager.SaveGameData;
                DeltaCharacterRotation = new Vector2(save_game_data.InvertYAxis ? -(input.y) : input.y, save_game_data.InvertXAxis ? -(input.x) : input.x);
            };
            GameInputActions.GameActionMap.Move.performed += (context) =>
            {
                if (CharacterController != null)
                {
                    CharacterController.Movement = context.ReadValue<Vector2>();
                }
            };
            GameInputActions.GameActionMap.Reload.performed += (context) =>
            {
                if (CharacterController != null)
                {
                    CharacterController.Reload();
                }
            };
            GameInputActions.GameActionMap.Shoot.performed += (context) =>
            {
                if (CharacterController != null)
                {
                    //CharacterController.Shoot();
                    CharacterController.HitBlock();
                }
            };
            GameInputActions.GameActionMap.Aim.performed += (context) =>
            {
                if (CharacterController != null)
                {
                    //CharacterController.Shoot();
                    CharacterController.PlaceBlock();
                }
            };
            GameInputActions.GameActionMap.Sneak.performed += (context) =>
            {
                if (CharacterController != null)
                {
                    CharacterController.RunningMode = (context.ReadValueAsButton() ? ERunningMode.Sneaking : ERunningMode.Walking);
                }
            };
            GameInputActions.GameActionMap.Sprint.performed += (context) =>
            {
                if (CharacterController != null)
                {
                    CharacterController.RunningMode = (context.ReadValueAsButton() ? ERunningMode.Sprinting : ERunningMode.Walking);
                }
            };
            GameInputActions.GameActionMap.SwitchView.performed += (context) =>
            {
                switch (viewMode)
                {
                    case EViewMode.FirstPerson:
                        viewMode = EViewMode.ThirdPerson;
                        break;
                    case EViewMode.ThirdPerson:
                        viewMode = EViewMode.FirstPerson;
                        break;
                }
            };
            GameInputActions.GameActionMap.SelectWeapon.performed += (context) =>
            {
                float input = context.ReadValue<float>();
                if (input <= -0.5f)
                {
                    CharacterController.SelectPreviousInventoryItemSlot();
                }
                else if (input >= 0.5f)
                {
                    CharacterController.SelectNextInventoryItemSlot();
                }
            };
            // TODO
        }

        /// <summary>
        /// On enable
        /// </summary>
        private void OnEnable()
        {
            GameInputActions?.Enable();
            playerControllers.Add(this);
        }

        /// <summary>
        /// On disable
        /// </summary>
        private void OnDisable()
        {
            GameInputActions?.Disable();
            playerControllers.Remove(this);
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start() => CharacterController = GetComponent<CharacterControllerScript>();

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (GameManager.GameState == EGameState.Playing)
            {
                if (CharacterController != null)
                {
                    CharacterController.Rotation += DeltaCharacterRotation * GameManager.SaveGameData.Sensitivity * Time.deltaTime;
                }
                if (firstPersonViewVirtualCameraGameObject != null)
                {
                    firstPersonViewVirtualCameraGameObject.SetActive(viewMode == EViewMode.FirstPerson);
                }
                if (thirdPersonViewVirtualCameraGameObject != null)
                {
                    thirdPersonViewVirtualCameraGameObject.SetActive(viewMode == EViewMode.ThirdPerson);
                }
            }
        }
    }
}

using Akasha.InputActions;
using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Input controller script class
    /// </summary>
    public class InputControllerScript : MonoBehaviour, IInputController
    {
        /// <summary>
        /// On any key pressed
        /// </summary>
        [SerializeField]
        private UnityEvent onAnyKeyPressed = default;

        /// <summary>
        /// On back key pressed
        /// </summary>
        [SerializeField]
        private UnityEvent onBackKeyPressed = default;

        /// <summary>
        /// On cancel key pressed
        /// </summary>
        [SerializeField]
        private UnityEvent onCancelKeyPressed = default;

        /// <summary>
        /// On forward key pressed
        /// </summary>
        [SerializeField]
        private UnityEvent onForwardKeyPressed = default;

        /// <summary>
        /// On menu key pressed
        /// </summary>
        [SerializeField]
        private UnityEvent onMenuKeyPressed = default;

        /// <summary>
        /// On modifier key pressed
        /// </summary>
        [SerializeField]
        private UnityEvent onModifierKeyPressed = default;

        /// <summary>
        /// On primary action key pressed
        /// </summary>
        [SerializeField]
        private UnityEvent onPrimaryActionKeyPressed = default;

        /// <summary>
        /// On primary trigger key pressed
        /// </summary>
        [SerializeField]
        private UnityEvent onPrimaryTriggerKeyPressed = default;

        /// <summary>
        /// On secondary action key pressed
        /// </summary>
        [SerializeField]
        private UnityEvent onSecondaryActionKeyPressed = default;

        /// <summary>
        /// On secondary trigger key pressed
        /// </summary>
        [SerializeField]
        private UnityEvent onSecondaryTriggerKeyPressed = default;

        /// <summary>
        /// On submit key pressed
        /// </summary>
        [SerializeField]
        private UnityEvent onSubmitKeyPressed = default;

        /// <summary>
        /// Game input actions
        /// </summary>
        public GameInputActions GameInputActions { get; private set; }

        /// <summary>
        /// On any key pressed
        /// </summary>
        public event Action OnAnyKeyPressed;

        /// <summary>
        /// On back key pressed
        /// </summary>
        public event Action OnBackKeyPressed;

        /// <summary>
        /// On cancel key pressed
        /// </summary>
        public event Action OnCancelKeyPressed;

        /// <summary>
        /// On forward key pressed
        /// </summary>
        public event Action OnForwardKeyPressed;

        /// <summary>
        /// On menu key pressed
        /// </summary>
        public event Action OnMenuKeyPressed;

        /// <summary>
        /// On modifier key pressed
        /// </summary>
        public event Action OnModifierKeyPressed;

        /// <summary>
        /// On primary action key pressed
        /// </summary>
        public event Action OnPrimaryActionKeyPressed;

        /// <summary>
        /// On primary trigger key pressed
        /// </summary>
        public event Action OnPrimaryTriggerKeyPressed;

        /// <summary>
        /// On secondary action key pressed
        /// </summary>
        public event Action OnSecondaryActionKeyPressed;

        /// <summary>
        /// On secondary trigger key pressed
        /// </summary>
        public event Action OnSecondaryTriggerKeyPressed;

        /// <summary>
        /// On submit key pressed
        /// </summary>
        public event Action OnSubmitKeyPressed;

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            GameInputActions = new GameInputActions();
            GameInputActions.UsagesActionMap.Any.performed += (context) =>
            {
                if (onAnyKeyPressed != null)
                {
                    onAnyKeyPressed.Invoke();
                }
                OnAnyKeyPressed?.Invoke();
            };
            GameInputActions.UsagesActionMap.Back.performed += (context) =>
            {
                if (onBackKeyPressed != null)
                {
                    onBackKeyPressed.Invoke();
                }
                OnBackKeyPressed?.Invoke();
            };
            GameInputActions.UsagesActionMap.Cancel.performed += (context) =>
            {
                if (onCancelKeyPressed != null)
                {
                    onCancelKeyPressed.Invoke();
                }
                OnCancelKeyPressed?.Invoke();
            };
            GameInputActions.UsagesActionMap.Forward.performed += (context) =>
            {
                if (onForwardKeyPressed != null)
                {
                    onForwardKeyPressed.Invoke();
                }
                OnForwardKeyPressed?.Invoke();
            };
            GameInputActions.UsagesActionMap.Menu.performed += (context) =>
            {
                if (onMenuKeyPressed != null)
                {
                    onMenuKeyPressed.Invoke();
                }
                OnMenuKeyPressed?.Invoke();
            };
            GameInputActions.UsagesActionMap.Modifier.performed += (context) =>
            {
                if (onModifierKeyPressed != null)
                {
                    onModifierKeyPressed.Invoke();
                }
                OnModifierKeyPressed?.Invoke();
            };
            GameInputActions.UsagesActionMap.PrimaryAction.performed += (context) =>
            {
                if (onPrimaryActionKeyPressed != null)
                {
                    onPrimaryActionKeyPressed.Invoke();
                }
                OnPrimaryActionKeyPressed?.Invoke();
            };
            GameInputActions.UsagesActionMap.PrimaryTrigger.performed += (context) =>
            {
                if (onPrimaryTriggerKeyPressed != null)
                {
                    onPrimaryTriggerKeyPressed.Invoke();
                }
                OnPrimaryTriggerKeyPressed?.Invoke();
            };
            GameInputActions.UsagesActionMap.SecondaryAction.performed += (context) =>
            {
                if (onSecondaryActionKeyPressed != null)
                {
                    onSecondaryActionKeyPressed.Invoke();
                }
                OnSecondaryActionKeyPressed?.Invoke();
            };
            GameInputActions.UsagesActionMap.SecondaryTrigger.performed += (context) =>
            {
                if (onSecondaryTriggerKeyPressed != null)
                {
                    onSecondaryTriggerKeyPressed.Invoke();
                }
                OnSecondaryTriggerKeyPressed?.Invoke();
            };
            GameInputActions.UsagesActionMap.Submit.performed += (context) =>
            {
                if (onSubmitKeyPressed != null)
                {
                    onSubmitKeyPressed.Invoke();
                }
                OnSubmitKeyPressed?.Invoke();
            };
        }

        /// <summary>
        /// On enable
        /// </summary>
        private void OnEnable() => GameInputActions?.Enable();

        /// <summary>
        /// On disable
        /// </summary>
        private void OnDisable() => GameInputActions?.Disable();
    }
}

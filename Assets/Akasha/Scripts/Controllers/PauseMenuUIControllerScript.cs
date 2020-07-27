using Akasha.InputActions;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnitySceneLoaderManager;

/// <summary>
/// AKasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Pause menu UI controller script class
    /// </summary>
    public class PauseMenuUIControllerScript : MonoBehaviour, IPauseMenuUIController
    {
        /// <summary>
        /// On main menu shown
        /// </summary>
        [SerializeField]
        private UnityEvent onMainMenuShown = default;

        /// <summary>
        /// On options shown
        /// </summary>
        [SerializeField]
        private UnityEvent onOptionsShown = default;

        /// <summary>
        /// Pause menu state
        /// </summary>
        private EPauseMenuState pauseMenuState;

        /// <summary>
        /// Pause menu state
        /// </summary>
        public EPauseMenuState PauseMenuState
        {
            get => pauseMenuState;
            set
            {
                if (pauseMenuState != value)
                {
                    switch (pauseMenuState)
                    {
                        case EPauseMenuState.ShowingPauseMenu:
                            if (value == EPauseMenuState.ShowingOptions)
                            {
                                pauseMenuState = EPauseMenuState.ShowingOptions;
                                if (onOptionsShown != null)
                                {
                                    onOptionsShown.Invoke();
                                }
                                OnOptionsShown?.Invoke();
                            }
                            break;
                        case EPauseMenuState.ShowingOptions:
                            if (value == EPauseMenuState.ShowingPauseMenu)
                            {
                                pauseMenuState = EPauseMenuState.ShowingPauseMenu;
                                if (onMainMenuShown != null)
                                {
                                    onMainMenuShown.Invoke();
                                }
                                OnMainMenuShown?.Invoke();
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Game input actions
        /// </summary>
        public GameInputActions GameInputActions { get; private set; }

        /// <summary>
        /// On main menu shown
        /// </summary>
        public event PauseMenuStateChangedDelegate OnMainMenuShown;

        /// <summary>
        /// On options shown
        /// </summary>
        public event PauseMenuStateChangedDelegate OnOptionsShown;

        /// <summary>
        /// Show pause menu
        /// </summary>
        public void ShowPauseMenu()
        {
            PauseMenuState = EPauseMenuState.ShowingPauseMenu;
            GameManager.GameState = EGameState.Paused;
        }

        /// <summary>
        /// Resume game
        /// </summary>
        public void ResumeGame()
        {
            PauseMenuState = EPauseMenuState.ShowingPauseMenu;
            GameManager.GameState = EGameState.Playing;
        }

        /// <summary>
        /// Show options
        /// </summary>
        public void ShowOptions()
        {
            PauseMenuState = EPauseMenuState.ShowingOptions;
        }

        /// <summary>
        /// Exit game
        /// </summary>
        public void ExitGame()
        {
            // TODO: Save world
            SceneLoaderManager.LoadScene("MainMenuScene");
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Awake()
        {
            GameInputActions = new GameInputActions();
            GameInputActions.GameActionMap.Pause.performed += (context) =>
            {
                switch (pauseMenuState)
                {
                    case EPauseMenuState.ShowingPauseMenu:
                        ((GameManager.GameState == EGameState.Paused) ? (Action)ResumeGame : ShowPauseMenu)();
                        break;
                    case EPauseMenuState.ShowingOptions:
                        ShowPauseMenu();
                        break;
                }
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

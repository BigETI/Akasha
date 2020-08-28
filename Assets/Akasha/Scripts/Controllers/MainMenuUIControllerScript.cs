using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Main menu controller script class
    /// </summary>
    [RequireComponent(typeof(InputControllerScript))]
    public class MainMenuUIControllerScript : MonoBehaviour, IMainMenuUIController
    {
        /// <summary>
        /// On menus hidden
        /// </summary>
        [SerializeField]
        private UnityEvent onMenusHidden = default;

        /// <summary>
        /// On main menu shown
        /// </summary>
        [SerializeField]
        private UnityEvent onMainMenuShown = default;

        /// <summary>
        /// On create world menu shown
        /// </summary>
        [SerializeField]
        private UnityEvent onCreateWorldMenuShown = default;

        /// <summary>
        /// On load worlds menu shown
        /// </summary>
        [SerializeField]
        private UnityEvent onLoadWorldsMenuShown = default;

        /// <summary>
        /// On options menu shown
        /// </summary>
        [SerializeField]
        private UnityEvent onOptionsMenuShown = default;

        /// <summary>
        /// On credits menu shown
        /// </summary>
        [SerializeField]
        private UnityEvent onCreditsMenuShown = default;

        /// <summary>
        /// Main menu state
        /// </summary>
        private EMainMenuState mainMenuState = EMainMenuState.Nothing;

        /// <summary>
        /// Main menu state
        /// </summary>
        public EMainMenuState MainMenuState
        {
            get => mainMenuState;
            set
            {
                if (mainMenuState != value)
                {
                    mainMenuState = value;
                    switch (value)
                    {
                        case EMainMenuState.Nothing:
                            if (onMenusHidden != null)
                            {
                                onMenusHidden.Invoke();
                            }
                            OnMenusHidden?.Invoke();
                            break;
                        case EMainMenuState.Main:
                            if (onMainMenuShown != null)
                            {
                                onMainMenuShown.Invoke();
                            }
                            OnMainMenuShown?.Invoke();
                            break;
                        case EMainMenuState.CreateWorld:
                            if (onCreateWorldMenuShown != null)
                            {
                                onCreateWorldMenuShown.Invoke();
                            }
                            OnCreateWorldMenuShown?.Invoke();
                            break;
                        case EMainMenuState.LoadWorlds:
                            if (onLoadWorldsMenuShown != null)
                            {
                                onLoadWorldsMenuShown.Invoke();
                            }
                            OnLoadWorldsMenuShown?.Invoke();
                            break;
                        case EMainMenuState.Options:
                            if (onOptionsMenuShown != null)
                            {
                                onOptionsMenuShown.Invoke();
                            }
                            OnOptionsMenuShown?.Invoke();
                            break;
                        case EMainMenuState.Credits:
                            if (onCreditsMenuShown != null)
                            {
                                onCreditsMenuShown.Invoke();
                            }
                            OnCreditsMenuShown?.Invoke();
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// On menus hidden
        /// </summary>
        public event MenusHiddenDelegate OnMenusHidden;

        /// <summary>
        /// On main menu shown
        /// </summary>
        public event MainMenuShownDelegate OnMainMenuShown;

        /// <summary>
        /// On main menu shown
        /// </summary>
        public event CreateWorldMenuShownDelegate OnCreateWorldMenuShown;

        /// <summary>
        /// On load worlds menu shown
        /// </summary>
        public event LoadWorldsMenuShownDelegate OnLoadWorldsMenuShown;

        /// <summary>
        /// On options menu shown
        /// </summary>
        public event OptionsMenuShownDelegate OnOptionsMenuShown;

        /// <summary>
        /// Credits menu shown
        /// </summary>
        public event CreditsMenuShownDelegate OnCreditsMenuShown;

        /// <summary>
        /// Hide menus
        /// </summary>
        public void HideMenus() => MainMenuState = EMainMenuState.Nothing;

        /// <summary>
        /// Show main menu
        /// </summary>
        public void ShowMainMenu() => MainMenuState = EMainMenuState.Main;

        /// <summary>
        /// Show create world menu
        /// </summary>
        public void ShowCreateWorldMenu() => MainMenuState = EMainMenuState.CreateWorld;

        /// <summary>
        /// Show load worlds menu
        /// </summary>
        public void ShowLoadWorldsMenu() => MainMenuState = EMainMenuState.LoadWorlds;

        /// <summary>
        /// Show options menu
        /// </summary>
        public void ShowOptionsMenu() => MainMenuState = EMainMenuState.Options;

        /// <summary>
        /// Show credits menu
        /// </summary>
        public void ShowCreditsMenu() => MainMenuState = EMainMenuState.Credits;

        /// <summary>
        /// Back
        /// </summary>
        public void Back()
        {
            switch (mainMenuState)
            {
                case EMainMenuState.CreateWorld:
                    MainMenuState = EMainMenuState.Main;
                    break;
                case EMainMenuState.LoadWorlds:
                    MainMenuState = EMainMenuState.Main;
                    break;
                case EMainMenuState.Options:
                    MainMenuState = EMainMenuState.Main;
                    break;
                case EMainMenuState.Credits:
                    MainMenuState = EMainMenuState.Main;
                    break;
            }
        }

        /// <summary>
        /// Exit game
        /// </summary>
        public void ExitGame() => Game.Quit();

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            InputControllerScript input_controller = GetComponent<InputControllerScript>();
            if (input_controller)
            {
                input_controller.OnBackKeyPressed += Back;
            }
        }
    }
}

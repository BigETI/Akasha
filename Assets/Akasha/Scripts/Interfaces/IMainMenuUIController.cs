using TMPro;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Main menu UI controller interface
    /// </summary>
    public interface IMainMenuUIController : IBehaviour
    {
        /// <summary>
        /// World name input field
        /// </summary>
        TMP_InputField WorldNameInputField { get; set; }

        /// <summary>
        /// World description input field
        /// </summary>
        TMP_InputField WorldDescriptionInputField { get; set; }

        /// <summary>
        /// Main menu state
        /// </summary>
        EMainMenuState MainMenuState { get; set; }

        /// <summary>
        /// On menus hidden
        /// </summary>
        event MenusHiddenDelegate OnMenusHidden;

        /// <summary>
        /// On main menu shown
        /// </summary>
        event MainMenuShownDelegate OnMainMenuShown;

        /// <summary>
        /// On main menu shown
        /// </summary>
        event CreateWorldMenuShownDelegate OnCreateWorldMenuShown;

        /// <summary>
        /// On load worlds menu shown
        /// </summary>
        event LoadWorldsMenuShownDelegate OnLoadWorldsMenuShown;

        /// <summary>
        /// On options menu shown
        /// </summary>
        event OptionsMenuShownDelegate OnOptionsMenuShown;

        /// <summary>
        /// Credits menu shown
        /// </summary>
        event CreditsMenuShownDelegate OnCreditsMenuShown;

        /// <summary>
        /// Hide menus
        /// </summary>
        void HideMenus();

        /// <summary>
        /// Show main menu
        /// </summary>
        void ShowMainMenu();

        /// <summary>
        /// Show create world menu
        /// </summary>
        void ShowCreateWorldMenu();

        /// <summary>
        /// Show load worlds menu
        /// </summary>
        void ShowLoadWorldsMenu();

        /// <summary>
        /// Show options menu
        /// </summary>
        void ShowOptionsMenu();

        /// <summary>
        /// Show credits menu
        /// </summary>
        void ShowCreditsMenu();

        /// <summary>
        /// Create new world
        /// </summary>
        void CreateNewWorld();

        /// <summary>
        /// Back
        /// </summary>
        void Back();

        /// <summary>
        /// Exit game
        /// </summary>
        void ExitGame();
    }
}

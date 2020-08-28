using Akasha.InputActions;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Pause menu UI controller interface
    /// </summary>
    public interface IPauseMenuUIController
    {
        /// <summary>
        /// Pause menu state
        /// </summary>
        EPauseMenuState PauseMenuState { get; set; }

        /// <summary>
        /// Game input actions
        /// </summary>
        GameInputActions GameInputActions { get; }

        /// <summary>
        /// On main menu shown
        /// </summary>
        event PauseMenuStateChangedDelegate OnMainMenuShown;

        /// <summary>
        /// On options shown
        /// </summary>
        event PauseMenuStateChangedDelegate OnOptionsShown;

        /// <summary>
        /// Show pause menu
        /// </summary>
        void ShowPauseMenu();

        /// <summary>
        /// Resume game
        /// </summary>
        void ResumeGame();

        /// <summary>
        /// Save game
        /// </summary>
        void SaveGame();

        /// <summary>
        /// Show options
        /// </summary>
        void ShowOptions();

        /// <summary>
        /// Exit game
        /// </summary>
        void ExitGame();
    }
}

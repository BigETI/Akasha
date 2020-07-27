using Akasha.InputActions;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Datapad UI controller interface
    /// </summary>
    public interface IDatapadUIController : IBehaviour
    {
        /// <summary>
        /// Datapad mode
        /// </summary>
        EDatapadMode DatapadMode { get; set; }

        /// <summary>
        /// Game input actions
        /// </summary>
        GameInputActions GameInputActions { get; }

        /// <summary>
        /// On show inventory
        /// </summary>
        event DatapadChangedDelegate OnShowInventory;

        /// <summary>
        /// On show crafting
        /// </summary>
        event DatapadChangedDelegate OnShowCrafting;

        /// <summary>
        /// On show technologies
        /// </summary>
        event DatapadChangedDelegate OnShowTechnologies;

        /// <summary>
        /// On show worlds
        /// </summary>
        event DatapadChangedDelegate OnShowWorlds;

        /// <summary>
        /// Show inventory
        /// </summary>
        void ShowInventory();

        /// <summary>
        /// Show crafting menu
        /// </summary>
        void ShowCraftingMenu();

        /// <summary>
        /// Show technologies
        /// </summary>
        void ShowTechnologies();

        /// <summary>
        /// Show worlds
        /// </summary>
        void ShowWorlds();
    }
}

using Akasha.InputActions;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Datapad UI controller script class
    /// </summary>
    public class DatapadUIControllerScript : MonoBehaviour, IDatapadUIController
    {
        /// <summary>
        /// On show inventory
        /// </summary>
        [SerializeField]
        private UnityEvent onShowInventory = default;

        /// <summary>
        /// On show crafting
        /// </summary>
        [SerializeField]
        private UnityEvent onShowCrafting = default;

        /// <summary>
        /// On show technologies
        /// </summary>
        [SerializeField]
        private UnityEvent onShowTechnologies = default;

        /// <summary>
        /// On show worlds
        /// </summary>
        [SerializeField]
        private UnityEvent onShowWorlds = default;

        /// <summary>
        /// Datapad mode
        /// </summary>
        private EDatapadMode datapadMode = EDatapadMode.Inventory;

        /// <summary>
        /// Datapad mode
        /// </summary>
        public EDatapadMode DatapadMode
        {
            get => datapadMode;
            set
            {
                if (datapadMode != value)
                {
                    datapadMode = value;
                    switch (datapadMode)
                    {
                        case EDatapadMode.Inventory:
                            if (onShowInventory != null)
                            {
                                onShowInventory.Invoke();
                            }
                            OnShowInventory?.Invoke();
                            break;
                        case EDatapadMode.Crafting:
                            if (onShowCrafting != null)
                            {
                                onShowCrafting.Invoke();
                            }
                            OnShowCrafting?.Invoke();
                            break;
                        case EDatapadMode.Technologies:
                            if (onShowTechnologies != null)
                            {
                                onShowTechnologies.Invoke();
                            }
                            OnShowTechnologies?.Invoke();
                            break;
                        case EDatapadMode.Worlds:
                            if (onShowWorlds != null)
                            {
                                onShowWorlds.Invoke();
                            }
                            OnShowWorlds?.Invoke();
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
        /// On show inventory
        /// </summary>
        public event DatapadChangedDelegate OnShowInventory;

        /// <summary>
        /// On show crafting
        /// </summary>
        public event DatapadChangedDelegate OnShowCrafting;

        /// <summary>
        /// On show technologies
        /// </summary>
        public event DatapadChangedDelegate OnShowTechnologies;

        /// <summary>
        /// On show worlds
        /// </summary>
        public event DatapadChangedDelegate OnShowWorlds;

        /// <summary>
        /// Show inventory
        /// </summary>
        public void ShowInventory() => DatapadMode = EDatapadMode.Inventory;

        /// <summary>
        /// Show crafting menu
        /// </summary>
        public void ShowCraftingMenu() => DatapadMode = EDatapadMode.Crafting;

        /// <summary>
        /// Show technologies
        /// </summary>
        public void ShowTechnologies() => DatapadMode = EDatapadMode.Technologies;

        /// <summary>
        /// Show worlds
        /// </summary>
        public void ShowWorlds() => DatapadMode = EDatapadMode.Worlds;

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            GameInputActions = new GameInputActions();
            GameInputActions.GameActionMap.Datapad.performed += (context) =>
            {
                GameManager.GameState = ((GameManager.GameState == EGameState.ShowingDatapad) ? EGameState.Playing : EGameState.ShowingDatapad);
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

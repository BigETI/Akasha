using Akasha.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Inventory UI controller script class
    /// </summary>
    public class InventoryUIControllerScript : MonoBehaviour, IInventoryUIController
    {
        /// <summary>
        /// Inventory item slot button asset
        /// </summary>
        [SerializeField]
        private GameObject inventoryItemSlotButtonAsset = default;

        /// <summary>
        /// Inventory panel rectangle transform
        /// </summary>
        [SerializeField]
        private RectTransform inventoryPanelRectangleTransform = default;

        /// <summary>
        /// Inventory item slot description
        /// </summary>
        [SerializeField]
        private InventoryItemSlotUIControllerScript inventoryItemSlotDescription = default;

        /// <summary>
        /// Inventory item slot controllers
        /// </summary>
        [SerializeField]
        private InventoryItemSlotUIControllerScript[] inventoryItemSlotControllers = Array.Empty<InventoryItemSlotUIControllerScript>();

        /// <summary>
        /// Last items
        /// </summary>
        private readonly List<(InventoryItemSlotUIControllerScript, IInventoryItemData)> itemSlots = new List<(InventoryItemSlotUIControllerScript, IInventoryItemData)>();

        /// <summary>
        /// Inventory item slot button asset
        /// </summary>
        public GameObject InventoryItemSlotButtonAsset
        {
            get => inventoryItemSlotButtonAsset;
            set => inventoryItemSlotButtonAsset = value;
        }

        /// <summary>
        /// Inventory panel rectangle transform
        /// </summary>
        public RectTransform InventoryPanelRectangleTransform
        {
            get => inventoryPanelRectangleTransform;
            set => inventoryPanelRectangleTransform = value;
        }

        /// <summary>
        /// Inventory item slot description
        /// </summary>
        public InventoryItemSlotUIControllerScript InventoryItemSlotDescription
        {
            get => inventoryItemSlotDescription;
            set => inventoryItemSlotDescription = value;
        }

        /// <summary>
        /// Inventory item slot controllers
        /// </summary>
        public InventoryItemSlotUIControllerScript[] InventoryItemSlotControllers
        {
            get
            {
                if (inventoryItemSlotControllers == null)
                {
                    inventoryItemSlotControllers = Array.Empty<InventoryItemSlotUIControllerScript>();
                }
                return inventoryItemSlotControllers;
            }
            set
            {
                inventoryItemSlotControllers = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <summary>
        /// Character controller
        /// </summary>
        public ICharacterController CharacterController { get; set; }

        /// <summary>
        /// Select inventory item slot
        /// </summary>
        /// <param name="inventoryItem">Inventory item</param>
        public void SelectInventoryItemSlot(IInventoryItemData inventoryItem)
        {
            if (inventoryItemSlotDescription)
            {
                inventoryItemSlotDescription.gameObject.SetActive(inventoryItem != null);
                inventoryItemSlotDescription.SetValues(inventoryItem, this);
            }
        }

        /// <summary>
        /// Deselect inventory item slot
        /// </summary>
        public void DeselectInventoryItemSlot() => SelectInventoryItemSlot(null);

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            PlayerControllerScript player_controller = FindObjectOfType<PlayerControllerScript>();
            if (player_controller)
            {
                CharacterController = player_controller.GetComponent<CharacterControllerScript>();
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            IReadOnlyList<InventoryItemData> inventory_items = ((CharacterController == null) ? Array.Empty<InventoryItemData>() : CharacterController.Inventory.Items);
            InventoryItemSlotUIControllerScript[] inventory_item_slot_controllers = InventoryItemSlotControllers;
            EGameState game_state = GameManager.GameState;
            if (itemSlots.Count > inventory_items.Count)
            {
                for (int index = inventory_items.Count; index < itemSlots.Count; index++)
                {
                    (InventoryItemSlotUIControllerScript, IInventoryItemData) item_slot = itemSlots[index];
                    if (item_slot.Item1)
                    {
                        Destroy(item_slot.Item1.gameObject);
                    }
                }
                itemSlots.RemoveRange(inventory_items.Count, itemSlots.Count - inventory_items.Count);
            }
            else if (itemSlots.Count < inventory_items.Count)
            {
                for (int index = itemSlots.Count; index < inventory_items.Count; index++)
                {
                    InventoryItemSlotUIControllerScript inventory_item_slot_ui_controller = null;
                    IInventoryItemData inventory_item = inventory_items[index];
                    if (inventoryItemSlotButtonAsset)
                    {
                        GameObject game_object = Instantiate(inventoryItemSlotButtonAsset);
                        if (game_object != null)
                        {
                            if (game_object.TryGetComponent(out inventory_item_slot_ui_controller) && game_object.TryGetComponent(out RectTransform rectangle_transform))
                            {
                                rectangle_transform.SetParent(inventoryPanelRectangleTransform, false);
                                inventory_item_slot_ui_controller.SetValues(inventory_item, this);
                            }
                            else
                            {
                                inventory_item_slot_ui_controller = null;
                                Destroy(game_object);
                            }
                        }
                    }
                    itemSlots.Add((inventory_item_slot_ui_controller, inventory_item));
                    if (index < inventory_item_slot_controllers.Length)
                    {
                        InventoryItemSlotUIControllerScript inventory_item_slot_controller = inventory_item_slot_controllers[index];
                        if (inventory_item_slot_controller)
                        {
                            inventory_item_slot_controller.SetValues(inventory_item, this);
                            inventory_item_slot_controller.gameObject.SetActive(true);
                            if ((game_state == EGameState.Playing) && (CharacterController != null) && (CharacterController.SelectedInventoryItemSlotIndex == index))
                            {
                                inventory_item_slot_controller.Select();
                            }
                        }
                    }
                }
            }
            for (int index = 0; index < itemSlots.Count; index++)
            {
                (InventoryItemSlotUIControllerScript, IInventoryItemData) item_slot = itemSlots[index];
                IInventoryItemData inventory_item = inventory_items[index];
                if ((item_slot.Item2.Item != inventory_item.Item) || (item_slot.Item2.Health != inventory_item.Health) || (item_slot.Item2.Quantity != inventory_item.Quantity))
                {
                    if (item_slot.Item1)
                    {
                        item_slot.Item1.SetValues(inventory_item, this);
                    }
                    if (index < inventory_item_slot_controllers.Length)
                    {
                        InventoryItemSlotUIControllerScript inventory_item_slot_controller = inventory_item_slot_controllers[index];
                        if (inventory_item_slot_controller)
                        {
                            inventory_item_slot_controller.SetValues(inventory_item, this);
                            inventory_item_slot_controller.gameObject.SetActive(true);
                        }
                    }
                }
                if ((game_state == EGameState.Playing) && (index < inventory_item_slot_controllers.Length) && (CharacterController != null) && (CharacterController.SelectedInventoryItemSlotIndex == index))
                {
                    inventory_item_slot_controllers[index].Select();
                }
            }
            for (int index = itemSlots.Count; index < inventory_item_slot_controllers.Length; index++)
            {
                InventoryItemSlotUIControllerScript inventory_item_slot_controller = inventory_item_slot_controllers[index];
                if (inventory_item_slot_controller)
                {
                    inventory_item_slot_controller.SetValues(null, this);
                    inventory_item_slot_controller.gameObject.SetActive(false);
                }
            }
            if ((CharacterController != null) && (CharacterController.SelectedInventoryItemSlotIndex >= itemSlots.Count))
            {
                CharacterController.SelectedInventoryItemSlotIndex = itemSlots.Count - 1;
            }
        }
    }
}

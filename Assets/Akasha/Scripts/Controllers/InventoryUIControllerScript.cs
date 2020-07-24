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
        /// Inventory item slot controllers
        /// </summary>
        [SerializeField]
        private InventoryItemSlotUIControllerScript[] inventoryItemSlotControllers = Array.Empty<InventoryItemSlotUIControllerScript>();

        /// <summary>
        /// Inventory item slot asset
        /// </summary>
        [SerializeField]
        private GameObject inventoryItemSlotAsset = default;

        /// <summary>
        /// Last items
        /// </summary>
        private List<(InventoryItemSlotUIControllerScript, IInventoryItemData)> itemSlots = new List<(InventoryItemSlotUIControllerScript, IInventoryItemData)>();

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
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                inventoryItemSlotControllers = value;
            }
        }

        /// <summary>
        /// Inventory item slot asset
        /// </summary>
        public GameObject InventoryItemSlotAsset
        {
            get => inventoryItemSlotAsset;
            set => inventoryItemSlotAsset = value;
        }

        /// <summary>
        /// Character controller
        /// </summary>
        public ICharacterController CharacterController { get; set; }

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
                    if (inventoryItemSlotAsset)
                    {
                        GameObject game_object = Instantiate(inventoryItemSlotAsset);
                        if (game_object != null)
                        {
                            if (game_object.TryGetComponent(out inventory_item_slot_ui_controller) && game_object.TryGetComponent(out RectTransform rectangle_transform))
                            {
                                inventory_item_slot_ui_controller.SetValues(inventory_item);
                                rectangle_transform.SetParent(transform, false);
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
                            inventory_item_slot_controller.SetValues(inventory_item);
                            inventory_item_slot_controller.gameObject.SetActive(true);
                            if ((CharacterController != null) && (CharacterController.SelectedInventoryItemSlotIndex == index) && inventory_item_slot_controller.Selectable)
                            {
                                inventory_item_slot_controller.Selectable.Select();
                            }
                        }
                    }
                }
            }
            for (int index = 0; index < itemSlots.Count; index++)
            {
                (InventoryItemSlotUIControllerScript, IInventoryItemData) item_slot = itemSlots[index];
                IInventoryItemData inventory_item = inventory_items[index];
                if ((item_slot.Item2.Item != inventory_item.Item) || (item_slot.Item2.Quantity != inventory_item.Quantity))
                {
                    if (item_slot.Item1)
                    {
                        item_slot.Item1.SetValues(inventory_item);
                    }
                    if (index < inventory_item_slot_controllers.Length)
                    {
                        InventoryItemSlotUIControllerScript inventory_item_slot_controller = inventory_item_slot_controllers[index];
                        if (inventory_item_slot_controller)
                        {
                            inventory_item_slot_controller.SetValues(inventory_item);
                            inventory_item_slot_controller.gameObject.SetActive(true);
                        }
                    }
                }
                if ((index < inventory_item_slot_controllers.Length) && (CharacterController != null) && (CharacterController.SelectedInventoryItemSlotIndex == index) && inventory_item_slot_controllers[index].Selectable)
                {
                    inventory_item_slot_controllers[index].Selectable.Select();
                }
            }
            for (int index = itemSlots.Count; index < inventory_item_slot_controllers.Length; index++)
            {
                InventoryItemSlotUIControllerScript inventory_item_slot_controller = inventory_item_slot_controllers[index];
                if (inventory_item_slot_controller)
                {
                    inventory_item_slot_controller.SetValues(null);
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

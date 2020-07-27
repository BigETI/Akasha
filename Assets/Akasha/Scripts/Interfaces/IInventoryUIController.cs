using Akasha.Controllers;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Inventory UI controller interface
    /// </summary>
    public interface IInventoryUIController : IBehaviour
    {
        /// <summary>
        /// Inventory item slot button asset
        /// </summary>
        GameObject InventoryItemSlotButtonAsset { get; set; }

        /// <summary>
        /// Inventory panel rectangle transform
        /// </summary>
        RectTransform InventoryPanelRectangleTransform { get; set; }

        /// <summary>
        /// Inventory item slot description
        /// </summary>
        InventoryItemSlotUIControllerScript InventoryItemSlotDescription { get; set; }

        /// <summary>
        /// Inventory item slot controllers
        /// </summary>
        InventoryItemSlotUIControllerScript[] InventoryItemSlotControllers { get; set; }

        /// <summary>
        /// Character controller
        /// </summary>
        ICharacterController CharacterController { get; set; }

        /// <summary>
        /// Select inventory item slot
        /// </summary>
        /// <param name="inventoryItem">Inventory item</param>
        void SelectInventoryItemSlot(IInventoryItemData inventoryItem);

        /// <summary>
        /// Deselect inventory item slot
        /// </summary>
        void DeselectInventoryItemSlot();
    }
}

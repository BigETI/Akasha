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
        /// Inventory item slot controllers
        /// </summary>
        InventoryItemSlotUIControllerScript[] InventoryItemSlotControllers { get; set; }

        /// <summary>
        /// Inventory item slot asset
        /// </summary>
        GameObject InventoryItemSlotAsset { get; set; }

        /// <summary>
        /// Character controller
        /// </summary>
        ICharacterController CharacterController { get; set; }
    }
}

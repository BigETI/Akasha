using TMPro;
using UnityEngine.UI;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Inventory item slot UI controller interface
    /// </summary>
    public interface IInventoryItemSlotUIController : IBehaviour
    {
        /// <summary>
        /// Icon image
        /// </summary>
        Image IconImage { get; set; }

        /// <summary>
        /// Icon image
        /// </summary>
        TextMeshProUGUI QuantityText { get; set; }

        /// <summary>
        /// Selectable
        /// </summary>
        Selectable Selectable { get; }

        /// <summary>
        /// Inventory item data
        /// </summary>
        IInventoryItemData InventoryItemData { get; }

        /// <summary>
        /// Set values
        /// </summary>
        /// <param name="inventoryItemData">Inventory item data</param>
        void SetValues(IInventoryItemData inventoryItemData);
    }
}

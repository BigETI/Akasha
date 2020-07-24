/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Inventory item data interface
    /// </summary>
    public interface IInventoryItemData
    {
        /// <summary>
        /// Inventory item name
        /// </summary>
        IItemObject Item { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        uint Quantity { get; set; }
    }
}

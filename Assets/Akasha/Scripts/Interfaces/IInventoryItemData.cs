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
        /// Health
        /// </summary>
        uint Health { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        uint Quantity { get; set; }

        /// <summary>
        /// Is usable
        /// </summary>
        bool IsUsable { get; }
    }
}

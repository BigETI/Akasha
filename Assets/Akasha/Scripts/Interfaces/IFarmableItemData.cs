using Akasha.Objects;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Farmable item data interface
    /// </summary>
    public interface IFarmableItemData
    {
        /// <summary>
        /// Farmable item
        /// </summary>
        ItemObjectScript FarmableItem { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        uint Quantity { get; set; }

        /// <summary>
        /// Bias
        /// </summary>
        uint Bias { get; set; }
    }
}

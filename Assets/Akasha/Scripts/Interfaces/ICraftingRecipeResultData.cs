using Akasha.Objects;

/// <summary>
/// AKasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Crafting recipe or result data interface
    /// </summary>
    public interface ICraftingRecipeResultData
    {
        /// <summary>
        /// Item
        /// </summary>
        ItemObjectScript Item { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        uint Quantity { get; set; }
    }
}

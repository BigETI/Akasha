using Akasha.Data;
using Akasha.Objects;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Farming tool data interface
    /// </summary>
    public interface IFarmingToolData
    {
        /// <summary>
        /// Farming tool item
        /// </summary>
        ItemObjectScript FarmingToolItem { get; set; }

        /// <summary>
        /// Damage
        /// </summary>
        ushort Damage { get; set; }

        /// <summary>
        /// Farmable items
        /// </summary>
        FarmableItemData[] FarmableItems { get; set; }

        /// <summary>
        /// Random farmable item
        /// </summary>
        FarmableItemData RandomFarmableItem { get; }
    }
}

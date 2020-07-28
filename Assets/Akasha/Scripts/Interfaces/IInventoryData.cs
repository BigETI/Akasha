using Akasha.Data;
using System.Collections.Generic;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Inventory data interface
    /// </summary>
    public interface IInventoryData
    {
        /// <summary>
        /// Inventory items
        /// </summary>
        IReadOnlyList<InventoryItemData> Items { get; }

        /// <summary>
        /// Weight
        /// </summary>
        uint Weight { get; }

        /// <summary>
        /// Add items
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="health">Health</param>
        /// <param name="quantity">Quantity</param>
        void AddItems(IItemObject item, uint health, uint quantity);

        /// <summary>
        /// Add items with weight limit
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="health">Health</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="weightLimit">Weight limit</param>
        /// <returns>Number of items added</returns>
        uint AddItemsWithWeightLimit(IItemObject item, uint health, uint quantity, uint weightLimit);

        /// <summary>
        /// Remove items
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="quantity">Quantity</param>
        /// <returns>Number of items removed</returns>
        uint RemoveItems(IItemObject item, uint quantity);

        /// <summary>
        /// Craft items
        /// </summary>
        /// <param name="craftingRecipies">Crafting recipies</param>
        /// <param name="quantity">Quantity</param>
        /// <returns>Number of crafting results</returns>
        uint CraftItems(ICraftingRecipesObject craftingRecipies, uint quantity);

        /// <summary>
        /// Set inventory item health
        /// </summary>
        /// <param name="inventoryItemSlotIndex">Inventory item slot index</param>
        /// <param name="health">Health</param>
        /// <returns>"true" if successful, otherwise "false"</returns>
        bool SetInventoryItemHealth(uint inventoryItemSlotIndex, uint health);
    }
}

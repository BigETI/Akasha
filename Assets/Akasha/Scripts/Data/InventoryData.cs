﻿using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// Akasha data namespace
/// </summary>
namespace Akasha.Data
{
    /// <summary>
    /// Inventory data class
    /// </summary>
    [Serializable]
    public class InventoryData : IInventoryData
    {
        /// <summary>
        /// Inventory items
        /// </summary>
        [SerializeField]
        private List<InventoryItemData> items;

        /// <summary>
        /// Inventory items
        /// </summary>
        public IReadOnlyList<InventoryItemData> Items
        {
            get
            {
                if (items == null)
                {
                    items = new List<InventoryItemData>();
                }
                return items;
            }
        }

        /// <summary>
        /// Weight
        /// </summary>
        public uint Weight
        {
            get
            {
                long ret = 0;
                foreach (InventoryItemData item in items)
                {
                    if (item.Item != null)
                    {
                        Interlocked.Add(ref ret, item.Item.Weight);
                    }
                }
                return (uint)ret;
            }
        }

        /// <summary>
        /// Add items
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="health">Health</param>
        /// <param name="quantity">Quantity</param>
        public void AddItems(IItemObject item, uint health, uint quantity)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (quantity > 0U)
            {
                uint remaining = quantity;
                if (items == null)
                {
                    items = new List<InventoryItemData>();
                }
                for (int index = 0; index < items.Count; index++)
                {
                    InventoryItemData item_data = items[index];
                    if ((item_data.Item == item) && (item_data.Health == health))
                    {
                        if (item.MaximalStackSize > 0U)
                        {
                            long capacity = (long)(item.MaximalStackSize) - item_data.Quantity;
                            if (capacity > 0L)
                            {
                                if (capacity < remaining)
                                {
                                    items[index] = new InventoryItemData(item, health, item.MaximalStackSize);
                                    remaining -= (uint)capacity;
                                }
                                else
                                {
                                    items[index] = new InventoryItemData(item, health, item_data.Quantity + remaining);
                                    remaining = 0U;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            items[index] = new InventoryItemData(item, health, item_data.Quantity + remaining);
                            remaining = 0U;
                            break;
                        }
                    }
                }
                while (remaining > 0U)
                {
                    if (item.MaximalStackSize > 0U)
                    {
                        if (remaining > item.MaximalStackSize)
                        {
                            items.Add(new InventoryItemData(item, health, item.MaximalStackSize));
                            remaining -= item.MaximalStackSize;
                        }
                        else
                        {
                            items.Add(new InventoryItemData(item, health, remaining));
                            remaining = 0U;
                        }
                    }
                    else
                    {
                        items.Add(new InventoryItemData(item, health, remaining));
                        remaining = 0U;
                    }
                }
            }
        }

        /// <summary>
        /// Add items with weight limit
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="health">Health</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="weightLimit">Weight limit</param>
        /// <returns>Number of items added</returns>
        public uint AddItemsWithWeightLimit(IItemObject item, uint health, uint quantity, uint weightLimit)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            uint ret = 0U;
            uint weight = Weight;
            if (weight < weightLimit)
            {
                uint remaining_capacity = weightLimit - weight;
                ret = remaining_capacity / item.Weight;
                ret = ((ret < quantity) ? ret : quantity);
                if (ret > 0U)
                {
                    uint remaining = ret;
                    for (int index = 0; index < items.Count; index++)
                    {
                        InventoryItemData item_data = items[index];
                        if ((item_data.Item == item) && (item_data.Health == health))
                        {
                            if (item.MaximalStackSize > 0U)
                            {
                                long capacity = (long)(item.MaximalStackSize) - item_data.Quantity;
                                if (capacity > 0L)
                                {
                                    if (capacity < remaining)
                                    {
                                        items[index] = new InventoryItemData(item, health, item.MaximalStackSize);
                                        remaining -= (uint)capacity;
                                    }
                                    else
                                    {
                                        items[index] = new InventoryItemData(item, health, item_data.Quantity + remaining);
                                        remaining = 0U;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                items[index] = new InventoryItemData(item, health, item_data.Quantity + remaining);
                                remaining = 0U;
                                break;
                            }
                        }
                    }
                    while (remaining > 0U)
                    {
                        if (item.MaximalStackSize > 0U)
                        {
                            if (remaining > item.MaximalStackSize)
                            {
                                items.Add(new InventoryItemData(item, health, item.MaximalStackSize));
                                remaining -= item.MaximalStackSize;
                            }
                            else
                            {
                                items.Add(new InventoryItemData(item, health, remaining));
                                remaining = 0U;
                            }
                        }
                        else
                        {
                            items.Add(new InventoryItemData(item, health, remaining));
                            remaining = 0U;
                        }
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Remove items
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="quantity">Quantity</param>
        /// <returns>Number of items removed</returns>
        public uint RemoveItems(IItemObject item, uint quantity)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            uint ret = 0U;
            uint remaining = quantity;
            for (int index = 0; index < items.Count; index++)
            {
                InventoryItemData item_data = items[index];
                if (item_data.Item == item)
                {
                    if (item_data.Quantity > remaining)
                    {
                        items[index] = new InventoryItemData(item, item_data.Health, item_data.Quantity - remaining);
                        ret += remaining;
                        break;
                    }
                    else
                    {
                        ret += item_data.Quantity;
                        remaining -= item_data.Quantity;
                        items.RemoveAt(index);
                        --index;
                        if (remaining == 0U)
                        {
                            break;
                        }
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Craft items
        /// </summary>
        /// <param name="craftingRecipes">Crafting recipes</param>
        /// <param name="quantity">Quantity</param>
        /// <returns>Number of crafting results</returns>
        public uint CraftItems(ICraftingRecipesObject craftingRecipes, uint quantity)
        {
            if (craftingRecipes == null)
            {
                throw new ArgumentNullException(nameof(craftingRecipes));
            }
            uint ret = 0U;
            if (quantity > 0U)
            {
                Dictionary<string, uint> capacity = new Dictionary<string, uint>();
                Dictionary<string, (IItemObject, uint)> dependencies = new Dictionary<string, (IItemObject, uint)>();
                foreach (InventoryItemData item in Items)
                {
                    if ((item.Item != null) && (item.Quantity > 0U))
                    {
                        string key = item.Item.Key;
                        if (capacity.ContainsKey(key))
                        {
                            capacity[key] = capacity[key] + item.Quantity;
                        }
                        else
                        {
                            capacity.Add(key, item.Quantity);
                        }
                    }
                }
                foreach (CraftingRecipeResultData crafting_recipe in craftingRecipes.CraftingRecipes)
                {
                    if (crafting_recipe.Item && (crafting_recipe.Quantity > 0U))
                    {
                        string key = crafting_recipe.Item.Key;
                        if (dependencies.ContainsKey(key))
                        {
                            dependencies[key] = (crafting_recipe.Item, dependencies[key].Item2 + crafting_recipe.Quantity);
                        }
                        else
                        {
                            dependencies.Add(key, (crafting_recipe.Item, crafting_recipe.Quantity));
                        }
                    }
                }
                ret = quantity;
                foreach (KeyValuePair<string, (IItemObject, uint)> dependency in dependencies)
                {
                    if (capacity.ContainsKey(dependency.Key))
                    {
                        uint capacity_quantity = capacity[dependency.Key];
                        uint result = capacity_quantity / dependency.Value.Item2;
                        ret = ((result < ret) ? result : ret);
                    }
                    else
                    {
                        ret = 0U;
                        break;
                    }
                }
                if (ret > 0U)
                {
                    foreach ((IItemObject, uint) dependency in dependencies.Values)
                    {
                        RemoveItems(dependency.Item1, dependency.Item2 * ret);
                    }
                    foreach (CraftingRecipeResultData crafting_result in craftingRecipes.CraftingResults)
                    {
                        if (crafting_result.Item && (crafting_result.Quantity > 0U))
                        {
                            AddItems(crafting_result.Item, crafting_result.Item.MaximalHealth, crafting_result.Quantity * ret);
                        }
                    }
                }
                capacity.Clear();
                dependencies.Clear();
            }
            return ret;
        }

        /// <summary>
        /// Set inventory item health
        /// </summary>
        /// <param name="inventoryItemSlotIndex">Inventory item slot index</param>
        /// <param name="health">Health</param>
        /// <returns>"true" if successful, otherwise "false"</returns>
        public bool SetInventoryItemHealth(uint inventoryItemSlotIndex, uint health)
        {
            bool ret = false;
            if ((items != null) && (inventoryItemSlotIndex < items.Count))
            {
                InventoryItemData inventory_item = items[(int)inventoryItemSlotIndex];
                items[(int)inventoryItemSlotIndex] = new InventoryItemData(inventory_item.Item, health, inventory_item.Quantity);
                ret = true;
            }
            return ret;
        }
    }
}

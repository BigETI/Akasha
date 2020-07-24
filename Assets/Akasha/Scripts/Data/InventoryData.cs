using System;
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
        /// <param name="quantity">Quantity</param>
        public void AddItems(IItemObject item, uint quantity)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (quantity > 0U)
            {
                bool add_item = true;
                if (items == null)
                {
                    items = new List<InventoryItemData>();
                }
                for (int index = 0; index < items.Count; index++)
                {
                    InventoryItemData item_data = items[index];
                    if (item_data.Item == item)
                    {
                        items[index] = new InventoryItemData(item, item_data.Quantity + quantity);
                        add_item = false;
                        break;
                    }
                }
                if (add_item)
                {
                    items.Add(new InventoryItemData(item, quantity));
                }
            }
        }

        /// <summary>
        /// Add items with weight limit
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="weightLimit">Weight limit</param>
        /// <returns>Number of items added</returns>
        public uint AddItemsWithWeightLimit(IItemObject item, uint quantity, uint weightLimit)
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
                bool add_item = true;
                ret = remaining_capacity / item.Weight;
                ret = ((ret < quantity) ? ret : quantity);
                if (ret > 0U)
                {
                    for (int index = 0; index < items.Count; index++)
                    {
                        InventoryItemData item_data = items[index];
                        if (item_data.Item == item)
                        {
                            items[index] = new InventoryItemData(item, item_data.Quantity + ret);
                            add_item = false;
                            break;
                        }
                    }
                    if (add_item)
                    {
                        items.Add(new InventoryItemData(item, ret));
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
                        items[index] = new InventoryItemData(item, item_data.Quantity - remaining);
                        ret += remaining;
                        remaining = 0U;
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
    }
}

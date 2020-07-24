using Akasha.Objects;
using System;
using UnityEngine;

/// <summary>
/// Akasha data namespace
/// </summary>
namespace Akasha.Data
{
    /// <summary>
    /// Inventory item data
    /// </summary>
    [Serializable]
    public struct InventoryItemData : IInventoryItemData
    {
        /// <summary>
        /// Inventory item name
        /// </summary>
        [SerializeField]
        private string name;

        /// <summary>
        /// Quantity
        /// </summary>
        [SerializeField]
        private uint quantity;

        /// <summary>
        /// Item
        /// </summary>
        private IItemObject item;

        /// <summary>
        /// Inventory item name
        /// </summary>
        public IItemObject Item
        {
            get
            {
                if (item == null)
                {
                    item = Resources.Load<ItemObjectScript>("Items/" + name);
                }
                return item;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                name = ((value is IBlockObject) ? "Blocks/" : "Entities/") + value.name;
                item = value;
            }
        }

        /// <summary>
        /// Quantity
        /// </summary>
        public uint Quantity
        {
            get => quantity;
            set => quantity = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="quantity">Quantity</param>
        public InventoryItemData(IItemObject item, uint quantity)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            name = ((item is IBlockObject) ? "Blocks/" : "Entities/") + item.name;
            this.item = item;
            this.quantity = quantity;
        }
    }
}

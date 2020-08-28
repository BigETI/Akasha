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
        /// Health
        /// </summary>
        [SerializeField]
        private uint health;

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
                    item = Resources.Load<ItemObjectScript>($"Items/{ name }");
                }
                return item;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                name = value.Key;
                item = value;
            }
        }

        /// <summary>
        /// Health
        /// </summary>
        public uint Health
        {
            get => health;
            set => health = value;
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
        /// Is usable
        /// </summary>
        public bool IsUsable => (Item != null) && (((Item.MaximalHealth > 0U) && (health > 0U)) || (Item.MaximalHealth <= 0U));

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="health">Health</param>
        /// <param name="quantity">Quantity</param>
        public InventoryItemData(IItemObject item, uint health, uint quantity)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            name = item.Key;
            this.item = item;
            this.health = health;
            this.quantity = quantity;
        }
    }
}

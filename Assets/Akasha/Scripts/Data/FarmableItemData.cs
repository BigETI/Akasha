using Akasha.Objects;
using System;
using UnityEngine;

/// <summary>
/// Akasha data namespace
/// </summary>
namespace Akasha.Data
{
    /// <summary>
    /// Farmable item structure
    /// </summary>
    [Serializable]
    public struct FarmableItemData : IFarmableItemData
    {
        /// <summary>
        /// Farmable item
        /// </summary>
        [SerializeField]
        private ItemObjectScript farmableItem;

        /// <summary>
        /// Quantity
        /// </summary>
        [SerializeField]
        private uint quantity;

        /// <summary>
        /// Bias
        /// </summary>
        [SerializeField]
        [Range(0, 1000)]
        private uint bias;

        /// <summary>
        /// Farmable item
        /// </summary>
        public ItemObjectScript FarmableItem
        {
            get => farmableItem;
            set => farmableItem = value;
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
        /// Bias
        /// </summary>
        public uint Bias
        {
            get => (uint)(Mathf.Clamp(bias, 0, 1000));
            set => bias = (uint)(Mathf.Clamp(value, 0, 1000));
        }
    }
}

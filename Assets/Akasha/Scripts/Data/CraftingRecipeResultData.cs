using Akasha.Objects;
using System;
using UnityEngine;

/// <summary>
/// Akasha data namespace
/// </summary>
namespace Akasha.Data
{
    /// <summary>
    /// Crafting recipe or result data class
    /// </summary>
    [Serializable]
    public struct CraftingRecipeResultData : ICraftingRecipeResultData
    {
        /// <summary>
        /// Item
        /// </summary>
        [SerializeField]
        private ItemObjectScript item;

        /// <summary>
        /// Quantity
        /// </summary>
        [SerializeField]
        private uint quantity;

        /// <summary>
        /// Item
        /// </summary>
        public ItemObjectScript Item
        {
            get => item;
            set => item = value;
        }

        /// <summary>
        /// Quantity
        /// </summary>
        public uint Quantity
        {
            get => quantity;
            set => quantity = value;
        }
    }
}

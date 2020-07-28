using Akasha.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Akasha data namespace
/// </summary>
namespace Akasha.Data
{
    /// <summary>
    /// Farming tool data structure
    /// </summary>
    [Serializable]
    public struct FarmingToolData : IFarmingToolData
    {
        /// <summary>
        /// Item
        /// </summary>
        [SerializeField]
        private ItemObjectScript farmingToolItem;

        /// <summary>
        /// Damage
        /// </summary>
        [SerializeField]
        private ushort damage;

        /// <summary>
        /// Farmable items
        /// </summary>
        [SerializeField]
        private FarmableItemData[] farmableItems;

        /// <summary>
        /// Farming tool item
        /// </summary>
        public ItemObjectScript FarmingToolItem
        {
            get => farmingToolItem;
            set => farmingToolItem = value;
        }

        /// <summary>
        /// Damage
        /// </summary>
        public ushort Damage
        {
            get => damage;
            set => damage = value;
        }

        /// <summary>
        /// Farmable items
        /// </summary>
        public FarmableItemData[] FarmableItems
        {
            get
            {
                if (farmableItems == null)
                {
                    farmableItems = Array.Empty<FarmableItemData>();
                }
                return farmableItems;
            }
            set => farmableItems = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Random farmable item
        /// </summary>
        public FarmableItemData RandomFarmableItem
        {
            get
            {
                FarmableItemData ret = default;
                if (FarmableItems.Length > 0)
                {
                    int bias_sum = 0;
                    Parallel.ForEach(FarmableItems, (farmable_item) => Interlocked.Add(ref bias_sum, (int)(farmable_item.Bias)));
                    int position = UnityEngine.Random.Range(0, bias_sum);
                    foreach (FarmableItemData farmable_item in FarmableItems)
                    {
                        position -= (int)(farmable_item.Bias);
                        if (position <= 0)
                        {
                            ret = farmable_item;
                            break;
                        }
                    }
                }
                return ret;
            }
        }
    }
}

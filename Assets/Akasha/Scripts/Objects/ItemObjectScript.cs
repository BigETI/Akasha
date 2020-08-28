using UnityEngine;
using UnityTranslator.Objects;

/// <summary>
/// Akasha objects namespace
/// </summary>
namespace Akasha.Objects
{
    /// <summary>
    /// Item object script class
    /// </summary>
    [CreateAssetMenu(fileName = "Item", menuName = "Akasha/Item")]
    public class ItemObjectScript : ScriptableObject, IItemObject
    {
        /// <summary>
        /// Item name string translation
        /// </summary>
        [SerializeField]
        private StringTranslationObjectScript itemNameStringTranslation = default;

        /// <summary>
        /// Icon sprite translation
        /// </summary>
        [SerializeField]
        private SpriteTranslationObjectScript iconSpriteTranslation = default;

        /// <summary>
        /// Description string translation
        /// </summary>
        [SerializeField]
        private StringTranslationObjectScript descriptionStringTranslation = default;

        /// <summary>
        /// Maximal health
        /// </summary>
        [SerializeField]
        private uint maximalHealth = default;

        /// <summary>
        /// Maximal stack size
        /// </summary>
        [SerializeField]
        private uint maximalStackSize = default;

        /// <summary>
        /// Weight
        /// </summary>
        [SerializeField]
        private uint weight = default;

        /// <summary>
        /// Maximal hit cooldown time
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float maximalHitCooldownTime = 0.5f;

        /// <summary>
        /// Item and hands asset
        /// </summary>
        [SerializeField]
        private GameObject itemAndHandsAsset = default;

        /// <summary>
        /// Item name
        /// </summary>
        public string ItemName => (itemNameStringTranslation ? itemNameStringTranslation.ToString() : string.Empty);

        /// <summary>
        /// Description
        /// </summary>
        public string Description => (descriptionStringTranslation ? descriptionStringTranslation.ToString() : string.Empty);

        /// <summary>
        /// Icon sprite
        /// </summary>
        public Sprite IconSprite => (iconSpriteTranslation ? iconSpriteTranslation.Sprite : null);

        /// <summary>
        /// Maximal health
        /// </summary>
        public uint MaximalHealth => maximalHealth;

        /// <summary>
        /// Maximal stack size
        /// </summary>
        public uint MaximalStackSize => maximalStackSize;

        /// <summary>
        /// Weight
        /// </summary>
        public uint Weight => weight;

        /// <summary>
        /// Maximal hit cooldown time
        /// </summary>
        public float MaximalHitCooldownTime => Mathf.Max(maximalHitCooldownTime, 0.0f);

        /// <summary>
        /// Item and hands asset
        /// </summary>
        public GameObject ItemAndHandsAsset => itemAndHandsAsset;

        /// <summary>
        /// Key
        /// </summary>
        public virtual string Key => $"Misc/{ name }";
    }
}

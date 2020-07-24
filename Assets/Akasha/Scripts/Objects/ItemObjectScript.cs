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
        /// Weight
        /// </summary>
        [SerializeField]
        private uint weight = default;

        /// <summary>
        /// Item name
        /// </summary>
        public string ItemName => (itemNameStringTranslation ? itemNameStringTranslation.ToString() : string.Empty);

        /// <summary>
        /// Icon sprite
        /// </summary>
        public Sprite IconSprite => (iconSpriteTranslation ? iconSpriteTranslation.Sprite : null);

        /// <summary>
        /// Weight
        /// </summary>
        public uint Weight => weight;
    }
}

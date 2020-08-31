using Akasha.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityTranslator.Objects;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Inventory item slot UI controller interface
    /// </summary>
    public interface IInventoryItemSlotUIController : IBehaviour
    {
        /// <summary>
        /// Unknown item icon image sprite translation
        /// </summary>
        SpriteTranslationObjectScript UnknownItemIconImageSpriteTranslation { get; set; }

        /// <summary>
        /// Weight string translation
        /// </summary>
        StringTranslationObjectScript WeightStringTranslation { get; set; }

        /// <summary>
        /// Description string translation
        /// </summary>
        StringTranslationObjectScript DescriptionStringTranslation { get; set; }

        /// <summary>
        /// Weight text format
        /// </summary>
        string WeightTextFormat { get; set; }

        /// <summary>
        /// Description text format
        /// </summary>
        string DescriptionTextFormat { get; set; }

        /// <summary>
        /// Empty health border color
        /// </summary>
        Color EmptyHealthBorderColor { get; set; }

        /// <summary>
        /// Empty health inner color
        /// </summary>
        Color EmptyHealthInnerColor { get; set; }

        /// <summary>
        /// Full health border color
        /// </summary>
        Color FullHealthBorderColor { get; set; }

        /// <summary>
        /// Full health inner color
        /// </summary>
        Color FullHealthInnerColor { get; set; }

        /// <summary>
        /// Item name text
        /// </summary>
        TextMeshProUGUI ItemNameText { get; set; }

        /// <summary>
        /// Icon image
        /// </summary>
        Image IconImage { get; set; }

        /// <summary>
        /// Health indicator radial progress
        /// </summary>
        RadialProgressUIControllerScript HealthIndicatorRadialProgress { get; set; }

        /// <summary>
        /// Icon image
        /// </summary>
        TextMeshProUGUI QuantityText { get; set; }

        /// <summary>
        /// Weight text
        /// </summary>
        TextMeshProUGUI WeightText { get; set; }

        /// <summary>
        /// Description text
        /// </summary>
        TextMeshProUGUI DescriptionText { get; set; }

        /// <summary>
        /// Selection indicator image
        /// </summary>
        Image SelectionIndicatorImage { get; set; }

        /// <summary>
        /// Inventory item data
        /// </summary>
        IInventoryItemData InventoryItemData { get; }

        /// <summary>
        /// Set values
        /// </summary>
        /// <param name="inventoryItemData">Inventory item data</param>
        /// <param name="parent">Parent</param>
        void SetValues(IInventoryItemData inventoryItemData, IInventoryUIController parent);

        /// <summary>
        /// Click
        /// </summary>
        void Select();
    }
}

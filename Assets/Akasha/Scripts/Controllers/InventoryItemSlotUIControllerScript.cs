using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityTranslator.Objects;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Inventory item slot UI controller script class
    /// </summary>
    public class InventoryItemSlotUIControllerScript : MonoBehaviour, IInventoryItemSlotUIController
    {
        /// <summary>
        /// Default weight text format
        /// </summary>
        private static readonly string defaultWeightTextFormat = "<b>{0}:</b> {1} kg";

        /// <summary>
        /// Default description text format
        /// </summary>
        private static readonly string defaultDescriptionTextFormat = $"<b>{{0}}:</b>{ Environment.NewLine }{{1}}";

        /// <summary>
        /// Inventory item slots
        /// </summary>
        private static readonly List<InventoryItemSlotUIControllerScript> inventoryItemSlots = new List<InventoryItemSlotUIControllerScript>();

        /// <summary>
        /// Unknown item icon image sprite translation
        /// </summary>
        [SerializeField]
        private SpriteTranslationObjectScript unknownItemIconImageSpriteTranslation = default;

        /// <summary>
        /// Weight string translation
        /// </summary>
        [SerializeField]
        private StringTranslationObjectScript weightStringTranslation = default;

        /// <summary>
        /// Description string translation
        /// </summary>
        [SerializeField]
        private StringTranslationObjectScript descriptionStringTranslation = default;

        /// <summary>
        /// Weight text format
        /// </summary>
        [SerializeField]
        private string weightTextFormat = defaultWeightTextFormat;

        /// <summary>
        /// Description text format
        /// </summary>
        [SerializeField]
        [TextArea]
        private string descriptionTextFormat = defaultDescriptionTextFormat;

        /// <summary>
        /// Empty health border color
        /// </summary>
        [SerializeField]
        private Color emptyHealthBorderColor = new Color(0.25f, 0.0f, 0.0f);

        /// <summary>
        /// Empty health inner color
        /// </summary>
        [SerializeField]
        private Color emptyHealthInnerColor = new Color(0.5f, 0.0f, 0.0f);

        /// <summary>
        /// Full health border color
        /// </summary>
        [SerializeField]
        private Color fullHealthBorderColor = new Color(0.0f, 0.25f, 0.0f);

        /// <summary>
        /// Full health inner color
        /// </summary>
        [SerializeField]
        private Color fullHealthInnerColor = new Color(0.0f, 0.5f, 0.0f);

        /// <summary>
        /// Item name text
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI itemNameText = default;

        /// <summary>
        /// Icon image
        /// </summary>
        [SerializeField]
        private Image iconImage = default;

        /// <summary>
        /// Health indicator radial progress
        /// </summary>
        [SerializeField]
        private RadialProgressUIControllerScript healthIndicatorRadialProgress = default;

        /// <summary>
        /// Icon image
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI quantityText = default;

        /// <summary>
        /// Weight text
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI weightText = default;

        /// <summary>
        /// Description text
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI descriptionText = default;

        /// <summary>
        /// Selection indicator image
        /// </summary>
        [SerializeField]
        private Image selectionIndicatorImage = default;

        /// <summary>
        /// Unknown item icon image sprite translation
        /// </summary>
        public SpriteTranslationObjectScript UnknownItemIconImageSpriteTranslation
        {
            get => unknownItemIconImageSpriteTranslation;
            set => unknownItemIconImageSpriteTranslation = value;
        }

        /// <summary>
        /// Weight string translation
        /// </summary>
        public StringTranslationObjectScript WeightStringTranslation
        {
            get => weightStringTranslation;
            set => weightStringTranslation = value;
        }

        /// <summary>
        /// Description string translation
        /// </summary>
        public StringTranslationObjectScript DescriptionStringTranslation
        {
            get => descriptionStringTranslation;
            set => descriptionStringTranslation = value;
        }

        /// <summary>
        /// Weight text format
        /// </summary>
        public string WeightTextFormat
        {
            get
            {
                if (weightTextFormat == null)
                {
                    weightTextFormat = defaultWeightTextFormat;
                }
                return weightTextFormat;
            }
            set => weightTextFormat = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Description text format
        /// </summary>
        public string DescriptionTextFormat
        {
            get
            {
                if (descriptionTextFormat == null)
                {
                    descriptionTextFormat = defaultDescriptionTextFormat;
                }
                return descriptionTextFormat;
            }
            set => descriptionTextFormat = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Empty health border color
        /// </summary>
        public Color EmptyHealthBorderColor
        {
            get => emptyHealthBorderColor;
            set => emptyHealthBorderColor = value;
        }

        /// <summary>
        /// Empty health inner color
        /// </summary>
        public Color EmptyHealthInnerColor
        {
            get => emptyHealthInnerColor;
            set => emptyHealthInnerColor = value;
        }

        /// <summary>
        /// Full health border color
        /// </summary>
        public Color FullHealthBorderColor
        {
            get => fullHealthBorderColor;
            set => fullHealthBorderColor = value;
        }

        /// <summary>
        /// Full health inner color
        /// </summary>
        public Color FullHealthInnerColor
        {
            get => fullHealthInnerColor;
            set => fullHealthInnerColor = value;
        }

        /// <summary>
        /// Item name text
        /// </summary>
        public TextMeshProUGUI ItemNameText
        {
            get => itemNameText;
            set => itemNameText = value;
        }

        /// <summary>
        /// Icon image
        /// </summary>
        public Image IconImage
        {
            get => iconImage;
            set => iconImage = value;
        }

        /// <summary>
        /// Health indicator radial progress
        /// </summary>
        public RadialProgressUIControllerScript HealthIndicatorRadialProgress
        {
            get => healthIndicatorRadialProgress;
            set => healthIndicatorRadialProgress = value;
        }

        /// <summary>
        /// Icon image
        /// </summary>
        public TextMeshProUGUI QuantityText
        {
            get => quantityText;
            set => quantityText = value;
        }

        /// <summary>
        /// Weight text
        /// </summary>
        public TextMeshProUGUI WeightText
        {
            get => weightText;
            set => weightText = value;
        }

        /// <summary>
        /// Description text
        /// </summary>
        public TextMeshProUGUI DescriptionText
        {
            get => descriptionText;
            set => descriptionText = value;
        }

        /// <summary>
        /// Selection indicator image
        /// </summary>
        public Image SelectionIndicatorImage
        {
            get => selectionIndicatorImage;
            set => selectionIndicatorImage = value;
        }

        /// <summary>
        /// Inventory item data
        /// </summary>
        public IInventoryItemData InventoryItemData { get; private set; }

        /// <summary>
        /// Parent
        /// </summary>
        public IInventoryUIController Parent { get; private set; }

        /// <summary>
        /// Set values
        /// </summary>
        /// <param name="inventoryItemData">Inventory item data</param>
        /// <param name="parent">Parent</param>
        public void SetValues(IInventoryItemData inventoryItemData, IInventoryUIController parent)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            InventoryItemData = inventoryItemData;
            if (itemNameText)
            {
                itemNameText.text = ((inventoryItemData == null) ? string.Empty : ((inventoryItemData.Item == null) ? string.Empty : inventoryItemData.Item.ItemName));
            }
            if (iconImage)
            {
                Sprite icon_image_sprite = inventoryItemData?.Item.IconSprite;
                iconImage.sprite = (icon_image_sprite ? icon_image_sprite : (unknownItemIconImageSpriteTranslation ? unknownItemIconImageSpriteTranslation.Sprite : null));
            }
            if (healthIndicatorRadialProgress)
            {
                float value = (((inventoryItemData != null) && (inventoryItemData.Item != null) && (inventoryItemData.Item.MaximalHealth > 0U)) ? ((float)(inventoryItemData.Health) / inventoryItemData.Item.MaximalHealth) : 0.0f);
                Vector3 new_border_color_vector = Vector3.Slerp(new Vector3(emptyHealthBorderColor.r, emptyHealthBorderColor.g, emptyHealthBorderColor.b), new Vector3(fullHealthBorderColor.r, fullHealthBorderColor.g, fullHealthBorderColor.b), value);
                Vector3 new_inner_color_vector = Vector3.Slerp(new Vector3(emptyHealthInnerColor.r, emptyHealthInnerColor.g, emptyHealthInnerColor.b), new Vector3(fullHealthInnerColor.r, fullHealthInnerColor.g, fullHealthInnerColor.b), value);
                healthIndicatorRadialProgress.BorderColor = new Color(new_border_color_vector.x, new_border_color_vector.y, new_border_color_vector.z, Mathf.Lerp(emptyHealthBorderColor.a, fullHealthBorderColor.a, value));
                healthIndicatorRadialProgress.InnerColor = new Color(new_inner_color_vector.x, new_inner_color_vector.y, new_inner_color_vector.z, Mathf.Lerp(emptyHealthInnerColor.a, fullHealthInnerColor.a, value));
                healthIndicatorRadialProgress.Value = value;
            }
            if (quantityText)
            {
                quantityText.text = ((inventoryItemData == null) ? string.Empty : ((inventoryItemData.Quantity > 1) ? inventoryItemData.Quantity.ToString() : string.Empty));
            }
            if (weightText)
            {
                weightText.text = string.Format(WeightTextFormat, weightStringTranslation ? weightStringTranslation.ToString() : string.Empty, (inventoryItemData == null) ? 0U : ((inventoryItemData.Item == null) ? 0U : inventoryItemData.Item.Weight * inventoryItemData.Quantity));
            }
            if (descriptionText)
            {
                descriptionText.text = string.Format(DescriptionTextFormat, descriptionStringTranslation ? descriptionStringTranslation.ToString() : string.Empty, (inventoryItemData == null) ? string.Empty : ((inventoryItemData.Item == null) ? string.Empty : inventoryItemData.Item.Description));
            }
        }

        /// <summary>
        /// Select
        /// </summary>
        public void Select()
        {
            foreach (InventoryItemSlotUIControllerScript inventory_item_slot in inventoryItemSlots)
            {
                if (inventory_item_slot && inventory_item_slot.SelectionIndicatorImage)
                {
                    inventory_item_slot.SelectionIndicatorImage.gameObject.SetActive(inventory_item_slot == this);
                }
            }
            Parent?.SelectInventoryItemSlot(InventoryItemData);
        }

        /// <summary>
        /// On enable
        /// </summary>
        private void OnEnable() => inventoryItemSlots.Add(this);

        /// <summary>
        /// On disable
        /// </summary>
        private void OnDisable() => inventoryItemSlots.Remove(this);
    }
}

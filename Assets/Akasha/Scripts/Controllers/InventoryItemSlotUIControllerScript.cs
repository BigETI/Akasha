using System;
using System.Diagnostics;
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
        private static readonly string defaultDescriptionTextFormat = "<b>{0}:</b>" + Environment.NewLine + "{1}";

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
        /// Empty health color
        /// </summary>
        [SerializeField]
        private Color emptyHealthColor = new Color(0.5f, 0.0f, 0.0f);

        /// <summary>
        /// Full health color
        /// </summary>
        [SerializeField]
        private Color fullHealthColor = new Color(0.0f, 0.5f, 0.0f);

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
        /// Health indicator image
        /// </summary>
        [SerializeField]
        private Image healthIndicatorImage = default;

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
        /// Empty health color
        /// </summary>
        public Color EmptyHealthColor
        {
            get => emptyHealthColor;
            set => emptyHealthColor = value;
        }

        /// <summary>
        /// Full health color
        /// </summary>
        public Color FullHealthColor
        {
            get => fullHealthColor;
            set => fullHealthColor = value;
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
        /// Health indicator image
        /// </summary>
        public Image HealthIndicatorImage
        {
            get => healthIndicatorImage;
            set => healthIndicatorImage = value;
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
        /// Selectable
        /// </summary>
        public Selectable Selectable { get; private set; }

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
                iconImage.sprite = inventoryItemData?.Item.IconSprite;
            }
            if (healthIndicatorImage)
            {
                float fill_amount = (((inventoryItemData != null) && (inventoryItemData.Item != null) && (inventoryItemData.Item.MaximalHealth > 0U)) ? ((float)(inventoryItemData.Health) / inventoryItemData.Item.MaximalHealth) : 0.0f);
                Vector3 new_color_vector = Vector3.Slerp(new Vector3(emptyHealthColor.r, emptyHealthColor.g, emptyHealthColor.b), new Vector3(fullHealthColor.r, fullHealthColor.g, fullHealthColor.b), fill_amount);
                healthIndicatorImage.color = new Color(new_color_vector.x, new_color_vector.y, new_color_vector.z, Mathf.Lerp(emptyHealthColor.a, fullHealthColor.a, fill_amount));
                healthIndicatorImage.fillAmount = fill_amount;
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
        /// Click
        /// </summary>
        public void Click() => Parent?.SelectInventoryItemSlot(InventoryItemData);

        /// <summary>
        /// Start
        /// </summary>
        private void Start() => Selectable = GetComponentInChildren<Selectable>(true);
    }
}

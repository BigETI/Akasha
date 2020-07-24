using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        /// Icon image
        /// </summary>
        [SerializeField]
        private Image iconImage = default;

        /// <summary>
        /// Icon image
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI quantityText = default;

        /// <summary>
        /// Icon image
        /// </summary>
        public Image IconImage
        {
            get => iconImage;
            set => iconImage = value;
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
        /// Selectable
        /// </summary>
        public Selectable Selectable { get; private set; }

        /// <summary>
        /// Inventory item data
        /// </summary>
        public IInventoryItemData InventoryItemData { get; private set; }

        /// <summary>
        /// Set values
        /// </summary>
        /// <param name="inventoryItemData">Inventory item data</param>
        public void SetValues(IInventoryItemData inventoryItemData)
        {
            InventoryItemData = inventoryItemData;
            if (iconImage)
            {
                iconImage.sprite = ((inventoryItemData == null) ? null : inventoryItemData.Item.IconSprite);
            }
            if (quantityText)
            {
                quantityText.text = ((inventoryItemData == null) ? string.Empty : ((inventoryItemData.Quantity > 1) ? inventoryItemData.Quantity.ToString() : string.Empty));
            }
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            Selectable = GetComponentInChildren<Selectable>(true);
        }
    }
}

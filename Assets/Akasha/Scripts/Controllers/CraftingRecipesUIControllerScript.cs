using Akasha.Data;
using Boo.Lang;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityTranslator.Objects;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Crafting recipes UI controller script class
    /// </summary>
    public class CraftingRecipesUIControllerScript : MonoBehaviour, ICraftingRecipesUIController
    {
        /// <summary>
        /// Default crafting recipes weight text format
        /// </summary>
        private static readonly string defaultCraftingRecipesWeightTextFormat = "<b>{0}:</b> {1} kg";

        /// <summary>
        /// Default crafting results weight text format
        /// </summary>
        private static readonly string defaultCraftingResultsWeightTextFormat = "<b>{0}:</b> {1} kg";

        /// <summary>
        /// Crafting recipes weight string translation
        /// </summary>
        [SerializeField]
        private StringTranslationObjectScript craftingRecipesWeightStringTranslation = default;

        /// <summary>
        /// Crafting results weight string translation
        /// </summary>
        [SerializeField]
        private StringTranslationObjectScript craftingResultsWeightStringTranslation = default;

        /// <summary>
        /// Crafting recipe asset
        /// </summary>
        [SerializeField]
        private GameObject craftingRecipeAsset = default;

        /// <summary>
        /// Crafting recipes weight text format
        /// </summary>
        [SerializeField]
        private string craftingRecipesWeightTextFormat = defaultCraftingRecipesWeightTextFormat;

        /// <summary>
        /// Crafting results weight text format
        /// </summary>
        [SerializeField]
        private string craftingResultsWeightTextFormat = defaultCraftingResultsWeightTextFormat;

        /// <summary>
        /// Crafting results text
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI craftingResultsText = default;

        /// <summary>
        /// Icon image
        /// </summary>
        [SerializeField]
        private Image iconImage = default;

        /// <summary>
        /// Crafting recipes weight text
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI craftingRecipesWeightText = default;

        /// <summary>
        /// Crafting results weight text
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI craftingResultsWeightText = default;

        /// <summary>
        /// Quantity selection
        /// </summary>
        [SerializeField]
        private QuantitySelectionUIControllerScript quantitySelection = default;

        /// <summary>
        /// Crafting recipes scroll view content rectangle transform
        /// </summary>
        [SerializeField]
        private RectTransform craftingRecipesScrollViewContentRectangleTransform = default;

        /// <summary>
        /// Crafting recipe UI controllers
        /// </summary>
        private readonly List<CraftingRecipeUIControllerScript> craftingRecipeUIControllers = new List<CraftingRecipeUIControllerScript>();

        /// <summary>
        /// Crafting recipes weight string translation
        /// </summary>
        public StringTranslationObjectScript CraftingRecipesWeightStringTranslation
        {
            get => craftingRecipesWeightStringTranslation;
            set => craftingRecipesWeightStringTranslation = value;
        }

        /// <summary>
        /// Crafting results weight string translation
        /// </summary>
        public StringTranslationObjectScript CraftingResultsWeightStringTranslation
        {
            get => craftingResultsWeightStringTranslation;
            set => craftingResultsWeightStringTranslation = value;
        }

        /// <summary>
        /// Crafting recipe asset
        /// </summary>
        public GameObject CraftingRecipeAsset
        {
            get => craftingRecipeAsset;
            set => craftingRecipeAsset = value;
        }

        /// <summary>
        /// Crafting recipes weight text format
        /// </summary>
        public string CraftingRecipesWeightTextFormat
        {
            get
            {
                if (craftingRecipesWeightTextFormat == null)
                {
                    craftingRecipesWeightTextFormat = defaultCraftingRecipesWeightTextFormat;
                }
                return craftingRecipesWeightTextFormat;
            }
            set => craftingRecipesWeightTextFormat = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Crafting results weight text format
        /// </summary>
        public string CraftingResultsWeightTextFormat
        {
            get
            {
                if (craftingResultsWeightTextFormat == null)
                {
                    craftingResultsWeightTextFormat = defaultCraftingResultsWeightTextFormat;
                }
                return craftingResultsWeightTextFormat;
            }
            set => craftingResultsWeightTextFormat = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Crafting results text
        /// </summary>
        public TextMeshProUGUI CraftingResultsText
        {
            get => craftingResultsText;
            set => craftingResultsText = value;
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
        /// Crafting recipes weight text
        /// </summary>
        public TextMeshProUGUI CraftingRecipesWeightText
        {
            get => craftingRecipesWeightText;
            set => craftingRecipesWeightText = value;
        }

        /// <summary>
        /// Crafting recipes weight text
        /// </summary>
        public TextMeshProUGUI CraftingResultsWeightText
        {
            get => craftingResultsWeightText;
            set => craftingResultsWeightText = value;
        }

        /// <summary>
        /// Quantity selection
        /// </summary>
        public QuantitySelectionUIControllerScript QuantitySelection
        {
            get => quantitySelection;
            set => quantitySelection = value;
        }

        /// <summary>
        /// Crafting recipes scroll view content rectangle transform
        /// </summary>
        public RectTransform CraftingRecipesScrollViewContentRectangleTransform
        {
            get => craftingRecipesScrollViewContentRectangleTransform;
            set => craftingRecipesScrollViewContentRectangleTransform = value;
        }

        /// <summary>
        /// Crafting recipes
        /// </summary>
        public ICraftingRecipesObject CraftingRecipes { get; private set; }

        /// <summary>
        /// Quantity
        /// </summary>
        public uint Quantity
        {
            get => (quantitySelection ? (uint)(Mathf.Max(quantitySelection.Quantity, 0)) : 0U);
            set
            {
                if (quantitySelection && (quantitySelection.Quantity != value))
                {
                    quantitySelection.Quantity = (int)value;
                    UpdateControls();
                }
            }
        }

        /// <summary>
        /// Is known
        /// </summary>
        public bool IsKnown { get; private set; }

        /// <summary>
        /// Parent
        /// </summary>
        public ICraftingUIController Parent { get; private set; }

        /// <summary>
        /// Update controls
        /// </summary>
        private void UpdateControls()
        {
            if (CraftingRecipes != null)
            {
                uint quantity = Quantity;
                if (craftingResultsText)
                {
                    craftingResultsText.text = CraftingRecipes.CraftingRecipesName;
                }
                if (iconImage)
                {
                    iconImage.sprite = CraftingRecipes.IconSprite;
                }
                if (craftingRecipesWeightText)
                {
                    craftingRecipesWeightText.text = string.Format(craftingRecipesWeightTextFormat, craftingRecipesWeightStringTranslation ? craftingRecipesWeightStringTranslation.ToString() : string.Empty, CraftingRecipes.CraftingRecipesWeight);
                }
                if (craftingResultsWeightText)
                {
                    craftingResultsWeightText.text = string.Format(craftingRecipesWeightTextFormat, craftingResultsWeightStringTranslation ? craftingResultsWeightStringTranslation.ToString() : string.Empty, CraftingRecipes.CraftingResultsWeight);
                }
                foreach (CraftingRecipeUIControllerScript crafting_recipe_ui_controller in craftingRecipeUIControllers)
                {
                    if (crafting_recipe_ui_controller)
                    {
                        Destroy(crafting_recipe_ui_controller.gameObject);
                    }
                }
                craftingRecipeUIControllers.Clear();
                if (craftingRecipeAsset && craftingRecipesScrollViewContentRectangleTransform)
                {
                    foreach (CraftingRecipeResultData crafting_recipe in CraftingRecipes.CraftingRecipes)
                    {
                        if (crafting_recipe.Item && (crafting_recipe.Quantity > 0U))
                        {
                            GameObject game_object = Instantiate(craftingRecipeAsset);
                            if (game_object)
                            {
                                if (game_object.TryGetComponent(out RectTransform rectangle_transform) && game_object.TryGetComponent(out CraftingRecipeUIControllerScript crafting_recipe_ui_controller))
                                {
                                    rectangle_transform.SetParent(craftingRecipesScrollViewContentRectangleTransform, false);
                                    crafting_recipe_ui_controller.SetValues(crafting_recipe, quantity);
                                    craftingRecipeUIControllers.Add(crafting_recipe_ui_controller);
                                }
                                else
                                {
                                    Destroy(game_object);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Set values
        /// </summary>
        /// <param name="craftingRecipes">Crafting recipes</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="isKnown">Is known</param>
        /// <param name="parent">Parent</param>
        public void SetValues(ICraftingRecipesObject craftingRecipes, uint quantity, bool isKnown, ICraftingUIController parent)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            CraftingRecipes = craftingRecipes;
            IsKnown = isKnown;
            if (quantitySelection)
            {
                quantitySelection.SetQuantityWithoutNotification((int)quantity);
            }
            //if (TryGetComponent(out Selectable selectable))
            //{
            //    selectable.interactable = isKnown;
            //}
            UpdateControls();
        }

        /// <summary>
        /// Click
        /// </summary>
        public void Click()
        {
            if (CraftingRecipes != null)
            {
                Parent?.SelectCraftingRecipes(CraftingRecipes);
            }
        }

        /// <summary>
        /// Craft
        /// </summary>
        public void Craft()
        {
            uint quantity = Quantity;
            if (IsKnown && (CraftingRecipes != null) && (quantity > 0U) && (Parent != null) && (Parent.CharacterController != null))
            {
                Parent.CharacterController.Inventory.CraftItems(CraftingRecipes, quantity);
            }
        }
    }
}

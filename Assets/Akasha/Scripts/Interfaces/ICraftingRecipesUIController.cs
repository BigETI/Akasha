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
    /// Crafting recipes UI controller interface
    /// </summary>
    public interface ICraftingRecipesUIController : IBehaviour
    {
        /// <summary>
        /// Crafting recipes weight string translation
        /// </summary>
        StringTranslationObjectScript CraftingRecipesWeightStringTranslation { get; set; }

        /// <summary>
        /// Crafting results weight string translation
        /// </summary>
        StringTranslationObjectScript CraftingResultsWeightStringTranslation { get; set; }

        /// <summary>
        /// Crafting recipe asset
        /// </summary>
        GameObject CraftingRecipeAsset { get; set; }

        /// <summary>
        /// Crafting recipes weight text format
        /// </summary>
        string CraftingRecipesWeightTextFormat { get; set; }

        /// <summary>
        /// Crafting results weight text format
        /// </summary>
        string CraftingResultsWeightTextFormat { get; set; }

        /// <summary>
        /// Crafting results text
        /// </summary>
        TextMeshProUGUI CraftingResultsText { get; set; }

        /// <summary>
        /// Icon image
        /// </summary>
        Image IconImage { get; set; }

        /// <summary>
        /// Crafting recipes weight text
        /// </summary>
        TextMeshProUGUI CraftingRecipesWeightText { get; set; }

        /// <summary>
        /// Crafting recipes weight text
        /// </summary>
        TextMeshProUGUI CraftingResultsWeightText { get; set; }

        /// <summary>
        /// Quantity selection
        /// </summary>
        QuantitySelectionUIControllerScript QuantitySelection { get; set; }

        /// <summary>
        /// Crafting recipes scroll view content rectangle transform
        /// </summary>
        RectTransform CraftingRecipesScrollViewContentRectangleTransform { get; set; }

        /// <summary>
        /// Crafting recipes
        /// </summary>
        ICraftingRecipesObject CraftingRecipes { get; }

        /// <summary>
        /// Quantity
        /// </summary>
        uint Quantity { get; set; }

        /// <summary>
        /// Set values
        /// </summary>
        /// <param name="craftingRecipes">Crafting recipes</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="isKnown">Is known</param>
        /// <param name="parent">Parent</param>
        void SetValues(ICraftingRecipesObject craftingRecipes, uint quantity, bool isKnown, ICraftingUIController parent);

        /// <summary>
        /// Click
        /// </summary>
        void Click();

        /// <summary>
        /// Craft
        /// </summary>
        void Craft();
    }
}

using TMPro;
using UnityEngine.UI;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Crafting recipe UI controller interface
    /// </summary>
    public interface ICraftingRecipeUIController : IBehaviour
    {
        /// <summary>
        /// Icon image
        /// </summary>
        Image IconImage { get; set; }

        /// <summary>
        /// Recipe text
        /// </summary>
        TextMeshProUGUI RecipeText { get; set; }

        /// <summary>
        /// Recipe text format
        /// </summary>
        string RecipeTextFormat { get; set; }

        /// <summary>
        /// Crafting recipe
        /// </summary>
        ICraftingRecipeResultData CraftingRecipe { get; }

        /// <summary>
        /// Quantity multiplicator
        /// </summary>
        uint QuantityMultiplicator { get; }

        /// <summary>
        /// Set values
        /// </summary>
        /// <param name="craftingRecipe">Crafting recipe</param>
        /// <param name="quantityMultiplicator">Quantity multiplicator</param>
        void SetValues(ICraftingRecipeResultData craftingRecipe, uint quantityMultiplicator);
    }
}

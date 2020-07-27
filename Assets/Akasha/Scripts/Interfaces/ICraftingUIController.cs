using Akasha.Controllers;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Crafting UI controller interface
    /// </summary>
    public interface ICraftingUIController
    {
        /// <summary>
        /// Crafting recipes button asset
        /// </summary>
        GameObject CraftingRecipesButtonAsset { get; set; }

        /// <summary>
        /// Crafting panel rectangle transform
        /// </summary>
        RectTransform CraftingPanelRectangleTransform { get; set; }

        /// <summary>
        /// Crafting recipes UI controller
        /// </summary>
        CraftingRecipesUIControllerScript CraftingRecipesUIController { get; set; }

        /// <summary>
        /// Character controller
        /// </summary>
        ICharacterController CharacterController { get; }

        /// <summary>
        /// Select crafting recipes
        /// </summary>
        /// <param name="craftingRecipes">Crafting recipes</param>
        void SelectCraftingRecipes(ICraftingRecipesObject craftingRecipes);

        /// <summary>
        /// Deselect crafting recipes
        /// </summary>
        void DeselectCraftingRecipes();
    }
}

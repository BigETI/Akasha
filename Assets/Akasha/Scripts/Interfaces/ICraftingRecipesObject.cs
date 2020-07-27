using Akasha.Data;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Crafting recipes object interface
    /// </summary>
    public interface ICraftingRecipesObject : IScriptableObject
    {
        /// <summary>
        /// Crafting recipes name
        /// </summary>
        string CraftingRecipesName { get; }

        /// <summary>
        /// Icon sprite
        /// </summary>
        Sprite IconSprite { get; }

        /// <summary>
        /// Crafting recipies
        /// </summary>
        IReadOnlyList<CraftingRecipeResultData> CraftingRecipes { get; }

        /// <summary>
        /// Crafting results
        /// </summary>
        IReadOnlyList<CraftingRecipeResultData> CraftingResults { get; }

        /// <summary>
        /// Crafting recipes weight
        /// </summary>
        uint CraftingRecipesWeight { get; }

        /// <summary>
        /// Crafting results weight
        /// </summary>
        uint CraftingResultsWeight { get; }
    }
}

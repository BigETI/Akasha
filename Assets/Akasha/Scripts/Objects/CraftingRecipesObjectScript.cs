using Akasha.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityTranslator.Objects;

/// <summary>
/// Akasha objects namespace
/// </summary>
namespace Akasha.Objects
{
    /// <summary>
    /// Crafting recipes object script class
    /// </summary>
    [CreateAssetMenu(fileName = "CraftingRecipe", menuName = "Akasha/Crafting recipes")]
    public class CraftingRecipesObjectScript : ScriptableObject, ICraftingRecipesObject
    {
        /// <summary>
        /// Crafting recipes name string translation
        /// </summary>
        [SerializeField]
        private StringTranslationObjectScript craftingRecipesNameStringTranslation = default;

        /// <summary>
        /// Icon sprite translation
        /// </summary>
        [SerializeField]
        private SpriteTranslationObjectScript iconSpriteTranslation = default;

        /// <summary>
        /// Crafting recipes
        /// </summary>
        [SerializeField]
        private CraftingRecipeResultData[] craftingRecipes = Array.Empty<CraftingRecipeResultData>();

        /// <summary>
        /// Crafting results
        /// </summary>
        [SerializeField]
        private CraftingRecipeResultData[] craftingResults = Array.Empty<CraftingRecipeResultData>();

        /// <summary>
        /// Crafting recipes name
        /// </summary>
        public string CraftingRecipesName => (craftingRecipesNameStringTranslation ? craftingRecipesNameStringTranslation.ToString() : string.Empty);

        /// <summary>
        /// Icon sprite
        /// </summary>
        public Sprite IconSprite => (iconSpriteTranslation ? iconSpriteTranslation.Sprite : null);

        /// <summary>
        /// Crafting recipes
        /// </summary>
        public IReadOnlyList<CraftingRecipeResultData> CraftingRecipes
        {
            get
            {
                if (craftingRecipes == null)
                {
                    craftingRecipes = Array.Empty<CraftingRecipeResultData>();
                }
                return craftingRecipes;
            }
        }

        /// <summary>
        /// Crafting results
        /// </summary>
        public IReadOnlyList<CraftingRecipeResultData> CraftingResults
        {
            get
            {
                if (craftingResults == null)
                {
                    craftingResults = Array.Empty<CraftingRecipeResultData>();
                }
                return craftingResults;
            }
        }

        /// <summary>
        /// Crafting recipes weight
        /// </summary>
        public uint CraftingRecipesWeight
        {
            get
            {
                uint ret = 0U;
                foreach (CraftingRecipeResultData crafting_recipe in CraftingRecipes)
                {
                    if (crafting_recipe.Item && (crafting_recipe.Item.Weight > 0U) && (crafting_recipe.Quantity > 0U))
                    {
                        ret += crafting_recipe.Item.Weight * crafting_recipe.Quantity;
                    }
                }
                return ret;
            }
        }

        /// <summary>
        /// Crafting results weight
        /// </summary>
        public uint CraftingResultsWeight
        {
            get
            {
                uint ret = 0U;
                foreach (CraftingRecipeResultData crafting_result in CraftingResults)
                {
                    if (crafting_result.Item && (crafting_result.Item.Weight > 0U) && (crafting_result.Quantity > 0U))
                    {
                        ret += crafting_result.Item.Weight * crafting_result.Quantity;
                    }
                }
                return ret;
            }
        }
    }
}

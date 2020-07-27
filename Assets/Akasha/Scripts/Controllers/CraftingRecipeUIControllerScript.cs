using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Crafting recipe UI controller script class
    /// </summary>
    public class CraftingRecipeUIControllerScript : MonoBehaviour, ICraftingRecipeUIController
    {
        /// <summary>
        /// Default recipe text format
        /// </summary>
        private static readonly string defaultRecipeTextFormat = "{0}" + Environment.NewLine + "<b>x{1}</b>";

        /// <summary>
        /// Icon image
        /// </summary>
        [SerializeField]
        private Image iconImage = default;

        /// <summary>
        /// Recipe text
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI recipeText = default;

        /// <summary>
        /// Recipe text format
        /// </summary>
        [SerializeField]
        [TextArea]
        private string recipeTextFormat = defaultRecipeTextFormat;

        /// <summary>
        /// Icon image
        /// </summary>
        public Image IconImage
        {
            get => iconImage;
            set => iconImage = value;
        }

        /// <summary>
        /// Recipe text
        /// </summary>
        public TextMeshProUGUI RecipeText
        {
            get => recipeText;
            set => recipeText = value;
        }

        /// <summary>
        /// Recipe text format
        /// </summary>
        public string RecipeTextFormat
        {
            get
            {
                if (recipeTextFormat == null)
                {
                    recipeTextFormat = defaultRecipeTextFormat;
                }
                return recipeTextFormat;
            }
            set
            {
                recipeTextFormat = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <summary>
        /// Crafting recipe
        /// </summary>
        public ICraftingRecipeResultData CraftingRecipe { get; private set; }

        /// <summary>
        /// Quantity multiplicator
        /// </summary>
        public uint QuantityMultiplicator { get; private set; }

        /// <summary>
        /// Set values
        /// </summary>
        /// <param name="craftingRecipe">Crafting recipe</param>
        /// <param name="quantityMultiplicator">Quantity multiplicator</param>
        public void SetValues(ICraftingRecipeResultData craftingRecipe, uint quantityMultiplicator)
        {
            CraftingRecipe = craftingRecipe ?? throw new ArgumentNullException(nameof(craftingRecipe));
            QuantityMultiplicator = quantityMultiplicator;
            if (iconImage)
            {
                iconImage.sprite = (craftingRecipe.Item ? craftingRecipe.Item.IconSprite : null);
            }
            if (recipeText)
            {
                recipeText.text = string.Format(RecipeTextFormat, (craftingRecipe.Item ? craftingRecipe.Item.ItemName : string.Empty), craftingRecipe.Quantity * quantityMultiplicator);
            }
        }
    }
}

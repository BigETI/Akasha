using Akasha.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Crafting UI controller script class
    /// </summary>
    public class CraftingUIControllerScript : MonoBehaviour, ICraftingUIController
    {
        /// <summary>
        /// Crafting recipes button asset
        /// </summary>
        [SerializeField]
        private GameObject craftingRecipesButtonAsset = default;

        /// <summary>
        /// Crafting panel rectangle transform
        /// </summary>
        [SerializeField]
        private RectTransform craftingPanelRectangleTransform;

        /// <summary>
        /// Crafting recipes UI controller
        /// </summary>
        [SerializeField]
        private CraftingRecipesUIControllerScript craftingRecipesUIController;

        /// <summary>
        /// Crafting recipes game objects
        /// </summary>
        private readonly List<GameObject> craftingRecipesGameObjects = new List<GameObject>();

        /// <summary>
        /// Crafting recipes button asset
        /// </summary>
        public GameObject CraftingRecipesButtonAsset
        {
            get => craftingRecipesButtonAsset;
            set => craftingRecipesButtonAsset = value;
        }

        /// <summary>
        /// Crafting panel rectangle transform
        /// </summary>
        public RectTransform CraftingPanelRectangleTransform
        {
            get => craftingPanelRectangleTransform;
            set => craftingPanelRectangleTransform = value;
        }

        /// <summary>
        /// Crafting recipes UI controller
        /// </summary>
        public CraftingRecipesUIControllerScript CraftingRecipesUIController
        {
            get => craftingRecipesUIController;
            set => craftingRecipesUIController = value;
        }

        /// <summary>
        /// Character controller
        /// </summary>
        public ICharacterController CharacterController { get; private set; }

        /// <summary>
        /// Select crafting recipes
        /// </summary>
        /// <param name="craftingRecipes">Crafting recipes</param>
        public void SelectCraftingRecipes(ICraftingRecipesObject craftingRecipes)
        {
            if (craftingRecipesUIController)
            {
                craftingRecipesUIController.gameObject.SetActive(craftingRecipes != null);
                craftingRecipesUIController.SetValues(craftingRecipes, 1U, (craftingRecipes != null) && (CharacterController != null) && CharacterController.KnownCraftingRecipesLookup.ContainsKey(craftingRecipes.name), this);
            }
        }

        /// <summary>
        /// Deselect crafting recipes
        /// </summary>
        public void DeselectCraftingRecipes() => SelectCraftingRecipes(null);

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            PlayerControllerScript player_controller = FindObjectOfType<PlayerControllerScript>();
            if (player_controller)
            {
                CharacterController = player_controller.GetComponent<CharacterControllerScript>();
            }
            foreach (GameObject crafting_recuipes_game_object in craftingRecipesGameObjects)
            {
                if (crafting_recuipes_game_object)
                {
                    Destroy(crafting_recuipes_game_object);
                }
            }
            craftingRecipesGameObjects.Clear();
            if (craftingPanelRectangleTransform && craftingRecipesButtonAsset)
            {
                CraftingRecipesObjectScript[] crafting_recipes_array = Resources.LoadAll<CraftingRecipesObjectScript>("CraftingRecipes");
                if (crafting_recipes_array != null)
                {
                    foreach (CraftingRecipesObjectScript crafting_recipes in crafting_recipes_array)
                    {
                        if (crafting_recipes)
                        {
                            GameObject game_object = Instantiate(craftingRecipesButtonAsset);
                            if (game_object)
                            {
                                if (game_object.TryGetComponent(out RectTransform rectangle_transform) && game_object.TryGetComponent(out CraftingRecipesUIControllerScript crafting_recipes_ui_controller))
                                {
                                    rectangle_transform.SetParent(craftingPanelRectangleTransform, false);
                                    crafting_recipes_ui_controller.SetValues(crafting_recipes, 1U, (CharacterController != null) && CharacterController.KnownCraftingRecipesLookup.ContainsKey(crafting_recipes.name), this);
                                    craftingRecipesGameObjects.Add(game_object);
                                }
                                else
                                {
                                    Debug.LogError("Game object must contain \"" + nameof(RectTransform) + "\" and \"" + nameof(CraftingRecipesUIControllerScript) + "\" components.");
                                }
                            }
                            else
                            {
                                Debug.LogError("Failed to instantiate game object.");
                            }
                        }
                    }
                }
            }
        }
    }
}

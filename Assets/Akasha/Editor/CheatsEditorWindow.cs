using Akasha;
using Akasha.Controllers;
using Akasha.Objects;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Akasha editor namespace
/// </summary>
namespace AkashaEditor
{
    /// <summary>
    /// Cheats editor window class
    /// </summary>
    public class CheatsEditorWindow : EditorWindow
    {
        /// <summary>
        /// Item
        /// </summary>
        private ItemObjectScript item;

        /// <summary>
        /// Crafting recipes
        /// </summary>
        private CraftingRecipesObjectScript craftingRecipes;

        /// <summary>
        /// Item quantity
        /// </summary>
        private int itemQuantity;

        /// <summary>
        /// Character controller
        /// </summary>
        private CharacterControllerScript characterController;

        /// <summary>
        /// Show window
        /// </summary>
        [MenuItem("Akasha/Cheats")]
        public static void ShowWindow()
        {
            GetWindow<CheatsEditorWindow>("Akasha cheats");
        }

        /// <summary>
        /// On GUI
        /// </summary>
        private void OnGUI()
        {
            if (Application.isPlaying)
            {
                if (!characterController)
                {
                    PlayerControllerScript player_controller = FindObjectOfType<PlayerControllerScript>();
                    if (player_controller)
                    {
                        characterController = player_controller.GetComponent<CharacterControllerScript>();
                    }
                }
                if (characterController)
                {
                    item = EditorGUILayout.ObjectField("Item", item, typeof(ItemObjectScript), false) as ItemObjectScript;
                    itemQuantity = EditorGUILayout.IntSlider(itemQuantity, 1, 1000);
                    if (item && (itemQuantity > 0) && GUILayout.Button("Spawn items"))
                    {
                        characterController.Inventory.AddItems(item, (uint)itemQuantity);
                    }
                    GUILayout.Space(32.0f);
                    craftingRecipes = EditorGUILayout.ObjectField("Crafting Recipe", craftingRecipes, typeof(CraftingRecipesObjectScript), false) as CraftingRecipesObjectScript;
                    if (craftingRecipes && GUILayout.Button("Learn crafting recipes"))
                    {
                        characterController.LearnCraftingRecipes(craftingRecipes);
                    }
                }
            }
            else
            {
                characterController = null;
            }
        }
    }
}

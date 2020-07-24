using Akasha;
using Akasha.Controllers;
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
        /// Item object
        /// </summary>
        private BlockObjectScript itemObject;

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
                    itemObject = EditorGUILayout.ObjectField("Item", itemObject, typeof(BlockObjectScript), false) as BlockObjectScript;
                    itemQuantity = EditorGUILayout.IntSlider(itemQuantity, 1, 1000);
                    if (itemObject && (itemQuantity > 0) && GUILayout.Button("Spawn items"))
                    {
                        characterController.Inventory.AddItems(itemObject, (uint)itemQuantity);
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

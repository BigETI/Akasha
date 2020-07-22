using Akasha;
using Akasha.Objects;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Akasha editor namesspace
/// </summary>
namespace AkashaEditor
{
    /// <summary>
    /// Block object editor script class
    /// </summary>
    [CustomEditor(typeof(BlockMeshVariantsObjectScript))]
    public class BlockVariantsObjectEditorScript : Editor
    {
        /// <summary>
        /// Draw block field
        /// </summary>
        /// <param name="blockAssetsSerializedProperty">Block assets serialized property</param>
        /// <param name="directionFlags">Direction flags</param>
        private void DrawBlockField(SerializedProperty blockAssetsSerializedProperty, EDirectionFlags directionFlags)
        {
            SerializedProperty block_asset_serialized_property = blockAssetsSerializedProperty.GetArrayElementAtIndex((int)(directionFlags));
            if (block_asset_serialized_property != null)
            {
                EditorGUILayout.PropertyField(block_asset_serialized_property, new GUIContent(directionFlags.ToString()));
            }
        }

        /// <summary>
        /// On inspector GUI
        /// </summary>
        public override void OnInspectorGUI()
        {
            SerializedProperty block_assets_serialized_property = serializedObject.FindProperty("blockAssets");
            SerializedProperty close_to_mesh_variants_serialized_property = serializedObject.FindProperty("closeToMeshVariants");
            if (block_assets_serialized_property != null)
            {
                if ((block_assets_serialized_property.isArray) && (block_assets_serialized_property.arrayElementType.Contains(nameof(GameObject))) && (close_to_mesh_variants_serialized_property != null))
                {
                    GUILayout.Label("Zero sides connected");
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Nothing);
                    GUILayout.Space(10);
                    GUILayout.Label("One side connected");
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Bottom);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Left);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Right);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Front);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Behind);
                    GUILayout.Space(10);
                    GUILayout.Label("Two sides connected");
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Bottom);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Left);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Right);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Front);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Bottom | EDirectionFlags.Left);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Bottom | EDirectionFlags.Right);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Bottom | EDirectionFlags.Front);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Bottom | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Left | EDirectionFlags.Right);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Left | EDirectionFlags.Front);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Left | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Right | EDirectionFlags.Front);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Right | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Front | EDirectionFlags.Behind);
                    GUILayout.Space(10);
                    GUILayout.Label("Three sides connected");
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Bottom | EDirectionFlags.Left);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Bottom | EDirectionFlags.Right);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Bottom | EDirectionFlags.Front);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Bottom | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Left | EDirectionFlags.Right);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Left | EDirectionFlags.Front);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Left | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Right | EDirectionFlags.Front);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Right | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Front | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Bottom | EDirectionFlags.Left | EDirectionFlags.Right);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Bottom | EDirectionFlags.Left | EDirectionFlags.Front);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Bottom | EDirectionFlags.Left | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Bottom | EDirectionFlags.Right | EDirectionFlags.Front);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Bottom | EDirectionFlags.Right | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Bottom | EDirectionFlags.Front | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Left | EDirectionFlags.Right | EDirectionFlags.Front);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Left | EDirectionFlags.Right | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Left | EDirectionFlags.Front | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Right | EDirectionFlags.Front | EDirectionFlags.Behind);
                    GUILayout.Space(10);
                    GUILayout.Label("Four sides connected");
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Bottom | EDirectionFlags.Left | EDirectionFlags.Right);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Bottom | EDirectionFlags.Left | EDirectionFlags.Front);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Bottom | EDirectionFlags.Left | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Bottom | EDirectionFlags.Right | EDirectionFlags.Front);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Bottom | EDirectionFlags.Right | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Bottom | EDirectionFlags.Front | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Left | EDirectionFlags.Right | EDirectionFlags.Front);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Left | EDirectionFlags.Right | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Left | EDirectionFlags.Front | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Right | EDirectionFlags.Front | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Bottom | EDirectionFlags.Left | EDirectionFlags.Right | EDirectionFlags.Front);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Bottom | EDirectionFlags.Left | EDirectionFlags.Right | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Bottom | EDirectionFlags.Left | EDirectionFlags.Front | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Bottom | EDirectionFlags.Right | EDirectionFlags.Front | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Left | EDirectionFlags.Right | EDirectionFlags.Front | EDirectionFlags.Behind);
                    GUILayout.Space(10);
                    GUILayout.Label("Five sides connected");
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Bottom | EDirectionFlags.Left | EDirectionFlags.Right | EDirectionFlags.Front);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Bottom | EDirectionFlags.Left | EDirectionFlags.Right | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Bottom | EDirectionFlags.Left | EDirectionFlags.Front | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Bottom | EDirectionFlags.Right | EDirectionFlags.Front | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Left | EDirectionFlags.Right | EDirectionFlags.Front | EDirectionFlags.Behind);
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Bottom | EDirectionFlags.Left | EDirectionFlags.Right | EDirectionFlags.Front | EDirectionFlags.Behind);
                    GUILayout.Space(10);
                    GUILayout.Label("Six sides connected");
                    DrawBlockField(block_assets_serialized_property, EDirectionFlags.Top | EDirectionFlags.Bottom | EDirectionFlags.Left | EDirectionFlags.Right | EDirectionFlags.Front | EDirectionFlags.Behind);
                    EditorGUILayout.PropertyField(close_to_mesh_variants_serialized_property);
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}

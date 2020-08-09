using Akasha;
using Akasha.Managers;
using Akasha.Objects;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Akasha editor namespace
/// </summary>
namespace AkashaEditor
{
    /// <summary>
    /// Blocks prefab object editor script
    /// </summary>
    [CustomEditor(typeof(BlocksPrefabObjectScript))]
    public class BlocksPrefabObjectEditorScript : Editor
    {
        /// <summary>
        /// On inspector GUI
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (target is IBlocksPrefabObject blocks_prefab)
            {
                blocks_prefab.Initialize();
                WorldManagerScript world_manager = WorldManagerScript.Instance;
                if (world_manager && world_manager.FollowTransformController)
                {
                    bool is_showing_grid = (world_manager.ShowingBlocksPrefab == blocks_prefab);
                    if (GUILayout.Toggle(is_showing_grid, "Show grid"))
                    {
                        world_manager.ShowingBlocksPrefab = blocks_prefab;
                    }
                    else if (is_showing_grid)
                    {
                        world_manager.ShowingBlocksPrefab = null;
                    }
                    if (GUILayout.Button("Spawn in world"))
                    {
                        BlockID blockID = world_manager.FollowTransformController.BlockID;
                        world_manager.SetBlocks(new BlockID(blockID.X + blocks_prefab.Offset.x, blockID.Y + blocks_prefab.Offset.y, blockID.Z + blocks_prefab.Offset.z), blocks_prefab.Size, blocks_prefab.Blocks, ESetBlocksOperation.FillEmpty);
                    }
                    if (GUILayout.Button("Save from world"))
                    {
                        // TODO
                    }
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}

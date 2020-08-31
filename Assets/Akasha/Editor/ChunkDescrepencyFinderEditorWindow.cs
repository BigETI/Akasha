using Akasha;
using Akasha.Data;
using Akasha.Managers;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.WSA;

/// <summary>
/// Akasha editor namespace
/// </summary>
namespace AkashaEditor
{
    /// <summary>
    /// Chunk descrepency finder editor script class
    /// </summary>
    public class ChunkDescrepencyFinderEditorWindow : EditorWindow
    {
        /// <summary>
        /// Chunk ID
        /// </summary>
        private ChunkID chunkID;

        /// <summary>
        /// File block data
        /// </summary>
        private Task<BlockData[]> fileBlocksTask;

        /// <summary>
        /// Generated blocks task
        /// </summary>
        private Task<BlockData[]> generatedBlocksTask;

        /// <summary>
        /// Output
        /// </summary>
        private string output = string.Empty;

        /// <summary>
        /// Show window
        /// </summary>
        [MenuItem("Akasha/Chunk descrepency finder")]
        public static void ShowWindow()
        {
            GetWindow<ChunkDescrepencyFinderEditorWindow>("Akasha chunk descrepency finder");
        }

        /// <summary>
        /// On GUI
        /// </summary>
        private void OnGUI()
        {
            WorldManagerScript world_manager = WorldManagerScript.Instance;
            if (world_manager)
            {
                chunkID = (ChunkID)(EditorGUILayout.Vector3IntField("Chunk ID", (Vector3Int)chunkID));
                if (GUILayout.Button("Find discrepencies"))
                {
                    fileBlocksTask = world_manager.IO.CreateReadChunkBlocksTask(chunkID);
                    generatedBlocksTask = world_manager.GetGeneratedBlocksTask(chunkID);
                }
                GUILayout.Label($"File blocks task status: { ((fileBlocksTask == null) ? "Not running" : fileBlocksTask.Status.ToString()) }");
                GUILayout.Label($"Generated blocks task status: { ((generatedBlocksTask == null) ? "Not running" : generatedBlocksTask.Status.ToString()) }");
                if ((fileBlocksTask != null) && (generatedBlocksTask != null))
                {
                    if (fileBlocksTask.IsCompleted && generatedBlocksTask.IsCompleted)
                    {
                        StringBuilder output_string_builder = new StringBuilder();
                        BlockData[] file_blocks = fileBlocksTask.Result;
                        BlockData[] generated_blocks = generatedBlocksTask.Result;
                        fileBlocksTask = null;
                        generatedBlocksTask = null;
                        output_string_builder.AppendLine($"File block count: { file_blocks.Length }");
                        output_string_builder.AppendLine($"Generated block count: { generated_blocks.Length }");
                        if (file_blocks.Length == generated_blocks.Length)
                        {
                            for (int index = 0; index < file_blocks.Length; index++)
                            {
                                BlockData file_block = file_blocks[index];
                                BlockData generated_block = generated_blocks[index];
                                if (file_block != generated_block)
                                {
                                    output_string_builder.AppendLine($"File block is { (file_block.IsABlock ? file_block.Block.Key : "nothing") } with { file_block.Health } health but generated block is { (generated_block.IsABlock ? generated_block.Block.Key : "nothing") } with { generated_block.Health } health.");
                                }
                            }
                            output_string_builder.Append("Finished comparing all blocks.");
                        }
                        else
                        {
                            output_string_builder.Append("File blocks count does not match generated block count.");
                        }
                        output = output_string_builder.ToString();
                        output_string_builder.Clear();
                    }
                }
                GUILayout.TextArea(output);
            }
        }
    }
}

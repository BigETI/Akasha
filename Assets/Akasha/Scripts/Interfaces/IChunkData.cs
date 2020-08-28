using Akasha.Data;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Chunk data interface
    /// </summary>
    public interface IChunkData : IValidable
    {
        /// <summary>
        /// Chunk ID
        /// </summary>
        ChunkID ChunkID { get; set; }

        /// <summary>
        /// Blocks
        /// </summary>
        BlockData[] Blocks { get; set; }
    }
}

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Chunk controller interface
    /// </summary>
    public interface IChunkController
    {
        /// <summary>
        /// Chunk ID
        /// </summary>
        ChunkID ChunkID { get; set; }

        /// <summary>
        /// Refresh
        /// </summary>
        void Refresh();
    }
}

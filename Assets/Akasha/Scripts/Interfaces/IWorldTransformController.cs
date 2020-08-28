using UnityEngine;
/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// World transform controller interface
    /// </summary>
    public interface IWorldTransformController : IBehaviour
    {
        /// <summary>
        /// Chunk ID
        /// </summary>
        ChunkID ChunkID { get; set; }

        /// <summary>
        /// Block ID
        /// </summary>
        BlockID BlockID { get; set; }

        /// <summary>
        /// Position offset
        /// </summary>
        Vector3 PositionOffset { get; set; }

        /// <summary>
        /// Get block ID
        /// </summary>
        /// <param name="worldPosition">World position</param>
        /// <returns>Block ID</returns>
        BlockID GetBlockID(Vector3 worldPosition);
    }
}

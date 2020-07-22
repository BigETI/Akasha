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
        /// Grid position
        /// </summary>
        ChunkID ChunkID { get; set; }
    }
}

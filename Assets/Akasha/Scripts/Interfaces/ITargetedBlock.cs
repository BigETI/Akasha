using Akasha.Data;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Targeted block interface
    /// </summary>
    public interface ITargetedBlock
    {
        /// <summary>
        /// Block
        /// </summary>
        BlockData Block { get; }

        /// <summary>
        /// Block ID
        /// </summary>
        BlockID ID { get; }

        /// <summary>
        /// Is a block
        /// </summary>
        bool IsABlock { get; }

        /// <summary>
        /// Is nothing
        /// </summary>
        bool IsNothing { get; }
    }
}

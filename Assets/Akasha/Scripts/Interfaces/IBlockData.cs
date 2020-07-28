using Akasha.Objects;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Block data interface
    /// </summary>
    public interface IBlockData
    {
        /// <summary>
        /// Block
        /// </summary>
        BlockObjectScript Block { get; set; }

        /// <summary>
        /// Health
        /// </summary>
        ushort Health { get; set; }

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

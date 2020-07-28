using Akasha.Data;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Block change interface
    /// </summary>
    public interface IBlockChange
    {
        /// <summary>
        /// Block
        /// </summary>
        BlockData Block { get; }

        /// <summary>
        /// Direction flags
        /// </summary>
        EDirectionFlags DirectionFlags { get; }
    }
}

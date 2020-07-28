using Akasha.Data;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Block change structure
    /// </summary>
    public readonly struct BlockChange : IBlockChange
    {
        /// <summary>
        /// Block
        /// </summary>
        public BlockData Block { get; }

        /// <summary>
        /// Direction flags
        /// </summary>
        public EDirectionFlags DirectionFlags { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="block">Block</param>
        /// <param name="directionFlags">Direction flags</param>
        public BlockChange(BlockData block, EDirectionFlags directionFlags)
        {
            Block = block;
            DirectionFlags = directionFlags;
        }
    }
}

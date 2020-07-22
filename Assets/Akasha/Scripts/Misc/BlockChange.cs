/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Block change structure
    /// </summary>
    public readonly struct BlockChange
    {
        /// <summary>
        /// Block
        /// </summary>
        public IBlockObject Block { get; }

        /// <summary>
        /// Direction flags
        /// </summary>
        public EDirectionFlags DirectionFlags { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="block">Block</param>
        /// <param name="directionFlags">Direction flags</param>
        public BlockChange(IBlockObject block, EDirectionFlags directionFlags)
        {
            Block = block;
            DirectionFlags = directionFlags;
        }
    }
}

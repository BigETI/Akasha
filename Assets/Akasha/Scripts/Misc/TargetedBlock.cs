using Akasha.Data;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Targeted block
    /// </summary>
    public readonly struct TargetedBlock : ITargetedBlock
    {
        /// <summary>
        /// Block
        /// </summary>
        public BlockData Block { get; }

        /// <summary>
        /// Block ID
        /// </summary>
        public BlockID ID { get; }

        /// <summary>
        /// Is a block
        /// </summary>
        public bool IsABlock => Block.IsABlock;

        /// <summary>
        /// Is nothing
        /// </summary>
        public bool IsNothing => Block.IsNothing;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="block">Block</param>
        /// <param name="id">ID</param>
        public TargetedBlock(BlockData block, BlockID id)
        {
            Block = block;
            ID = id;
        }
    }
}

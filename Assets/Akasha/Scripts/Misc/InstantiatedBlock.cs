using Akasha.Data;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Instantiated block structure
    /// </summary>
    public readonly struct InstantiatedBlock : IInstantiatedBlock
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
        /// Instance
        /// </summary>
        public GameObject Instance { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="block">Block</param>
        /// <param name="directionFlags">Direction flags</param>
        /// <param name="instance">Instance</param>
        public InstantiatedBlock(BlockData block, EDirectionFlags directionFlags, GameObject instance)
        {
            Block = block;
            DirectionFlags = directionFlags;
            Instance = instance;
        }
    }
}

using Akasha.Data;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Instantiated block interface
    /// </summary>
    public interface IInstantiatedBlock
    {
        /// <summary>
        /// Block
        /// </summary>
        BlockData Block { get; }

        /// <summary>
        /// Direction flags
        /// </summary>
        EDirectionFlags DirectionFlags { get; }

        /// <summary>
        /// Instance
        /// </summary>
        GameObject Instance { get; }
    }
}

using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Instantiated game object structure
    /// </summary>
    public readonly struct InstantiatedGameObject
    {
        /// <summary>
        /// Block type
        /// </summary>
        public IBlockObject Block { get; }

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
        public InstantiatedGameObject(IBlockObject block, EDirectionFlags directionFlags, GameObject instance)
        {
            Block = block;
            DirectionFlags = directionFlags;
            Instance = instance;
        }
    }
}

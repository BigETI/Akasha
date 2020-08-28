using System;
using UnityEngine;

/// <summary>
/// Akasha data namespace
/// </summary>
namespace Akasha.Data
{
    /// <summary>
    /// World meta data class
    /// </summary>
    [Serializable]
    public class WorldMetaData : IWorldMetaData
    {
        /// <summary>
        /// World name
        /// </summary>
        [SerializeField]
        private string worldName = string.Empty;

        /// <summary>
        /// World description
        /// </summary>
        [SerializeField]
        private string worldDescription = string.Empty;

        /// <summary>
        /// World seed
        /// </summary>
        [SerializeField]
        private int worldSeed;

        /// <summary>
        /// World name
        /// </summary>
        public string WorldName
        {
            get
            {
                if (worldName == null)
                {
                    worldName = string.Empty;
                }
                return worldName;
            }
            set => worldName = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// World description
        /// </summary>
        public string WorldDescription
        {
            get
            {
                if (worldDescription == null)
                {
                    worldDescription = string.Empty;
                }
                return worldDescription;
            }
            set => worldName = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// World seed
        /// </summary>
        public int WorldSeed
        {
            get => worldSeed;
            set => worldSeed = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="worldName">World name</param>
        /// <param name="worldDescription">World description</param>
        /// <param name="worldSeed">World seed</param>
        public WorldMetaData(string worldName, string worldDescription, int worldSeed)
        {
            this.worldName = worldName ?? throw new ArgumentNullException(nameof(worldName));
            this.worldDescription = worldDescription ?? throw new ArgumentNullException(nameof(worldDescription));
            this.worldSeed = worldSeed;
        }
    }
}

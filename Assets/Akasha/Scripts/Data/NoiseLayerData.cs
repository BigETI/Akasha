using Akasha.Objects;
using System;
using UnityEngine;

/// <summary>
/// Akasha data namespace
/// </summary>
namespace Akasha.Data
{
    /// <summary>
    /// Noise layer data class
    /// </summary>
    [Serializable]
    public class NoiseLayerData : INoiseLayerData
    {
        /// <summary>
        /// Filter
        /// </summary>
        [SerializeField]
        private ESetBlocksOperation filter = ESetBlocksOperation.OverrideAll;

        /// <summary>
        /// Block
        /// </summary>
        [SerializeField]
        private BlockObjectScript block;

        /// <summary>
        /// Noise generators
        /// </summary>
        [SerializeField]
        private NoiseGeneratorData[] noiseGenerators = Array.Empty<NoiseGeneratorData>();

        /// <summary>
        /// Filter
        /// </summary>
        public ESetBlocksOperation Filter
        {
            get => filter;
            set => filter = value;
        }

        /// <summary>
        /// Block
        /// </summary>
        public BlockObjectScript Block
        {
            get => block;
            set => block = value;
        }

        /// <summary>
        /// Noise generators
        /// </summary>
        public NoiseGeneratorData[] NoiseGenerators
        {
            get
            {
                if (noiseGenerators == null)
                {
                    noiseGenerators = Array.Empty<NoiseGeneratorData>();
                }
                return noiseGenerators;
            }
            set => noiseGenerators = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}

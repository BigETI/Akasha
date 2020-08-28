using Akasha.Data;
using Akasha.Objects;
using LibNoise;
using UnityEngine;

/// <summary>
/// AKasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Surface ´feature data interface
    /// </summary>
    public interface ISurfaceFeatureData
    {
        /// <summary>
        /// Noise seed
        /// </summary>
        int NoiseSeed { get; set; }

        /// <summary>
        /// Noise frequency
        /// </summary>
        double NoiseFrequency { get; set; }

        /// <summary>
        /// Noise lacunarity
        /// </summary>
        double NoiseLacunarity { get; set; }

        /// <summary>
        /// Noise persistence
        /// </summary>
        double NoisePersistence { get; set; }

        /// <summary>
        /// Noise octave count
        /// </summary>
        int NoiseOctaveCount { get; set; }

        /// <summary>
        /// Noise octave count
        /// </summary>
        QualityMode NoiseQuality { get; set; }

        /// <summary>
        /// Input noise offset
        /// </summary>
        Vector3 InputNoiseOffset { get; set; }

        /// <summary>
        /// Input noise scale
        /// </summary>
        Vector3 InputNoiseScale { get; set; }

        /// <summary>
        /// Output noise offset
        /// </summary>
        double OutputNoiseOffset { get; set; }

        /// <summary>
        /// Output noise scale
        /// </summary>
        double OutputNoiseScale { get; set; }

        /// <summary>
        /// Block
        /// </summary>
        BlockData Block { get; set; }

        /// <summary>
        /// Surface blocks
        /// </summary>
        BlockObjectScript[] SurfaceBlocks { get; set; }

        /// <summary>
        /// World seed
        /// </summary>
        int WorldSeed { get; }

        /// <summary>
        /// Is initialized
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="worldSeed">World seed</param>
        void Initialize(int worldSeed);

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="worldSeed">World seed</param>
        /// <param name="force">Force initialization</param>
        void Initialize(int worldSeed, bool force);

        /// <summary>
        /// Get weight
        /// </summary>
        /// <param name="blockID">BlockID</param>
        /// <returns>Weight</returns>
        double GetWeight(BlockID blockID);
    }
}

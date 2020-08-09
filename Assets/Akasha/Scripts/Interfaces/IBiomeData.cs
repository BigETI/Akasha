using Akasha.Data;
using LibNoise;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Biome data interface
    /// </summary>
    public interface IBiomeData
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
        Vector2 InputNoiseScale { get; set; }

        /// <summary>
        /// Output noise offset
        /// </summary>
        double OutputNoiseOffset { get; set; }

        /// <summary>
        /// Output noise scale
        /// </summary>
        double OutputNoiseScale { get; set; }

        /// <summary>
        /// Minimal temperature in Kelvin
        /// </summary>
        float MinimalTemperature { get; set; }

        /// <summary>
        /// Maximal temperature in Kelvin
        /// </summary>
        float MaximalTemperature { get; set; }

        /// <summary>
        /// Minimal pressure in atmosphere
        /// </summary>
        float MinimalPressure { get; set; }

        /// <summary>
        /// Maximal pressure in atmosphere
        /// </summary>
        float MaximalPressure { get; set; }

        /// <summary>
        /// Minimal wind speed in meters per second
        /// </summary>
        float MinimalWindSpeed { get; set; }

        /// <summary>
        /// Maximal wind speed in meters per second
        /// </summary>
        float MaximalWindSpeed { get; set; }

        /// <summary>
        /// Possible weathers
        /// </summary>
        WeatherData[] PossibleWeathers { get; set; }

        /// <summary>
        /// Noise layers
        /// </summary>
        NoiseLayerData[] NoiseLayers { get; set; }

        /// <summary>
        /// Is initialized
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// Initialize
        /// </summary>
        void Initialize();

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="force">Force initialization</param>
        void Initialize(bool force);

        /// <summary>
        /// Get biome weight
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <returns>Weight of biome</returns>
        double GetBiomeWeight(BlockID blockID);

        /// <summary>
        /// Get biome noise result
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <returns>Biome noise result</returns>
        double GetBiomeNoiseResult(BlockID blockID);

        /// <summary>
        /// Get generated block without surface feature
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <returns>Block</returns>
        BlockData GetGeneratedBlockWithoutSurfaceFeature(BlockID blockID);

        /// <summary>
        /// Get generated block without surface feature
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <param name="nouseOutputOffset">Noise output offset</param>
        /// <returns>Block</returns>
        BlockData GetGeneratedBlockWithoutSurfaceFeature(BlockID blockID, double noiseOutputOffset);

        /// <summary>
        /// Get generated block
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <returns>Generated block</returns>
        BlockData GetGeneratedBlock(BlockID blockID);

        /// <summary>
        /// Get generated block
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <param name="nouseOutputOffset">Noise output offset</param>
        /// <returns>Generated block</returns>
        BlockData GetGeneratedBlock(BlockID blockID, double noiseOutputOffset);
    }
}

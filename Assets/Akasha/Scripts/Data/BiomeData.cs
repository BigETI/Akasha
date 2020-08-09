using Akasha.Objects;
using LibNoise;
using LibNoise.Generator;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Akasha data namespace
/// </summary>
namespace Akasha.Data
{
    /// <summary>
    /// Biome data class
    /// </summary>
    [Serializable]
    public class BiomeData : IBiomeData
    {
        /// <summary>
        /// Noise generator operators
        /// </summary>
        private static readonly NoiseGeneratorOperatorDelegate[] noiseGeneratorOperators = new NoiseGeneratorOperatorDelegate[]
        {
            (left, right) => left + right,
            (left, right) => left - right,
            (left, right) => left * right,
            (left, right) => left / right
        };

        /// <summary>
        /// Noise seed
        /// </summary>
        [SerializeField]
        private int noiseSeed;

        /// <summary>
        /// Noise frequency
        /// </summary>
        [SerializeField]
        private double noiseFrequency = 1.0;

        /// <summary>
        /// Noise lacunarity
        /// </summary>
        [SerializeField]
        private double noiseLacunarity = 2.0;

        /// <summary>
        /// Noise persistence
        /// </summary>
        [SerializeField]
        private double noisePersistence = 0.5;

        /// <summary>
        /// Noise octave count
        /// </summary>
        [SerializeField]
        private int noiseOctaveCount = 6;

        /// <summary>
        /// Noise quality
        /// </summary>
        [SerializeField]
        private QualityMode noiseQuality = QualityMode.Medium;

        /// <summary>
        /// Input noise offset
        /// </summary>
        [SerializeField]
        private Vector3 inputNoiseOffset = Vector3.zero;

        /// <summary>
        /// Input noise scale
        /// </summary>
        [SerializeField]
        private Vector2 inputNoiseScale = Vector2.one;

        /// <summary>
        /// Output noise offset
        /// </summary>
        [SerializeField]
        private double outputNoiseOffset = 0.0;

        /// <summary>
        /// Output noise scale
        /// </summary>
        [SerializeField]
        private double outputNoiseScale = 1.0;

        /// <summary>
        /// Minimal temperature in Kelvin
        /// </summary>
        [SerializeField]
        [Range(0.0f, 10000.0f)]
        private float minimalTemperature = 278.15f;

        /// <summary>
        /// Maximal temperature in Kelvin
        /// </summary>
        [SerializeField]
        [Range(0.0f, 10000.0f)]
        private float maximalTemperature = 288.15f;

        /// <summary>
        /// Minimal pressure in atmosphere
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float minimalPressure = 0.9f;

        /// <summary>
        /// Maximal pressure in atmosphere
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float maximalPressure = 1.1f;

        /// <summary>
        /// Minimal wind speed in meters per second
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float minimalWindSpeed;

        /// <summary>
        /// Maximal wind speed in meters per second
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float maximalWindSpeed = 5.0f;

        /// <summary>
        /// Possible weathers
        /// </summary>
        [SerializeField]
        private WeatherData[] possibleWeathers = new WeatherData[]
        {
            new WeatherData()
        };

        /// <summary>
        /// Noise layers
        /// </summary>
        [SerializeField]
        private NoiseLayerData[] noiseLayers = Array.Empty<NoiseLayerData>();

        /// <summary>
        /// Surface features
        /// </summary>
        [SerializeField]
        private SurfaceFeatureData[] surfaceFeatures = Array.Empty<SurfaceFeatureData>();

        /// <summary>
        /// Perlin noise
        /// </summary>
        private Perlin perlinNoise;

        /// <summary>
        /// Computed noise layer modules
        /// </summary>
        private List<(INoiseLayerData, List<(INoiseGeneratorData, ModuleBase)>)> computedNoiseLayers;

        /// <summary>
        /// Noise seed
        /// </summary>
        public int NoiseSeed
        {
            get => noiseSeed;
            set
            {
                if (noiseSeed != value)
                {
                    noiseSeed = value;
                    Initialize(true);
                }
            }
        }

        /// <summary>
        /// Noise frequency
        /// </summary>
        public double NoiseFrequency
        {
            get => noiseFrequency;
            set
            {
                if (noiseFrequency != value)
                {
                    noiseFrequency = value;
                    Initialize(true);
                }
            }
        }

        /// <summary>
        /// Noise lacunarity
        /// </summary>
        public double NoiseLacunarity
        {
            get => noiseLacunarity;
            set
            {
                if (noiseLacunarity != value)
                {
                    noiseLacunarity = value;
                    Initialize(true);
                }
            }
        }

        /// <summary>
        /// Noise persistence
        /// </summary>
        public double NoisePersistence
        {
            get => noisePersistence;
            set
            {
                if (noisePersistence != value)
                {
                    noisePersistence = value;
                    Initialize(true);
                }
            }
        }

        /// <summary>
        /// Noise octave count
        /// </summary>
        public int NoiseOctaveCount
        {
            get => noiseOctaveCount;
            set
            {
                if (noiseOctaveCount != value)
                {
                    noiseOctaveCount = value;
                    Initialize(true);
                }
            }
        }

        /// <summary>
        /// Noise octave count
        /// </summary>
        public QualityMode NoiseQuality
        {
            get => noiseQuality;
            set
            {
                if (noiseQuality != value)
                {
                    noiseQuality = value;
                    Initialize(true);
                }
            }
        }

        /// <summary>
        /// Input noise offset
        /// </summary>
        public Vector3 InputNoiseOffset
        {
            get => inputNoiseOffset;
            set => inputNoiseOffset = value;
        }

        /// <summary>
        /// Input noise scale
        /// </summary>
        public Vector2 InputNoiseScale
        {
            get => inputNoiseScale;
            set => inputNoiseScale = value;
        }

        /// <summary>
        /// Output noise offset
        /// </summary>
        public double OutputNoiseOffset
        {
            get => outputNoiseOffset;
            set => outputNoiseOffset = value;
        }

        /// <summary>
        /// Output noise scale
        /// </summary>
        public double OutputNoiseScale
        {
            get => outputNoiseScale;
            set => outputNoiseScale = value;
        }

        /// <summary>
        /// Minimal temperature in Kelvin
        /// </summary>
        public float MinimalTemperature
        {
            get => Mathf.Clamp(minimalTemperature, 0.0f, maximalTemperature);
            set => minimalTemperature = Mathf.Clamp(value, 0.0f, maximalTemperature);
        }

        /// <summary>
        /// Maximal temperature in Kelvin
        /// </summary>
        public float MaximalTemperature
        {
            get => Mathf.Max(MinimalTemperature, maximalTemperature);
            set => maximalTemperature = Mathf.Max(MinimalTemperature, value);
        }

        /// <summary>
        /// Minimal pressure in atmosphere
        /// </summary>
        public float MinimalPressure
        {
            get => Mathf.Clamp(minimalPressure, 0.0f, maximalPressure);
            set => minimalPressure = Mathf.Clamp(value, 0.0f, maximalPressure);
        }

        /// <summary>
        /// Maximal pressure in atmosphere
        /// </summary>
        public float MaximalPressure
        {
            get => Mathf.Max(MinimalPressure, maximalPressure);
            set => maximalPressure = Mathf.Max(MinimalPressure, value);
        }

        /// <summary>
        /// Minimal wind speed in meters per second
        /// </summary>
        public float MinimalWindSpeed
        {
            get => Mathf.Clamp(minimalWindSpeed, 0.0f, maximalWindSpeed);
            set => minimalWindSpeed = Mathf.Clamp(value, 0.0f, maximalWindSpeed);
        }

        /// <summary>
        /// Maximal wind speed in meters per second
        /// </summary>
        public float MaximalWindSpeed
        {
            get => Mathf.Max(MinimalWindSpeed, maximalWindSpeed);
            set => maximalWindSpeed = Mathf.Max(MinimalWindSpeed, value);
        }

        /// <summary>
        /// Possible weathers
        /// </summary>
        public WeatherData[] PossibleWeathers
        {
            get
            {
                if (possibleWeathers == null)
                {
                    possibleWeathers = Array.Empty<WeatherData>();
                }
                return possibleWeathers;
            }
            set => possibleWeathers = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Noise layers
        /// </summary>
        public NoiseLayerData[] NoiseLayers
        {
            get
            {
                if (noiseLayers == null)
                {
                    noiseLayers = Array.Empty<NoiseLayerData>();
                }
                return noiseLayers;
            }
            set
            {
                noiseLayers = value ?? throw new ArgumentNullException(nameof(value));
                Initialize(true);
            }
        }

        /// <summary>
        /// Surface features
        /// </summary>
        private SurfaceFeatureData[] SurfaceFeatures
        {
            get
            {
                if (surfaceFeatures == null)
                {
                    surfaceFeatures = Array.Empty<SurfaceFeatureData>();
                }
                return surfaceFeatures;
            }
            set => surfaceFeatures = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Is initialized
        /// </summary>
        public bool IsInitialized => ((perlinNoise != null) && (computedNoiseLayers != null));

        /// <summary>
        /// Initialize
        /// </summary>
        public void Initialize() => Initialize(false);

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="force">Force initialization</param>
        public void Initialize(bool force)
        {
            bool init = false;
            if (computedNoiseLayers == null)
            {
                computedNoiseLayers = new List<(INoiseLayerData, List<(INoiseGeneratorData, ModuleBase)>)>();
                init = true;
            }
            if (perlinNoise == null)
            {
                init = true;
            }
            if (force || init)
            {
                perlinNoise = new Perlin(noiseFrequency, noiseLacunarity, noisePersistence, noiseOctaveCount, noiseSeed, noiseQuality);
                computedNoiseLayers.Clear();
                foreach (INoiseLayerData noise_layer in NoiseLayers)
                {
                    if (noise_layer != null)
                    {
                        List<(INoiseGeneratorData, ModuleBase)> noise_generator_modules = new List<(INoiseGeneratorData, ModuleBase)>();
                        foreach (INoiseGeneratorData noise_generator in noise_layer.NoiseGenerators)
                        {
                            noise_generator_modules.Add((noise_generator, noise_generator.NewNoiseModule));
                        }
                        computedNoiseLayers.Add((noise_layer, noise_generator_modules));
                    }
                }
                foreach (ISurfaceFeatureData surface_feature in SurfaceFeatures)
                {
                    surface_feature?.Initialize();
                }
            }
        }

        /// <summary>
        /// Get biome weight
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <returns>Weight of biome</returns>
        public double GetBiomeWeight(BlockID blockID) => ((perlinNoise == null) ? 0.0 : ((perlinNoise.GetValue((blockID.X * inputNoiseScale.x) + inputNoiseOffset.x, inputNoiseOffset.y, (blockID.Z * inputNoiseScale.y) + inputNoiseOffset.z) * outputNoiseScale) + outputNoiseOffset));

        /// <summary>
        /// Get noise layer result
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <param name="computedNoiseLayer">Computed noise layer</param>
        /// <returns>Noise layer result</returns>
        public double GetNoiseLayerResult(BlockID blockID, (INoiseLayerData, List<(INoiseGeneratorData, ModuleBase)>) computedNoiseLayer)
        {
            double ret = 0.0;
            foreach ((INoiseGeneratorData, ModuleBase) noise_generator_module in computedNoiseLayer.Item2)
            {
                ret = noiseGeneratorOperators[(int)(noise_generator_module.Item1.GeneratorOperator)]
                (
                    ret,
                    (noise_generator_module.Item2.GetValue
                    (
                        ((((noise_generator_module.Item1.AxisFlags & ENoiseGeneratorAxisFlags.X) == ENoiseGeneratorAxisFlags.X) ? blockID.X : 0L) * noise_generator_module.Item1.InputScale.x) + noise_generator_module.Item1.InputOffset.x,
                        ((((noise_generator_module.Item1.AxisFlags & ENoiseGeneratorAxisFlags.Y) == ENoiseGeneratorAxisFlags.Y) ? blockID.Y : 0L) * noise_generator_module.Item1.InputScale.y) + noise_generator_module.Item1.InputOffset.y,
                        ((((noise_generator_module.Item1.AxisFlags & ENoiseGeneratorAxisFlags.Z) == ENoiseGeneratorAxisFlags.Z) ? blockID.Z : 0L) * noise_generator_module.Item1.InputScale.z) + noise_generator_module.Item1.InputOffset.z
                    ) * noise_generator_module.Item1.OutputScale) + (((noise_generator_module.Item1.AxisFlags & ENoiseGeneratorAxisFlags.X) == ENoiseGeneratorAxisFlags.X) ? 0L : blockID.X) + (((noise_generator_module.Item1.AxisFlags & ENoiseGeneratorAxisFlags.Y) == ENoiseGeneratorAxisFlags.Y) ? 0L : blockID.Y) + (((noise_generator_module.Item1.AxisFlags & ENoiseGeneratorAxisFlags.Z) == ENoiseGeneratorAxisFlags.Z) ? 0L : blockID.Z) + noise_generator_module.Item1.OutputOffset
                );
            }
            return ret;
        }

        /// <summary>
        /// Get biome noise result
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <returns>Biome noise result</returns>
        public double GetBiomeNoiseResult(BlockID blockID)
        {
            double ret = 0.0;
            if (computedNoiseLayers != null)
            {
                foreach ((INoiseLayerData, List<(INoiseGeneratorData, ModuleBase)>) computed_noise_layer in computedNoiseLayers)
                {
                    ret = computed_noise_layer.Item1.Block ? Math.Max(GetNoiseLayerResult(blockID, computed_noise_layer), ret) : ret;
                }
            }
            return ret;
        }

        /// <summary>
        /// Get generated block without surface feature
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <returns>Block</returns>
        public BlockData GetGeneratedBlockWithoutSurfaceFeature(BlockID blockID) => GetGeneratedBlockWithoutSurfaceFeature(blockID, 0.0);

        /// <summary>
        /// Get generated block without surface feature
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <param name="nouseOutputOffset">Noise output offset</param>
        /// <returns>Block</returns>
        public BlockData GetGeneratedBlockWithoutSurfaceFeature(BlockID blockID, double noiseOutputOffset)
        {
            BlockData ret = default;
            if (computedNoiseLayers != null)
            {
                foreach ((INoiseLayerData, List<(INoiseGeneratorData, ModuleBase)>) computed_noise_layer in computedNoiseLayers)
                {
                    if (((computed_noise_layer.Item1.Filter == ESetBlocksOperation.FillEmpty) && ret.IsABlock) || ((computed_noise_layer.Item1.Filter == ESetBlocksOperation.ReplaceFull) && ret.IsNothing))
                    {
                        break;
                    }
                    if ((GetNoiseLayerResult(blockID, computed_noise_layer) + noiseOutputOffset) >= 0.0)
                    {
                        ret = (computed_noise_layer.Item1.Block ? new BlockData(computed_noise_layer.Item1.Block, computed_noise_layer.Item1.Block.InitialHealth) : default);
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Get generated block
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <returns>Generated block</returns>
        public BlockData GetGeneratedBlock(BlockID blockID) => GetGeneratedBlock(blockID, 0.0);

        /// <summary>
        /// Get generated block
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <param name="nouseOutputOffset">Noise output offset</param>
        /// <returns>Generated block</returns>
        public BlockData GetGeneratedBlock(BlockID blockID, double noiseOutputOffset)
        {
            BlockData ret = GetGeneratedBlockWithoutSurfaceFeature(blockID, noiseOutputOffset);
            if ((surfaceFeatures != null) && (perlinNoise != null) && ret.IsNothing)
            {
                BlockData surface_block = GetGeneratedBlockWithoutSurfaceFeature(blockID + BlockID.Down, noiseOutputOffset);
                if (surface_block.IsABlock)
                {
                    BlockData selected_block = ret;
                    double selected_block_weight = 0.0;
                    foreach (ISurfaceFeatureData surface_feature in surfaceFeatures)
                    {
                        if (surface_feature != null)
                        {
                            double weight = surface_feature.GetWeight(blockID);
                            if (selected_block_weight < weight)
                            {
                                foreach (BlockObjectScript surface_feature_surface_block in surface_feature.SurfaceBlocks)
                                {
                                    if (surface_block.Block == surface_feature_surface_block)
                                    {
                                        selected_block = surface_feature.Block;
                                        selected_block_weight = weight;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    ret = selected_block;
                }
            }
            return ret;
        }
    }
}

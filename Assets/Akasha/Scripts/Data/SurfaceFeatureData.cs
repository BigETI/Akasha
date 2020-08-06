using Akasha.Objects;
using LibNoise;
using LibNoise.Generator;
using System;
using UnityEngine;
using UnityExtensions;

/// <summary>
/// Akasha data namespace
/// </summary>
namespace Akasha.Data
{
    /// <summary>
    /// Surface feature data structure
    /// </summary>
    [Serializable]
    public class SurfaceFeatureData : ISurfaceFeatureData
    {
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
        private Vector3 inputNoiseScale = Vector3.one;

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
        /// Block
        /// </summary>
        [SerializeField]
        private BlockData block;

        /// <summary>
        /// Surface blocks
        /// </summary>
        [SerializeField]
        private BlockObjectScript[] surfaceBlocks;

        /// <summary>
        /// Perlin noise
        /// </summary>
        private Perlin perlinNoise;

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
        public Vector3 InputNoiseScale
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
        /// Block
        /// </summary>
        public BlockData Block
        {
            get => block;
            set => block = value;
        }

        /// <summary>
        /// Surface blocks
        /// </summary>
        public BlockObjectScript[] SurfaceBlocks
        {
            get
            {
                if (surfaceBlocks == null)
                {
                    surfaceBlocks = Array.Empty<BlockObjectScript>();
                }
                return surfaceBlocks;
            }
            set => surfaceBlocks = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Is initialized
        /// </summary>
        public bool IsInitialized => (perlinNoise != null);

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
            if (force || (perlinNoise == null))
            {
                perlinNoise = new Perlin(noiseFrequency, noiseLacunarity, noisePersistence, noiseOctaveCount, noiseSeed, noiseQuality);
            }
        }

        /// <summary>
        /// Get weight
        /// </summary>
        /// <param name="blockID">BlockID</param>
        /// <returns>Weight</returns>
        public double GetWeight(BlockID blockID) => ((perlinNoise == null) ? 0.0 : ((perlinNoise.GetValue((blockID.X * inputNoiseScale.x) + inputNoiseOffset.x, (blockID.Y * inputNoiseScale.y) + inputNoiseOffset.y, (blockID.Z * inputNoiseScale.z) + inputNoiseOffset.z) * outputNoiseScale) + outputNoiseOffset));
    }
}

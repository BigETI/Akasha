using LibNoise;
using LibNoise.Generator;
using System;
using UnityEngine;

/// <summary>
/// Akasha data namespace
/// </summary>
namespace Akasha.Data
{
    /// <summary>
    /// Noise generator data
    /// </summary>
    [Serializable]
    public class NoiseGeneratorData : INoiseGeneratorData
    {
        /// <summary>
        /// Operator
        /// </summary>
        [SerializeField]
        private ENoiseGeneratorOperator generatorOperator = ENoiseGeneratorOperator.Add;

        /// <summary>
        /// Noise layer algorithm
        /// </summary>
        [SerializeField]
        private ENoiseGeneratorAlgorithm noiseLayerAlgorithm = ENoiseGeneratorAlgorithm.Perlin;

        /// <summary>
        /// Axis flags
        /// </summary>
        [SerializeField]
        private ENoiseGeneratorAxisFlags axisFlags = ENoiseGeneratorAxisFlags.XZ;

        /// <summary>
        /// Seed
        /// </summary>
        [SerializeField]
        private int seed;

        /// <summary>
        /// Frequency
        /// </summary>
        [SerializeField]
        private double frequency = 1.0;

        /// <summary>
        /// Lacunarity
        /// </summary>
        [SerializeField]
        private double lacunarity = 2.0;

        /// <summary>
        /// Persistence
        /// </summary>
        [SerializeField]
        private double persistence = 0.5;

        /// <summary>
        /// Displacement
        /// </summary>
        [SerializeField]
        private double displacement = 1.0;

        /// <summary>
        /// Octave count
        /// </summary>
        [SerializeField]
        private int octaveCount = 6;

        /// <summary>
        /// Distance
        /// </summary>
        [SerializeField]
        private bool distance = default;

        /// <summary>
        /// Quality
        /// </summary>
        [SerializeField]
        private QualityMode quality = QualityMode.Medium;

        /// <summary>
        /// Input offset
        /// </summary>
        [SerializeField]
        private Vector3 inputOffset = Vector3.zero;

        /// <summary>
        /// Input scale
        /// </summary>
        [SerializeField]
        private Vector3 inputScale = Vector3.one;

        /// <summary>
        /// Output offset
        /// </summary>
        [SerializeField]
        private float outputOffset = default;

        /// <summary>
        /// Output scale
        /// </summary>
        [SerializeField]
        private float outputScale = 1.0f;

        /// <summary>
        /// Generator operator
        /// </summary>
        public ENoiseGeneratorOperator GeneratorOperator
        {
            get => generatorOperator;
            set => generatorOperator = value;
        }

        /// <summary>
        /// Noise layer algorithm
        /// </summary>
        public ENoiseGeneratorAlgorithm NoiseLayerAlgorithm
        {
            get => noiseLayerAlgorithm;
            set => noiseLayerAlgorithm = value;
        }

        /// <summary>
        /// Axis flags
        /// </summary>
        public ENoiseGeneratorAxisFlags AxisFlags
        {
            get => axisFlags;
            set => axisFlags = value;
        }

        /// <summary>
        /// Seed
        /// </summary>
        public int Seed
        {
            get => seed;
            set => seed = value;
        }

        /// <summary>
        /// Frequency
        /// </summary>
        public double Frequency
        {
            get => frequency;
            set => frequency = value;
        }

        /// <summary>
        /// Lacunarity
        /// </summary>
        public double Lacunarity
        {
            get => lacunarity;
            set => lacunarity = value;
        }

        /// <summary>
        /// Persistence
        /// </summary>
        public double Persistence
        {
            get => persistence;
            set => persistence = value;
        }

        /// <summary>
        /// Displacement
        /// </summary>
        public double Displacement
        {
            get => displacement;
            set => displacement = value;
        }

        /// <summary>
        /// Octave count
        /// </summary>
        public int OctaveCount
        {
            get => octaveCount;
            set => octaveCount = value;
        }

        /// <summary>
        /// Distance
        /// </summary>
        public bool Distance
        {
            get => distance;
            set => distance = value;
        }

        /// <summary>
        /// Quality
        /// </summary>
        public QualityMode Quality
        {
            get => quality;
            set => quality = value;
        }

        /// <summary>
        /// Input offset
        /// </summary>
        public Vector3 InputOffset
        {
            get => inputOffset;
            set => inputOffset = value;
        }

        /// <summary>
        /// Input scale
        /// </summary>
        public Vector3 InputScale
        {
            get => inputScale;
            set => inputScale = value;
        }

        /// <summary>
        /// Output offset
        /// </summary>
        public float OutputOffset
        {
            get => outputOffset;
            set => outputOffset = value;
        }

        /// <summary>
        /// Output scale
        /// </summary>
        public float OutputScale
        {
            get => outputScale;
            set => outputScale = value;
        }

        /// <summary>
        /// New noise module
        /// </summary>
        /// <param name="worldSeed">World seed</param>
        /// <returns>Noise module if successful, otherwise "null"</returns>
        public ModuleBase CreateNewNoiseModule(int worldSeed)
        {
            ModuleBase ret = null;
            switch (noiseLayerAlgorithm)
            {
                case ENoiseGeneratorAlgorithm.Billow:
                    ret = new Billow(frequency, lacunarity, persistence, octaveCount, seed + worldSeed, quality);
                    break;
                case ENoiseGeneratorAlgorithm.Checker:
                    ret = new Checker();
                    break;
                case ENoiseGeneratorAlgorithm.Constant:
                    ret = new Const(0.0f);
                    break;
                case ENoiseGeneratorAlgorithm.Cylinders:
                    ret = new Cylinders(frequency);
                    break;
                case ENoiseGeneratorAlgorithm.Perlin:
                    ret = new Perlin(frequency, lacunarity, persistence, octaveCount, seed + worldSeed, quality);
                    break;
                case ENoiseGeneratorAlgorithm.RidgedMultiFractal:
                    ret = new RidgedMultifractal(frequency, lacunarity, octaveCount, seed + worldSeed, quality);
                    break;
                case ENoiseGeneratorAlgorithm.Spheres:
                    ret = new Spheres(frequency);
                    break;
                case ENoiseGeneratorAlgorithm.Voronoi:
                    ret = new Voronoi(frequency, displacement, seed + worldSeed, distance);
                    break;
            }
            return ret;
        }

        ///// <summary>
        ///// Default constructor
        ///// </summary>
        //public NoiseGeneratorData()
        //{
        //    // ...
        //}

        ///// <summary>
        ///// Constructor
        ///// </summary>
        ///// <param name="generatorOperator">Generator operator</param>
        ///// <param name="noiseLayerAlgorithm">Noise layer algorithm</param>
        ///// <param name="axisFlags">Axis flags</param>
        ///// <param name="seed">Seed</param>
        ///// <param name="frequency">Frequency</param>
        ///// <param name="lacunarity">Lacunarity</param>
        ///// <param name="octaveCount">Octave count</param>
        ///// <param name="persistence">Persistence</param>
        ///// <param name="displacement">Displacement</param>
        ///// <param name="distance">Distance</param>
        ///// <param name="quality">Quality</param>
        ///// <param name="inputOffset">Input offset</param>
        ///// <param name="inputScale">Input scale</param>
        ///// <param name="outputOffset">Output offset</param>
        ///// <param name="outputScale">Output scale</param>
        //public NoiseGeneratorData(ENoiseGeneratorOperator generatorOperator, ENoiseGeneratorAlgorithm noiseLayerAlgorithm, ENoiseGeneratorAxisFlags axisFlags, int seed, double frequency, double lacunarity, double persistence, double displacement, bool distance, int octaveCount, QualityMode quality, Vector3 inputOffset, Vector3 inputScale, float outputOffset, float outputScale)
        //{
        //    this.generatorOperator = generatorOperator;
        //    this.noiseLayerAlgorithm = noiseLayerAlgorithm;
        //    this.axisFlags = axisFlags;
        //    this.seed = seed;
        //    this.frequency = frequency;
        //    this.lacunarity = lacunarity;
        //    this.persistence = persistence;
        //    this.displacement = displacement;
        //    this.distance = distance;
        //    this.octaveCount = octaveCount;
        //    this.quality = quality;
        //    this.inputOffset = inputOffset;
        //    this.inputScale = inputScale;
        //    this.outputOffset = outputOffset;
        //    this.outputScale = outputScale;
        //}
    }
}

using LibNoise;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Noise layer data interface
    /// </summary>
    public interface INoiseGeneratorData
    {
        /// <summary>
        /// Generator operator
        /// </summary>
        ENoiseGeneratorOperator GeneratorOperator { get; set; }

        /// <summary>
        /// Noise layer algorithm
        /// </summary>
        ENoiseGeneratorAlgorithm NoiseLayerAlgorithm { get; set; }

        /// <summary>
        /// Axis flags
        /// </summary>
        ENoiseGeneratorAxisFlags AxisFlags { get; set; }

        /// <summary>
        /// Seed
        /// </summary>
        int Seed { get; set; }

        /// <summary>
        /// Frequency
        /// </summary>
        double Frequency { get; set; }

        /// <summary>
        /// Lacunarity
        /// </summary>
        double Lacunarity { get; set; }

        /// <summary>
        /// Persistence
        /// </summary>
        double Persistence { get; set; }

        /// <summary>
        /// Displacement
        /// </summary>
        double Displacement { get; set; }

        /// <summary>
        /// Octave count
        /// </summary>
        int OctaveCount { get; set; }

        /// <summary>
        /// Distance
        /// </summary>
        bool Distance { get; set; }

        /// <summary>
        /// Quality
        /// </summary>
        QualityMode Quality { get; set; }

        /// <summary>
        /// Input offset
        /// </summary>
        Vector3 InputOffset { get; set; }

        /// <summary>
        /// Input scale
        /// </summary>
        Vector3 InputScale { get; set; }

        /// <summary>
        /// Output offset
        /// </summary>
        float OutputOffset { get; set; }

        /// <summary>
        /// Output scale
        /// </summary>
        float OutputScale { get; set; }

        /// <summary>
        /// New noise module
        /// </summary>
        ModuleBase NewNoiseModule { get; }
    }
}

using Akasha.Data;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Noise layer data interface
    /// </summary>
    public interface INoiseLayerData
    {
        /// <summary>
        /// Filter
        /// </summary>
        ENoiseLayerFilter Filter { get; set; }

        /// <summary>
        /// Block
        /// </summary>
        BlockObjectScript Block { get; set; }

        /// <summary>
        /// Noise generators
        /// </summary>
        NoiseGeneratorData[] NoiseGenerators { get; set; }
    }
}

using Akasha.Data;
using Akasha.Objects;

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
        ESetBlocksOperation Filter { get; set; }

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

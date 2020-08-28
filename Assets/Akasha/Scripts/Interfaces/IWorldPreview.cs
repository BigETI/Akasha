using Akasha.Data;

/// <summary>
/// AKasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// World preview interface
    /// </summary>
    public interface IWorldPreview : IValidable
    {
        /// <summary>
        /// World preview image data
        /// </summary>
        byte[] PreviewImageData { get; }

        /// <summary>
        /// World meta data
        /// </summary>
        WorldMetaData MetaData { get; }
    }
}

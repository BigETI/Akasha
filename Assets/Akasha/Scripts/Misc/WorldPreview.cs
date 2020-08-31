using Akasha.Data;
using System;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// World preview structure
    /// </summary>
    public readonly struct WorldPreview : IWorldPreview
    {
        /// <summary>
        /// World preview image data
        /// </summary>
        public byte[] PreviewImageData { get; }

        /// <summary>
        /// World meta data
        /// </summary>
        public WorldMetaData MetaData { get; }

        /// <summary>
        /// Is valid
        /// </summary>
        public bool IsValid => (MetaData != null);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="previewImageData">Preview image data</param>
        /// <param name="metaData">Meta data</param>
        public WorldPreview(byte[] previewImageData, WorldMetaData metaData)
        {
            PreviewImageData = previewImageData;
            MetaData = metaData ?? throw new ArgumentNullException(nameof(metaData));
        }
    }
}

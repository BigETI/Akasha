using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Worlds panel UI controller interface
    /// </summary>
    public interface IWorldsPanelUIController : IBehaviour
    {
        /// <summary>
        /// World panel asset
        /// </summary>
        GameObject WorldPanelAsset { get; set; }

        /// <summary>
        /// Rectangle transform
        /// </summary>
        RectTransform RectangleTransform { get; }

        /// <summary>
        /// Vertical layout group
        /// </summary>
        VerticalLayoutGroup VerticalLayoutGroup { get; }

        /// <summary>
        /// Update visuals
        /// </summary>
        void UpdateVisuals();
    }
}

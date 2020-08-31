using System;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// World panel UI controller interface
    /// </summary>
    public interface IWorldPanelUIController
    {
        /// <summary>
        /// Preview image
        /// </summary>
        Image PreviewImage { get; set; }

        /// <summary>
        /// World name text
        /// </summary>
        TextMeshProUGUI WorldNameText { get; set; }

        /// <summary>
        /// World description text
        /// </summary>
        TextMeshProUGUI WorldDescriptionText { get; set; }

        /// <summary>
        /// World GUID
        /// </summary>
        Guid WorldGUID { get; }

        /// <summary>
        /// Set values
        /// </summary>
        /// <param name="worldGUID">World GUID</param>
        /// <param name="parent">Parent</param>
        void SetValues(Guid worldGUID, IWorldsPanelUIController parent);

        /// <summary>
        /// Load world
        /// </summary>
        void LoadWorld();

        /// <summary>
        /// Delete world
        /// </summary>
        void DeleteWorld();
    }
}

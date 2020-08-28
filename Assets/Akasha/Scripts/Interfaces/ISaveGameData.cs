using System;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Save game data
    /// </summary>
    public interface ISaveGameData
    {
        /// <summary>
        /// Invert X axis
        /// </summary>
        bool InvertXAxis { get; set; }

        /// <summary>
        /// Invert Y axis
        /// </summary>
        bool InvertYAxis { get; set; }

        /// <summary>
        /// Enable view bobbing
        /// </summary>
        bool EnableViewBobbing { get; set; }

        /// <summary>
        /// Sensitivity
        /// </summary>
        Vector2 Sensitivity { get; set; }

        /// <summary>
        /// Player character GUID
        /// </summary>
        Guid PlayerCharacterGUID { get; set; }
    }
}

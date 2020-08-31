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
        /// Master volume
        /// </summary>
        float MasterVolume { get; set; }

        /// <summary>
        /// Music volume
        /// </summary>
        float MusicVolume { get; set; }

        /// <summary>
        /// Sound effects volume
        /// </summary>
        float SoundEffectsVolume { get; set; }

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

        /// <summary>
        /// Are cheats enabled
        /// </summary>
        bool AreCheatsEnabled { get; set; }
    }
}

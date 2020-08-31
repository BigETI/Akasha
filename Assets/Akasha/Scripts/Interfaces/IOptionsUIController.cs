using UnityEngine.UI;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Options UI controller interface
    /// </summary>
    public interface IOptionsUIController : IBehaviour
    {
        /// <summary>
        /// Master volume slider
        /// </summary>
        Slider MasterVolumeSlider { get; set; }

        /// <summary>
        /// Music volume slider
        /// </summary>
        Slider MusicVolumeSlider { get; set; }

        /// <summary>
        /// Sound effects volume slider
        /// </summary>
        Slider SoundEffectsVolumeSlider { get; set; }

        /// <summary>
        /// Enable cheats toggle
        /// </summary>
        Toggle EnableCheatsToggle { get; set; }
    }
}

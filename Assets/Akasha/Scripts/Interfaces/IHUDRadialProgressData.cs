using Akasha.Controllers;

/// <summary>
/// AKasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// HUD radial progress data interface
    /// </summary>
    public interface IHUDRadialProgressData
    {
        /// <summary>
        /// Radial progress
        /// </summary>
        RadialProgressUIControllerScript RadialProgress { get; set; }

        /// <summary>
        /// Blink threshold
        /// </summary>
        float BlinkThreshold { get; set; }

        /// <summary>
        /// Animate progress
        /// </summary>
        bool AnimateProgress { get; set; }

        /// <summary>
        /// Progress speed
        /// </summary>
        float ProgressSpeed { get; set; }

        /// <summary>
        /// Reset
        /// </summary>
        void Reset();
    }
}

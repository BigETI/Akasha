using Akasha.Controllers;
using Akasha.Data;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// HUD UI controller interface
    /// </summary>
    public interface IHUDUIController : IBehaviour
    {
        /// <summary>
        /// Blink time
        /// </summary>
        float BlinkTime { get; set; }

        /// <summary>
        /// Block health HUD radial progress
        /// </summary>
        HUDRadialProgressData BlockHealthHUDRadialProgress { get; set; }

        /// <summary>
        /// Hit HUD radial progress
        /// </summary>
        HUDRadialProgressData HitHUDRadialProgress { get; set; }

        /// <summary>
        /// Stamina HUD radial progress
        /// </summary>
        HUDRadialProgressData StaminaHUDRadialProgress { get; set; }

        /// <summary>
        /// Fullness HUD radial progress
        /// </summary>
        HUDRadialProgressData FullnessHUDRadialProgress { get; set; }

        /// <summary>
        /// Health HUD radial progress
        /// </summary>
        HUDRadialProgressData HealthHUDRadialProgress { get; set; }

        /// <summary>
        /// Armor HUD radial progress
        /// </summary>
        HUDRadialProgressData ArmorHUDRadialProgress { get; set; }

        /// <summary>
        /// Elapsed blink time
        /// </summary>
        float ElapsedBlinkTime { get; }

        /// <summary>
        /// Blink state
        /// </summary>
        bool BlinkState { get; }

        /// <summary>
        /// Player character controller
        /// </summary>
        CharacterControllerScript PlayerCharacterController { get; }
    }
}

using TMPro;
using UnityEngine.UI;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// HUD panel UI controller interface
    /// </summary>
    public interface IHUDPanelUIController : IBehaviour
    {
        /// <summary>
        /// Health image
        /// </summary>
        Image HealthImage { get; set; }

        /// <summary>
        /// Weapon name text
        /// </summary>
        TextMeshProUGUI WeaponNameText { get; set; }

        /// <summary>
        /// Weapon ammo text
        /// </summary>
        TextMeshProUGUI WeaponAmmoText { get; set; }

        /// <summary>
        /// Weapon image
        /// </summary>
        Image WeaponImage { get; set; }
    }
}

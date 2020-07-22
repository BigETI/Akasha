using Akasha.Objects;

/// <summary>
/// Akasha
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Weapon pickup contorller interface
    /// </summary>
    public interface IWeaponPickupController : IBehaviour
    {
        /// <summary>
        /// Weapon
        /// </summary>
        WeaponObjectScript Weapon { get; set; }
    }
}

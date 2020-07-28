/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Weapon object interface
    /// </summary>
    public interface IWeaponObject : IItemObject
    {
        /// <summary>
        /// Damage
        /// </summary>
        float Damage { get; }

        /// <summary>
        /// Kockback inpulse
        /// </summary>
        float KnockbackImpulse { get; }

        /// <summary>
        /// Distance
        /// </summary>
        float Distance { get; }
    }
}

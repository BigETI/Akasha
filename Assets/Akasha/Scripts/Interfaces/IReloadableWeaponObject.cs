using Akasha.Objects;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Reloadable weapon object
    /// </summary>
    public interface IReloadableWeaponObject : IWeaponObject
    {
        /// <summary>
        /// Ammo
        /// </summary>
        AmmoObjectScript Ammo { get; }

        /// <summary>
        /// Ammo capacity
        /// </summary>
        uint AmmoCapacity { get; }

        /// <summary>
        /// Reload time
        /// </summary>
        float ReloadTime { get; }

        /// <summary>
        /// Bullet hole asset
        /// </summary>
        GameObject BulletHoleAsset { get; }

        /// <summary>
        /// Projectile asset
        /// </summary>
        GameObject ProjectileAsset { get; }
    }
}

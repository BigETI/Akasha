using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Weapon object interface
    /// </summary>
    public interface IWeaponObject : IScriptableObject
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

        /// <summary>
        /// Ammo capacity
        /// </summary>
        uint AmmoCapacity { get; }

        /// <summary>
        /// Shoot time
        /// </summary>
        float ShootTime { get; }

        /// <summary>
        /// Reload time
        /// </summary>
        float ReloadTime { get; }

        /// <summary>
        /// Weapon name string
        /// </summary>
        string WeaponName { get; }

        /// <summary>
        /// Weapon icon sprite
        /// </summary>
        Sprite WeaponIconSprite { get; }

        /// <summary>
        /// Bullet hole asset
        /// </summary>
        GameObject BulletHoleAsset { get; }

        /// <summary>
        /// Weapon and hands asset
        /// </summary>
        GameObject WeaponAndHandsAsset { get; }

        /// <summary>
        /// Projectile asset
        /// </summary>
        GameObject ProjectileAsset { get; }
    }
}

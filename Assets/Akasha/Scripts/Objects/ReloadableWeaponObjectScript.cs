using UnityEngine;

/// <summary>
/// Akasha objects namespace
/// </summary>
namespace Akasha.Objects
{
    /// <summary>
    /// Reloadable weapon object script class
    /// </summary>
    [CreateAssetMenu(fileName = "ReloadableWeapon", menuName = "Akasha/Reloadable weapon")]
    public class ReloadableWeaponObjectScript : WeaponObjectScript, IReloadableWeaponObject
    {
        /// <summary>
        /// Ammo
        /// </summary>
        [SerializeField]
        private AmmoObjectScript ammo = default;

        /// <summary>
        /// Ammo capacity
        /// </summary>
        [SerializeField]
        [Range(1U, 10000U)]
        private uint ammoCapacity = 1U;

        /// <summary>
        /// Reload time
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float reloadTime = 1.0f;

        /// <summary>
        /// Bullet hole asset
        /// </summary>
        [SerializeField]
        private GameObject bulletHoleAsset = default;

        /// <summary>
        /// Projectile asset
        /// </summary>
        [SerializeField]
        private GameObject projectileAsset = default;

        /// <summary>
        /// Ammo
        /// </summary>
        public AmmoObjectScript Ammo => ammo;

        //// <summary>
        /// Ammo capacity
        /// </summary>
        public uint AmmoCapacity => (uint)(Mathf.Max(ammoCapacity, 1));

        /// <summary>
        /// Reload time
        /// </summary>
        public float ReloadTime => Mathf.Max(reloadTime, 0.0f);

        /// <summary>
        /// Bullet hole asset
        /// </summary>
        public GameObject BulletHoleAsset => bulletHoleAsset;

        /// <summary>
        /// Projectile asset
        /// </summary>
        public GameObject ProjectileAsset => projectileAsset;

        /// <summary>
        /// Key
        /// </summary>
        public override string Key => "ReloadableWeapons/" + name;
    }
}

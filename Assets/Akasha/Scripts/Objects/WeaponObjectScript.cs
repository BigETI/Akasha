using UnityEngine;
using UnityTranslator.Objects;

/// <summary>
/// Akasha objects namespace
/// </summary>
namespace Akasha.Objects
{
    /// <summary>
    /// Weapon object script class
    /// </summary>
    [CreateAssetMenu(fileName = "Weapon", menuName = "Akasha/Weapon")]
    public class WeaponObjectScript : ScriptableObject, IWeaponObject
    {
        /// <summary>
        /// Damage
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float damage = 25.0f;

        /// <summary>
        /// Kockback inpulse
        /// </summary>
        [SerializeField]
        [Range(0.0f, 10000.0f)]
        private float knockbackImpulse = 25.0f;

        /// <summary>
        /// Distance
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100000.0f)]
        private float distance = 1000.0f;

        /// <summary>
        /// Ammo capacity
        /// </summary>
        [SerializeField]
        [Range(1U, 10000U)]
        private uint ammoCapacity = 1U;

        /// <summary>
        /// Shoot time
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float shootTime = 0.0625f;

        /// <summary>
        /// Reload time
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float reloadTime = 1.0f;

        /// <summary>
        /// Weapon name string translation
        /// </summary>
        [SerializeField]
        private StringTranslationObjectScript weaponNameStringTranslation = default;

        /// <summary>
        /// Weapon icon sprite translation
        /// </summary>
        [SerializeField]
        private SpriteTranslationObjectScript weaponIconSpriteTranslation = default;

        /// <summary>
        /// Bullet hole asset
        /// </summary>
        [SerializeField]
        private GameObject bulletHoleAsset = default;

        /// <summary>
        /// Weapon and hands asset
        /// </summary>
        [SerializeField]
        private GameObject weaponAndHandsAsset = default;

        /// <summary>
        /// Projectile asset
        /// </summary>
        [SerializeField]
        private GameObject projectileAsset = default;

        /// <summary>
        /// Damage
        /// </summary>
        public float Damage => Mathf.Max(damage, 0.0f);

        /// <summary>
        /// Kockback inpulse
        /// </summary>
        public float KnockbackImpulse => knockbackImpulse;

        /// <summary>
        /// Distance
        /// </summary>
        public float Distance => distance;

        /// <summary>
        /// Ammo capacity
        /// </summary>
        public uint AmmoCapacity => (uint)(Mathf.Max(ammoCapacity, 1));

        /// <summary>
        /// Shoot time
        /// </summary>
        public float ShootTime => Mathf.Max(shootTime, 0.0f);

        /// <summary>
        /// Reload time
        /// </summary>
        public float ReloadTime => Mathf.Max(reloadTime, 0.0f);

        /// <summary>
        /// Weapon name string
        /// </summary>
        public string WeaponName => ((weaponNameStringTranslation == null) ? string.Empty : weaponNameStringTranslation.ToString());

        /// <summary>
        /// Weapon icon sprite
        /// </summary>
        public Sprite WeaponIconSprite => ((weaponIconSpriteTranslation == null) ? null : weaponIconSpriteTranslation.Sprite);

        /// <summary>
        /// Bullet hole asset
        /// </summary>
        public GameObject BulletHoleAsset => bulletHoleAsset;

        /// <summary>
        /// Weapon and hands asset
        /// </summary>
        public GameObject WeaponAndHandsAsset => weaponAndHandsAsset;

        /// <summary>
        /// Projectile asset
        /// </summary>
        public GameObject ProjectileAsset => projectileAsset;
    }
}

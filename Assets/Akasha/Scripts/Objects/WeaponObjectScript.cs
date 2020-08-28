using UnityEngine;

/// <summary>
/// Akasha objects namespace
/// </summary>
namespace Akasha.Objects
{
    /// <summary>
    /// Weapon object script class
    /// </summary>
    [CreateAssetMenu(fileName = "Weapon", menuName = "Akasha/Weapon")]
    public class WeaponObjectScript : ItemObjectScript, IWeaponObject
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
        private float distance = 0.5f;

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
        /// Key
        /// </summary>
        public override string Key => $"Weapons/{ name }";
    }
}

using UnityEngine;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Hurtable ground controller script class
    /// </summary>
    public class HurtableGroundControllerScript : MonoBehaviour, IHurtableGroundController
    {
        /// <summary>
        /// Damage per second
        /// </summary>
        [SerializeField]
        private float damagePerSecond = 50.0f;

        /// <summary>
        /// Damage per second
        /// </summary>
        public float DamagePerSecond
        {
            get => Mathf.Max(damagePerSecond, 0.0f);
            set => damagePerSecond = Mathf.Max(value, 0.0f);
        }
    }
}

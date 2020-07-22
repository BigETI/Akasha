/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Explosive controller interface
    /// </summary>
    public interface IExplosiveController : IDestructibleController
    {
        /// <summary>
        /// Explosion radius
        /// </summary>
        float ExplosionRadius { get; set; }

        /// <summary>
        /// Maximal damage
        /// </summary>
        float MaximalDamage { get; set; }

        /// <summary>
        /// Maximal kockback impulse
        /// </summary>
        float MaximalKnockbackImpulse { get; set; }

        /// <summary>
        /// Explode
        /// </summary>
        void Explode();
    }
}

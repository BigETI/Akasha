/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Hurtable ground controller interface
    /// </summary>
    public interface IHurtableGroundController : IBehaviour
    {
        /// <summary>
        /// Damage per second
        /// </summary>
        float DamagePerSecond { get; set; }
    }
}

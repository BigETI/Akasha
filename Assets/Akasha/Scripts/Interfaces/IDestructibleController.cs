using System;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Destructible controller interface
    /// </summary>
    public interface IDestructibleController : IBehaviour
    {
        /// <summary>
        /// Health
        /// </summary>
        float Health { get; set; }

        /// <summary>
        /// On destroyed
        /// </summary>
        event Action OnDestroyed;
    }
}

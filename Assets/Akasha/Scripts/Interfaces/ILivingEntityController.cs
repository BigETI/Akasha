﻿/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Living entity controller interface
    /// </summary>
    public interface ILivingEntityController : IEntityController
    {
        /// <summary>
        /// Health
        /// </summary>
        float Health { get; set; }

        /// <summary>
        /// Maximal health
        /// </summary>
        float MaximalHealth { get; set; }

        /// <summary>
        /// Is alive
        /// </summary>
        bool IsAlive { get; }

        /// <summary>
        /// On destroyed
        /// </summary>
        event DiedDelegete OnDied;
    }
}

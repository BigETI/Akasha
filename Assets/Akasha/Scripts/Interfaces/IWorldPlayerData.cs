/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// World player data interface
    /// </summary>
    public interface IWorldPlayerData : IWorldEntityData
    {
        /// <summary>
        /// Fullness
        /// </summary>
        float Fullness { get; set; }

        /// <summary>
        /// Stamina
        /// </summary>
        float Stamina { get; set; }

        /// <summary>
        /// Is exhausted
        /// </summary>
        bool IsExhausted { get; set; }

        /// <summary>
        /// Elapsed hit cooldown time
        /// </summary>
        float ElapsedHitCooldownTime { get; set; }

        /// <summary>
        /// Elapsed stamina regeneration cooldown time
        /// </summary>
        float ElapsedStaminaRegenerationCooldownTime { get; set; }

        /// <summary>
        /// Known crafting recipes
        /// </summary>
        string[] KnownCraftingRecipes { get; set; }
    }
}

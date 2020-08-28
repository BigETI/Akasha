using Akasha.Data;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Character controller interface
    /// </summary>
    public interface ICharacterController : ILivingEntityController
    {
        /// <summary>
        /// Minimal horizontal rotation
        /// </summary>
        float MinimalHorizontalRotation { get; set; }

        /// <summary>
        /// Maximal horizontal rotation
        /// </summary>
        float MaximalHorizontalRotation { get; set; }

        /// <summary>
        /// Gravity magnitude
        /// </summary>
        float GravityMagnitude { get; set; }

        /// <summary>
        /// Movement speed
        /// </summary>
        float MovementSpeed { get; set; }

        /// <summary>
        /// Sprint multiplier
        /// </summary>
        float SprintMultiplier { get; set; }

        /// <summary>
        /// Sneak multiplier
        /// </summary>
        float SneakMultiplier { get; set; }

        /// <summary>
        /// Jump height
        /// </summary>
        float JumpHeight { get; set; }

        /// <summary>
        /// Minimal fall damage speed
        /// </summary>
        float MinimalFallDamageSpeed { get; set; }

        /// <summary>
        /// Maximal fall damage speed
        /// </summary>
        float MaximalFallDamageSpeed { get; set; }

        /// <summary>
        /// Default hit cooldown time
        /// </summary>
        float DefaultHitCooldownTime { get; set; }

        /// <summary>
        /// Stamina depletion per second
        /// </summary>
        float StaminaDepletionPerSecond { get; set; }

        /// <summary>
        /// Stamina regeneration per second
        /// </summary>
        float StaminaRegenerationPerSecond { get; set; }

        /// <summary>
        /// Stamina regeneration cooldown time
        /// </summary>
        float StaminaRegenerationCooldownTime { get; set; }

        /// <summary>
        /// Fullness
        /// </summary>
        float Fullness { get; set; }

        /// <summary>
        /// Maximal fullness
        /// </summary>
        float MaximalFullness { get; set; }

        /// <summary>
        /// Hunger per second
        /// </summary>
        float HungerPerSecond { get; set; }

        /// <summary>
        /// Hunger damage per second
        /// </summary>
        float HungerDamagePerSecond { get; set; }

        /// <summary>
        /// Interaction distance
        /// </summary>
        float InteractionDistance { get; set; }

        /// <summary>
        /// Inventory
        /// </summary>
        InventoryData Inventory { get; set; }

        /// <summary>
        /// Eyes transform
        /// </summary>
        Transform EyesTransform { get; set; }

        /// <summary>
        /// Rotation
        /// </summary>
        Vector2 Rotation { get; set; }

        /// <summary>
        /// Known crafting recipes lookup
        /// </summary>
        IReadOnlyDictionary<string, ICraftingRecipesObject> KnownCraftingRecipesLookup { get; }

        /// <summary>
        /// Vertical velocity magnitude
        /// </summary>
        float VerticalVelocityMagnitude { get; }

        /// <summary>
        /// Movement
        /// </summary>
        Vector2 Movement { get; set; }

        /// <summary>
        /// Running mode
        /// </summary>
        ERunningMode RunningMode { get; set; }

        /// <summary>
        /// Is hitting
        /// </summary>
        bool IsHitting { get; set; }

        /// <summary>
        /// Selected inventory item slot index
        /// </summary>
        int SelectedInventoryItemSlotIndex { get; set; }

        /// <summary>
        /// Selected inventory item
        /// </summary>
        IInventoryItemData SelectedInventoryItem { get; }

        /// <summary>
        /// Maximal hit cooldown time
        /// </summary>
        float MaximalHitCooldownTime { get; }

        /// <summary>
        /// Elapsed hit cooldown time
        /// </summary>
        float ElapsedHitCooldownTime { get; }

        /// <summary>
        /// Stamina
        /// </summary>
        float Stamina { get; }

        /// <summary>
        /// Elapsed stamina regeneration cooldown time
        /// </summary>
        float ElapsedStaminaRegenerationCooldownTime { get; }

        /// <summary>
        /// Is exhausted
        /// </summary>
        bool IsExhausted { get; }

        /// <summary>
        /// Inventory UI controller
        /// </summary>
        IInventoryUIController InventoryUIController { get; }

        /// <summary>
        /// New world player data snapshot
        /// </summary>
        WorldPlayerData NewWorldPlayerDataSnapshot { get; }

        /// <summary>
        /// Place block
        /// </summary>
        void PlaceBlock();

        /// <summary>
        /// Get targeted block
        /// </summary>
        /// <param name="collisionNormalDistance">Collision normal distance</param>
        /// <returns>Targeted block</returns>
        ITargetedBlock GetTargetedBlock(float collisionNormalDistance);

        /// <summary>
        /// Interact
        /// </summary>
        void Interact();

        /// <summary>
        /// Jump
        /// </summary>
        void Jump();

        /// <summary>
        /// Shoot
        /// </summary>
        void Shoot();

        /// <summary>
        /// Reload
        /// </summary>
        void Reload();

        /// <summary>
        /// Select previous inventory item slot
        /// </summary>
        void SelectPreviousInventoryItemSlot();

        /// <summary>
        /// Select next inventory item slot
        /// </summary>
        void SelectNextInventoryItemSlot();

        /// <summary>
        /// Learn crafting recipes
        /// </summary>
        /// <param name="craftingRecipes">Crafting recipes</param>
        /// <returns>"true" if successful, otherwise "false"</returns>
        bool LearnCraftingRecipes(ICraftingRecipesObject craftingRecipes);
    }
}

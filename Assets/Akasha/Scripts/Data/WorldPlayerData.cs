using Akasha.Data;
using System;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// World player data class
    /// </summary>
    [Serializable]
    public class WorldPlayerData : WorldEntityData, IWorldPlayerData
    {
        /// <summary>
        /// Fullness
        /// </summary>
        [SerializeField]
        private float fullness;

        /// <summary>
        /// Stamina
        /// </summary>
        [SerializeField]
        private float stamina;

        /// <summary>
        /// Is exhausted
        /// </summary>
        [SerializeField]
        private bool isExhausted;

        /// <summary>
        /// Elapsed hit cooldown time
        /// </summary>
        [SerializeField]
        private float elapsedHitCooldownTime;

        /// <summary>
        /// Elapsed stamina regeneration cooldown time
        /// </summary>
        [SerializeField]
        private float elapsedStaminaRegenerationCooldownTime;

        /// <summary>
        /// Known crafting recipes
        /// </summary>
        [SerializeField]
        private string[] knownCraftingRecipes;

        /// <summary>
        /// Fullness
        /// </summary>
        public float Fullness
        {
            get => fullness;
            set => fullness = value;
        }

        /// <summary>
        /// Stamina
        /// </summary>
        public float Stamina
        {
            get => stamina;
            set => stamina = value;
        }

        /// <summary>
        /// Is exhausted
        /// </summary>
        public bool IsExhausted
        {
            get => isExhausted;
            set => isExhausted = value;
        }

        /// <summary>
        /// Elapsed hit cooldown time
        /// </summary>
        public float ElapsedHitCooldownTime
        {
            get => elapsedHitCooldownTime;
            set => elapsedHitCooldownTime = value;
        }

        /// <summary>
        /// Elapsed stamina regeneration cooldown time
        /// </summary>
        public float ElapsedStaminaRegenerationCooldownTime
        {
            get => elapsedStaminaRegenerationCooldownTime;
            set => elapsedStaminaRegenerationCooldownTime = value;
        }

        /// <summary>
        /// Known crafting recipes
        /// </summary>
        public string[] KnownCraftingRecipes
        {
            get
            {
                if (knownCraftingRecipes == null)
                {
                    knownCraftingRecipes = Array.Empty<string>();
                }
                return knownCraftingRecipes;
            }
            set => knownCraftingRecipes = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="guid">GUID</param>
        /// <param name="entity">Entity</param>
        /// <param name="health">Health</param>
        /// <param name="armor">Armor</param>
        /// <param name="blockID">Block ID</param>
        /// <param name="positionOffset">Position offset</param>
        /// <param name="rotation">Rotation</param>
        /// <param name="inventory">Inventory</param>
        /// <param name="fullness">Fullness</param>
        /// <param name="stamina">Stamina</param>
        /// <param name="isExhausted">Is exhausted</param>
        /// <param name="elapsedHitCooldownTime">Elapsed hit cooldown time</param>
        /// <param name="elapsedStaminaRegenerationCooldownTime">Elapsed stamina regeneration cooldown time</param>
        /// <param name="knownCraftingRecipes">Known crafting recipes</param>
        public WorldPlayerData(Guid guid, IEntityObject entity, float health, float armor, BlockID blockID, Vector3 positionOffset, Vector2 rotation, InventoryData inventory, float fullness, float stamina, bool isExhausted, float elapsedHitCooldownTime, float elapsedStaminaRegenerationCooldownTime, string[] knownCraftingRecipes) : base(guid, entity, health, armor, blockID, positionOffset, rotation, inventory)
        {
            Fullness = fullness;
            Stamina = stamina;
            this.isExhausted = isExhausted;
            ElapsedHitCooldownTime = elapsedHitCooldownTime;
            ElapsedStaminaRegenerationCooldownTime = elapsedStaminaRegenerationCooldownTime;
            KnownCraftingRecipes = knownCraftingRecipes;
        }
    }
}

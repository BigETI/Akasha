using Akasha.Data;
using Akasha.Managers;
using Akasha.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Character controller script class
    /// </summary>
    public class CharacterControllerScript : LivingEntityControllerScript, ICharacterController
    {
        /// <summary>
        /// Minimal horizontal rotation
        /// </summary>
        [SerializeField]
        [Range(-90.0f, 0.0f)]
        private float minimalHorizontalRotation = -90.0f;

        /// <summary>
        /// Maximal horizontal rotation
        /// </summary>
        [SerializeField]
        [Range(0.0f, 90.0f)]
        private float maximalHorizontalRotation = 90.0f;

        /// <summary>
        /// Gravity magnitude
        /// </summary>
        [SerializeField]
        private float gravityMagnitude = 9.81f;

        /// <summary>
        /// Movement speed
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float movementSpeed = 5.0f;

        /// <summary>
        /// Sprint multiplier
        /// </summary>
        [SerializeField]
        [Range(1.0f, 1000.0f)]
        private float sprintMultiplier = 1.5f;

        /// <summary>
        /// Sneak multiplier
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float sneakMultiplier = 0.5f;

        /// <summary>
        /// Jump height
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float jumpHeight = 1.5f;

        /// <summary>
        /// Minimal fall damage speed
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float minimalFallDamageSpeed = 25.0f;

        /// <summary>
        /// Maximal fall damage speed
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float maximalFallDamageSpeed = 50.0f;

        /// <summary>
        /// Default hit cooldown time
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float defaultHitCooldownTime = 0.5f;

        /// <summary>
        /// Stamina depletion per second
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float staminaDepletionPerSecond = 0.0625f;

        /// <summary>
        /// Stamina regeneration per second
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float staminaRegenerationPerSecond = 0.25f;

        /// <summary>
        /// Stamina regeneration cooldown time
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float staminaRegenerationCooldownTime = 0.5f;

        /// <summary>
        /// Fullness
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float fullness = 25.0f;

        /// <summary>
        /// Maximal fullness
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float maximalFullness = 100.0f;

        /// <summary>
        /// Hunger per second
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float hungerPerSecond = 0.03125f;

        /// <summary>
        /// Hunger damage per second
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float hungerDamagePerSecond = 1.0f;

        /// <summary>
        /// Interaction distance
        /// </summary>
        [SerializeField]
        private float interactionDistance = 3.0f;

        /// <summary>
        /// Inventory
        /// </summary>
        [SerializeField]
        private InventoryData inventory = default;

        /// <summary>
        /// Eyes transform
        /// </summary>
        [SerializeField]
        private Transform eyesTransform = default;

        /// <summary>
        /// Movement
        /// </summary>
        private Vector2 movement;

        /// <summary>
        /// Selected inventory item slot index
        /// </summary>
        private int selectedInventoryItemSlotIndex;

        /// <summary>
        /// Rotation
        /// </summary>
        private Vector2 rotation;

        /// <summary>
        /// Known crafting recipes lookup
        /// </summary>
        private readonly Dictionary<string, ICraftingRecipesObject> knownCraftingRecipesLookup = new Dictionary<string, ICraftingRecipesObject>();

        /// <summary>
        /// Raycast hits
        /// </summary>
        private RaycastHit[] raycastHits = Array.Empty<RaycastHit>();

        /// <summary>
        /// Minimal horizontal rotation
        /// </summary>
        public float MinimalHorizontalRotation
        {
            get => Mathf.Min(minimalHorizontalRotation, maximalHorizontalRotation);
            set => minimalHorizontalRotation = value;
        }

        /// <summary>
        /// Maximal horizontal rotation
        /// </summary>
        public float MaximalHorizontalRotation
        {
            get => Mathf.Max(minimalHorizontalRotation, maximalHorizontalRotation);
            set => maximalHorizontalRotation = value;
        }

        /// <summary>
        /// Gravity magnitude
        /// </summary>
        public float GravityMagnitude
        {
            get => Mathf.Max(gravityMagnitude, 0.0f);
            set => gravityMagnitude = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Movement speed
        /// </summary>
        public float MovementSpeed
        {
            get => Mathf.Max(movementSpeed, 0.0f);
            set => movementSpeed = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Sprint multiplier
        /// </summary>
        public float SprintMultiplier
        {
            get => Mathf.Max(sprintMultiplier, 1.0f);
            set => sprintMultiplier = Mathf.Max(value, 1.0f);
        }

        /// <summary>
        /// Sneak multiplier
        /// </summary>
        public float SneakMultiplier
        {
            get => Mathf.Clamp(sneakMultiplier, 0.0f, 1.0f);
            set => sneakMultiplier = Mathf.Clamp(value, 0.0f, 1.0f);
        }

        /// <summary>
        /// Jump height
        /// </summary>
        public float JumpHeight
        {
            get => Mathf.Max(jumpHeight, 0.0f);
            set => jumpHeight = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Minimal fall damage speed
        /// </summary>
        public float MinimalFallDamageSpeed
        {
            get => Mathf.Clamp(minimalFallDamageSpeed, 0.0f, MaximalFallDamageSpeed);
            set => minimalFallDamageSpeed = Mathf.Clamp(value, 0.0f, MaximalFallDamageSpeed);
        }

        /// <summary>
        /// Maximal fall damage speed
        /// </summary>
        public float MaximalFallDamageSpeed
        {
            get => Mathf.Max(maximalFallDamageSpeed, 0.0f);
            set => maximalFallDamageSpeed = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Default hit cooldown time
        /// </summary>
        public float DefaultHitCooldownTime
        {
            get => Mathf.Max(defaultHitCooldownTime, 0.0f);
            set => defaultHitCooldownTime = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Stamina depletion per second
        /// </summary>
        public float StaminaDepletionPerSecond
        {
            get => Mathf.Max(staminaDepletionPerSecond, 0.0f);
            set => staminaDepletionPerSecond = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Stamina regeneration per second
        /// </summary>
        public float StaminaRegenerationPerSecond
        {
            get => Mathf.Max(staminaRegenerationPerSecond, 0.0f);
            set => staminaRegenerationPerSecond = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Stamina regeneration cooldown time
        /// </summary>
        public float StaminaRegenerationCooldownTime
        {
            get => Mathf.Max(staminaRegenerationCooldownTime, 0.0f);
            set => staminaRegenerationCooldownTime = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Fullness
        /// </summary>
        public float Fullness
        {
            get => Mathf.Clamp(fullness, 0.0f, MaximalFullness);
            set => fullness = Mathf.Clamp(value, 0.0f, MaximalFullness);
        }

        /// <summary>
        /// Maximal fullness
        /// </summary>
        public float MaximalFullness
        {
            get => Mathf.Max(maximalFullness, 0.0f);
            set => maximalFullness = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Hunger per second
        /// </summary>
        public float HungerPerSecond
        {
            get => Mathf.Max(hungerPerSecond, 0.0f);
            set => hungerPerSecond = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Hunger damage per second
        /// </summary>
        public float HungerDamagePerSecond
        {
            get => Mathf.Max(hungerDamagePerSecond, 0.0f);
            set => hungerDamagePerSecond = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Interaction distance
        /// </summary>
        public float InteractionDistance
        {
            get => Mathf.Max(interactionDistance, 0.0f);
            set => interactionDistance = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Inventory
        /// </summary>
        public InventoryData Inventory
        {
            get
            {
                if (inventory == null)
                {
                    inventory = new InventoryData();
                }
                return inventory;
            }
            set => inventory = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Eyes transform
        /// </summary>
        public Transform EyesTransform
        {
            get => eyesTransform;
            set => eyesTransform = value;
        }

        /// <summary>
        /// Rotation
        /// </summary>
        public Vector2 Rotation
        {
            get => rotation;
            set => rotation = new Vector2(Mathf.Clamp(value.x, MinimalHorizontalRotation, MaximalHorizontalRotation), Mathf.Repeat(value.y, 360.0f - float.Epsilon));
        }

        /// <summary>
        /// Known crafting recipes lookup
        /// </summary>
        public IReadOnlyDictionary<string, ICraftingRecipesObject> KnownCraftingRecipesLookup => knownCraftingRecipesLookup;

        /// <summary>
        /// Vertical velocity magnitude
        /// </summary>
        public float VerticalVelocityMagnitude { get; private set; }

        /// <summary>
        /// Is on ground
        /// </summary>
        public bool IsOnGround { get; private set; }

        /// <summary>
        /// Movement
        /// </summary>
        public Vector2 Movement
        {
            get => movement;
            set => movement = ((value.sqrMagnitude > 1.0f) ? value.normalized : value);
        }

        /// <summary>
        /// Running mode
        /// </summary>
        public ERunningMode RunningMode { get; set; }

        /// <summary>
        /// Is hitting
        /// </summary>
        public bool IsHitting { get; set; }

        /// <summary>
        /// Selected inventory item slot index
        /// </summary>
        public int SelectedInventoryItemSlotIndex
        {
            get
            {
                int item_count = Mathf.Min((InventoryUIController == null) ? 0 : InventoryUIController.InventoryItemSlotControllers.Length, Inventory.Items.Count);
                if (selectedInventoryItemSlotIndex >= item_count)
                {
                    selectedInventoryItemSlotIndex = item_count - 1;
                    ElapsedHitCooldownTime = 0.0f;
                }
                return selectedInventoryItemSlotIndex;
            }
            set
            {
                int item_count = Mathf.Min((InventoryUIController == null) ? 0 : InventoryUIController.InventoryItemSlotControllers.Length, Inventory.Items.Count);
                int selected_inventory_item_slot_index = value;
                if (selected_inventory_item_slot_index >= item_count)
                {
                    selected_inventory_item_slot_index = item_count - 1;
                }
                else if (selected_inventory_item_slot_index < -1)
                {
                    selected_inventory_item_slot_index = -1;
                }
                if (selectedInventoryItemSlotIndex != selected_inventory_item_slot_index)
                {
                    selectedInventoryItemSlotIndex = selected_inventory_item_slot_index;
                    ElapsedHitCooldownTime = 0.0f;
                }
            }
        }

        /// <summary>
        /// Selected inventory item
        /// </summary>
        public IInventoryItemData SelectedInventoryItem
        {
            get
            {
                IInventoryItemData ret = null;
                int selected_inventory_item_slot_index = SelectedInventoryItemSlotIndex;
                if ((selected_inventory_item_slot_index >= 0) && (selected_inventory_item_slot_index < Inventory.Items.Count))
                {
                    ret = Inventory.Items[selected_inventory_item_slot_index];
                }
                return ret;
            }
        }

        /// <summary>
        /// Maximal hit cooldown time
        /// </summary>
        public float MaximalHitCooldownTime
        {
            get
            {
                IInventoryItemData selected_item = SelectedInventoryItem;
                return ((selected_item == null) ? DefaultHitCooldownTime : selected_item.Item.MaximalHitCooldownTime);
            }
        }

        /// <summary>
        /// Elapsed hit cooldown time
        /// </summary>
        public float ElapsedHitCooldownTime { get; private set; }

        /// <summary>
        /// Stamina
        /// </summary>
        public float Stamina { get; private set; } = 1.0f;

        /// <summary>
        /// Elapsed stamina regeneration cooldown time
        /// </summary>
        public float ElapsedStaminaRegenerationCooldownTime { get; private set; }

        /// <summary>
        /// Is exhausted
        /// </summary>
        public bool IsExhausted { get; private set; }

        /// <summary>
        /// Inventory UI controller
        /// </summary>
        public IInventoryUIController InventoryUIController { get; private set; }

        /// <summary>
        /// World player data snapshot
        /// </summary>
        public WorldPlayerData WorldPlayerDataSnapshot
        {
            get => new WorldPlayerData(GUID, EntityObject, Health, Armor, (WorldTransformController == null) ? BlockID.Zero : WorldTransformController.BlockID, (WorldTransformController == null) ? Vector3.zero : WorldTransformController.PositionOffset, Rotation, Inventory, Fullness, Stamina, IsExhausted, ElapsedHitCooldownTime, ElapsedStaminaRegenerationCooldownTime, (new List<string>(KnownCraftingRecipesLookup.Keys)).ToArray());
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                EntityObject = value.Entity as EntityObjectScript;
                Health = value.Health;
                Armor = value.Armor;
                if (WorldTransformController != null)
                {
                    WorldTransformController.BlockID = value.BlockID;
                    // TODO: Fix position offset
                    //WorldTransformController.PositionOffset = value.PositionOffset;
                }
                Rotation = value.Rotation;
                Inventory = value.Inventory;
                Fullness = value.Fullness;
                Stamina = value.Stamina;
                IsExhausted = value.IsExhausted;
                ElapsedHitCooldownTime = value.ElapsedHitCooldownTime;
                ElapsedStaminaRegenerationCooldownTime = value.ElapsedStaminaRegenerationCooldownTime;
                knownCraftingRecipesLookup.Clear();
                foreach (string key in value.KnownCraftingRecipes)
                {
                    if (key != null)
                    {
                        CraftingRecipesObjectScript crafting_recipe = Resources.Load<CraftingRecipesObjectScript>($"CraftingRecipes/{ key }");
                        if (crafting_recipe)
                        {
                            knownCraftingRecipesLookup.Add(key, crafting_recipe);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Place block
        /// </summary>
        public void PlaceBlock()
        {
            IInventoryItemData item = SelectedInventoryItem;
            if (item != null)
            {
                if ((item.Item is BlockObjectScript block) && (item.Quantity > 0U) && SetTargetedBlock(new BlockData(block, block.InitialHealth), 1.0f))
                {
                    Inventory.RemoveItems(item.Item, 1U);
                }
            }
        }

        /// <summary>
        /// Get targeted block
        /// </summary>
        /// <param name="collisionNormalDistance">Collision normal distance</param>
        /// <returns>Targeted block</returns>
        public ITargetedBlock GetTargetedBlock(float collisionNormalDistance)
        {
            ITargetedBlock ret = null;
            WorldManagerScript world_manager = WorldManagerScript.Instance;
            if (world_manager != null)
            {
                int raycast_hit_count = PhysicsUtils.Raycast(eyesTransform.position, eyesTransform.forward, InteractionDistance, ref raycastHits);
                if (raycast_hit_count > 0)
                {
                    Vector3Int chunk_size = world_manager.ChunkSize;
                    float distance = float.PositiveInfinity;
                    BlockID? block_id = null;
                    for (int index = 0; index < raycast_hit_count; index++)
                    {
                        RaycastHit raycast_hit = raycastHits[index];
                        if ((raycast_hit.distance < distance) && !(raycast_hit.collider.isTrigger) && (raycast_hit.collider.transform.parent != null))
                        {
                            ChunkControllerScript chunk_controller = raycast_hit.collider.GetComponentInParent<ChunkControllerScript>();
                            if (chunk_controller != null)
                            {
                                ChunkID chunk_id = chunk_controller.ChunkID;
                                Vector3 local_position = raycast_hit.collider.transform.position - chunk_controller.transform.position;
                                block_id = new BlockID(Mathf.RoundToInt(local_position.x + (chunk_size.x * 0.5f) - 0.5f) + ((long)(chunk_id.X) * chunk_size.x) + Mathf.RoundToInt(raycast_hit.normal.x * collisionNormalDistance), Mathf.RoundToInt(local_position.y + (chunk_size.y * 0.5f) - 0.5f) + ((long)(chunk_id.Y) * chunk_size.y) + Mathf.RoundToInt(raycast_hit.normal.y * collisionNormalDistance), Mathf.RoundToInt(local_position.z + (chunk_size.z * 0.5f) - 0.5f) + ((long)(chunk_id.Z) * chunk_size.z) + Mathf.RoundToInt(raycast_hit.normal.z * collisionNormalDistance));
                                distance = raycast_hit.distance;
                            }
                        }
                    }
                    if (block_id != null)
                    {
                        ret = new TargetedBlock(world_manager.GetBlock(block_id.Value), block_id.Value);
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Set targeted block
        /// </summary>
        /// <param name="block">Block</param>
        /// <param name="distanceCollisionNormal">Collision normal distance</param>
        /// <returns>"true" if successful, otherwise "false"</returns>
        private bool SetTargetedBlock(BlockData block, float collisionNormalDistance)
        {
            bool ret = false;
            WorldManagerScript world_manager = WorldManagerScript.Instance;
            if (world_manager)
            {
                ITargetedBlock targeted_block = GetTargetedBlock(collisionNormalDistance);
                if ((targeted_block != null) && targeted_block.IsNothing)
                {
                    world_manager.SetBlock(targeted_block.ID, block);
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>
        /// Interact
        /// </summary>
        public void Interact()
        {
            // TODO
        }

        /// <summary>
        /// Jump
        /// </summary>
        public void Jump()
        {
            if (IsAlive && IsOnGround)
            {
                VerticalVelocityMagnitude = Mathf.Sqrt(2.0f * JumpHeight * GravityMagnitude);
            }
        }

        /// <summary>
        /// Shoot
        /// </summary>
        public void Shoot()
        {
            // TODO
        }

        /// <summary>
        /// Reload
        /// </summary>
        public void Reload()
        {
            // TODO
        }

        /// <summary>
        /// Select previous inventory item slot
        /// </summary>
        public void SelectPreviousInventoryItemSlot()
        {
            if (SelectedInventoryItemSlotIndex > 0)
            {
                --SelectedInventoryItemSlotIndex;
            }
            else
            {
                SelectedInventoryItemSlotIndex = Inventory.Items.Count - 1;
            }
        }

        /// <summary>
        /// Select next inventory item slot
        /// </summary>
        public void SelectNextInventoryItemSlot()
        {
            if (SelectedInventoryItemSlotIndex < (Inventory.Items.Count - 1))
            {
                ++SelectedInventoryItemSlotIndex;
            }
            else
            {
                SelectedInventoryItemSlotIndex = Mathf.Min(0, Inventory.Items.Count - 1);
            }
        }

        /// <summary>
        /// Learn crafting recipes
        /// </summary>
        /// <param name="craftingRecipes">Crafting recipes</param>
        /// <returns>"true" if successful, otherwise "false"</returns>
        public bool LearnCraftingRecipes(ICraftingRecipesObject craftingRecipes)
        {
            if (craftingRecipes == null)
            {
                throw new ArgumentNullException(nameof(craftingRecipes));
            }
            bool ret = false;
            if (!(knownCraftingRecipesLookup.ContainsKey(craftingRecipes.name)))
            {
                knownCraftingRecipesLookup.Add(craftingRecipes.name, craftingRecipes);
                ret = true;
            }
            return ret;
        }

        /// <summary>
        /// Start
        /// </summary>
        protected override void Start()
        {
            base.Start();
            InventoryUIController = FindObjectOfType<InventoryUIControllerScript>(true);
            ElapsedStaminaRegenerationCooldownTime = StaminaRegenerationCooldownTime;
        }

        /// <summary>
        /// Update
        /// </summary>
        protected override void Update()
        {
            base.Update();
            float delta_time = Time.deltaTime;
            transform.localRotation = Quaternion.AngleAxis(rotation.y, Vector3.up);
            if (eyesTransform)
            {
                eyesTransform.localRotation = Quaternion.AngleAxis(rotation.x, Vector3.right);
            }
            VerticalVelocityMagnitude -= GravityMagnitude * delta_time;
            if (IsAlive)
            {
                float movement_speed = MovementSpeed;
                float stamina_regenration_cooldown_time = StaminaRegenerationCooldownTime;
                ElapsedStaminaRegenerationCooldownTime = Mathf.Clamp(ElapsedStaminaRegenerationCooldownTime + Time.deltaTime, 0.0f, stamina_regenration_cooldown_time);
                Fullness -= HungerPerSecond * Time.deltaTime;
                if (Fullness <= float.Epsilon)
                {
                    Health -= HungerDamagePerSecond * Time.deltaTime;
                }
                bool deplete_stamina = false;
                switch (RunningMode)
                {
                    case ERunningMode.Sprinting:
                        float sprint_multiplier = SprintMultiplier;
                        deplete_stamina = ((movement.magnitude * sprint_multiplier) > 1.0f) && !IsExhausted;
                        ElapsedStaminaRegenerationCooldownTime = (deplete_stamina ? 0.0f : ElapsedStaminaRegenerationCooldownTime);
                        if (!IsExhausted && (Stamina > float.Epsilon))
                        {
                            movement_speed *= sprint_multiplier;
                        }
                        break;
                    case ERunningMode.Sneaking:
                        float sneak_multiplier = SneakMultiplier;
                        deplete_stamina = ((movement.magnitude * sneak_multiplier) > 1.0f) && !IsExhausted;
                        ElapsedStaminaRegenerationCooldownTime = (deplete_stamina ? 0.0f : ElapsedStaminaRegenerationCooldownTime);
                        movement_speed *= sneak_multiplier;
                        break;
                }
                Stamina = (deplete_stamina ? Mathf.Clamp(Stamina - (StaminaDepletionPerSecond * Time.deltaTime), 0.0f, 1.0f) : ((ElapsedStaminaRegenerationCooldownTime + float.Epsilon) < stamina_regenration_cooldown_time ? Stamina : Mathf.Clamp(Stamina + (StaminaRegenerationPerSecond * Time.deltaTime), 0.0f, 1.0f)));
                IsExhausted = (Stamina <= float.Epsilon) || (Stamina < (1.0f - float.Epsilon) && IsExhausted);
                Move(((transform.right * movement.x) + (transform.forward * movement.y)) * (movement_speed * delta_time));
            }
            IsOnGround = Move(Vector3.up * (VerticalVelocityMagnitude * delta_time)) || TestCollision(transform.position + (Vector3.down * 0.0625f));
            if (IsOnGround)
            {
                float fall_speed = -VerticalVelocityMagnitude;
                float minimal_fall_damage_speed = MinimalFallDamageSpeed;
                if (fall_speed >= minimal_fall_damage_speed)
                {
                    float delta = MaximalFallDamageSpeed - minimal_fall_damage_speed;
                    Damage(MaximalHealth * ((delta > float.Epsilon) ? Mathf.Clamp((fall_speed - minimal_fall_damage_speed) / delta, 0.0f, 1.0f) : 1.0f));
                }
                VerticalVelocityMagnitude = 0.0f;
            }
            ElapsedHitCooldownTime += delta_time;
            if (IsHitting && (GameManager.GameState == EGameState.Playing))
            {
                WorldManagerScript world_manager = WorldManagerScript.Instance;
                if (world_manager)
                {
                    IInventoryItemData item = SelectedInventoryItem;
                    if ((item == null) || item.IsUsable)
                    {
                        ITargetedBlock targeted_block = GetTargetedBlock(0.0f);
                        if ((targeted_block != null) && targeted_block.IsABlock)
                        {
                            IFarmingToolData farming_tool = targeted_block.Block.Block.GetFarmingToolDataFromFarmingToolItem(item?.Item);
                            if (farming_tool != null)
                            {
                                while ((((item == null) || item.IsUsable)) && (ElapsedHitCooldownTime >= MaximalHitCooldownTime))
                                {
                                    FarmableItemData farmable_item = farming_tool.RandomFarmableItem;
                                    targeted_block = new TargetedBlock((targeted_block.Block.Health < farming_tool.Damage) ? default : (new BlockData(targeted_block.Block.Block, (ushort)(targeted_block.Block.Health - farming_tool.Damage))), targeted_block.ID);
                                    world_manager.SetBlock(targeted_block.ID, targeted_block.Block);
                                    if (farmable_item.FarmableItem && (farmable_item.Quantity > 0U))
                                    {
                                        Inventory.AddItems(farmable_item.FarmableItem, farmable_item.FarmableItem.MaximalHealth, farmable_item.Quantity);
                                    }
                                    if (farming_tool.FarmingToolItem && (item != null) && (item.Item != null) && (item.Item.MaximalHealth > 0U) && (item.Health > 0U))
                                    {
                                        item = new InventoryItemData(item.Item, item.Health - 1U, item.Quantity);
                                        Inventory.SetInventoryItemHealth((uint)selectedInventoryItemSlotIndex, item.Health);
                                    }
                                    ElapsedHitCooldownTime = 0.0f;
                                }
                            }
                            else
                            {
                                ElapsedHitCooldownTime = Mathf.Min(ElapsedHitCooldownTime, MaximalHitCooldownTime);
                            }
                        }
                        else
                        {
                            ElapsedHitCooldownTime = Mathf.Min(ElapsedHitCooldownTime, MaximalHitCooldownTime);
                        }
                    }
                    else
                    {
                        ElapsedHitCooldownTime = Mathf.Min(ElapsedHitCooldownTime, MaximalHitCooldownTime);
                    }
                }
                else
                {
                    ElapsedHitCooldownTime = Mathf.Min(ElapsedHitCooldownTime, MaximalHitCooldownTime);
                }
            }
            else
            {
                ElapsedHitCooldownTime = Mathf.Min(ElapsedHitCooldownTime, MaximalHitCooldownTime);
            }
        }
    }
}

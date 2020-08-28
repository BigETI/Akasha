using Akasha.Data;
using UnityEngine;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// HUD UI controller script class
    /// </summary>
    public class HUDUIControllerScript : MonoBehaviour, IHUDUIController
    {
        /// <summary>
        /// Blink time
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float blinkTime = 0.25f;

        /// <summary>
        /// Block health HUD radial progress
        /// </summary>
        [SerializeField]
        private HUDRadialProgressData blockHealthHUDRadialProgress = default;

        /// <summary>
        /// Hit HUD radial progress
        /// </summary>
        [SerializeField]
        private HUDRadialProgressData hitHUDRadialProgress = default;

        /// <summary>
        /// Stamina HUD radial progress
        /// </summary>
        [SerializeField]
        private HUDRadialProgressData staminaHUDRadialProgress = default;

        /// <summary>
        /// Fullness HUD radial progress
        /// </summary>
        [SerializeField]
        private HUDRadialProgressData fullnessHUDRadialProgress = default;

        /// <summary>
        /// Health HUD radial progress
        /// </summary>
        [SerializeField]
        private HUDRadialProgressData healthHUDRadialProgress = default;

        /// <summary>
        /// Armor HUD radial progress
        /// </summary>
        [SerializeField]
        private HUDRadialProgressData armorHUDRadialProgress = default;

        /// <summary>
        /// Blink time
        /// </summary>
        public float BlinkTime
        {
            get => Mathf.Max(blinkTime, 0.0f);
            set => blinkTime = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Block health HUD radial progress
        /// </summary>
        public HUDRadialProgressData BlockHealthHUDRadialProgress
        {
            get => blockHealthHUDRadialProgress;
            set => blockHealthHUDRadialProgress = value;
        }

        /// <summary>
        /// Hit HUD radial progress
        /// </summary>
        public HUDRadialProgressData HitHUDRadialProgress
        {
            get => hitHUDRadialProgress;
            set => hitHUDRadialProgress = value;
        }

        /// <summary>
        /// Stamina HUD radial progress
        /// </summary>
        public HUDRadialProgressData StaminaHUDRadialProgress
        {
            get => staminaHUDRadialProgress;
            set => staminaHUDRadialProgress = value;
        }

        /// <summary>
        /// Fullness HUD radial progress
        /// </summary>
        public HUDRadialProgressData FullnessHUDRadialProgress
        {
            get => fullnessHUDRadialProgress;
            set => fullnessHUDRadialProgress = value;
        }

        /// <summary>
        /// Health HUD radial progress
        /// </summary>
        public HUDRadialProgressData HealthHUDRadialProgress
        {
            get => healthHUDRadialProgress;
            set => healthHUDRadialProgress = value;
        }

        /// <summary>
        /// Armor HUD radial progress
        /// </summary>
        public HUDRadialProgressData ArmorHUDRadialProgress
        {
            get => armorHUDRadialProgress;
            set => armorHUDRadialProgress = value;
        }

        /// <summary>
        /// Elapsed blink time
        /// </summary>
        public float ElapsedBlinkTime { get; private set; }

        /// <summary>
        /// Blink state
        /// </summary>
        public bool BlinkState { get; private set; }

        /// <summary>
        /// Player character controller
        /// </summary>
        public CharacterControllerScript PlayerCharacterController { get; private set; }

        /// <summary>
        /// Update radial progress
        /// </summary>
        /// <param name="radialProgress">Radial progress</param>
        /// <param name="value">Value</param>
        /// <param name="showProgress">Show progress</param>
        /// <param name="forceBlinking">Force blinking</param>
        private void UpdateRadialProgress(HUDRadialProgressData radialProgress, float value, bool showProgress, bool forceBlinking)
        {
            float progress_speed = radialProgress.ProgressSpeed;
            float new_value = (radialProgress.AnimateProgress ? ((radialProgress.RadialProgress.Value < value) ? Mathf.Clamp(radialProgress.RadialProgress.Value + (progress_speed * Time.deltaTime), radialProgress.RadialProgress.Value, value) : ((radialProgress.RadialProgress.Value > value) ? Mathf.Clamp(radialProgress.RadialProgress.Value - (progress_speed * Time.deltaTime), value, radialProgress.RadialProgress.Value) : radialProgress.RadialProgress.Value)) : value);
            radialProgress.RadialProgress.gameObject.SetActive(showProgress && ((!forceBlinking && (radialProgress.BlinkThreshold <= new_value)) || BlinkState));
            radialProgress.RadialProgress.Value = new_value;
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            PlayerControllerScript player_controller = FindObjectOfType<PlayerControllerScript>();
            if (player_controller != null)
            {
                PlayerCharacterController = player_controller.GetComponent<CharacterControllerScript>();
            }
            blockHealthHUDRadialProgress.Reset();
            hitHUDRadialProgress.Reset();
            staminaHUDRadialProgress.Reset();
            fullnessHUDRadialProgress.Reset();
            healthHUDRadialProgress.Reset();
            armorHUDRadialProgress.Reset();
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            float blink_time = BlinkTime;
            if (blink_time > float.Epsilon)
            {
                ElapsedBlinkTime += Time.deltaTime;
                while (ElapsedBlinkTime > blink_time)
                {
                    BlinkState = !BlinkState;
                    ElapsedBlinkTime -= blink_time;
                }
            }
            if (PlayerCharacterController != null)
            {
                float maximal_hit_cooldown_time = PlayerCharacterController.MaximalHitCooldownTime;
                if (blockHealthHUDRadialProgress.RadialProgress)
                {
                    ITargetedBlock targeted_block = PlayerCharacterController.GetTargetedBlock(0.0f);
                    IInventoryItemData inventory_item_data = PlayerCharacterController.SelectedInventoryItem;
                    bool show_block_health = (((targeted_block != null) && targeted_block.IsABlock) && (targeted_block.Block.Block.GetFarmingToolDataFromFarmingToolItem(inventory_item_data?.Item) != null));
                    if (show_block_health)
                    {
                        UpdateRadialProgress(blockHealthHUDRadialProgress, show_block_health ? ((targeted_block.Block.Block.InitialHealth > 0) ? ((float)(targeted_block.Block.Health) / targeted_block.Block.Block.InitialHealth) : 0.0f) : 0.0f, true, false);
                    }
                    else
                    {
                        blockHealthHUDRadialProgress.RadialProgress.gameObject.SetActive(false);
                        blockHealthHUDRadialProgress.RadialProgress.Value = 0.0f;
                    }
                }
                if (hitHUDRadialProgress.RadialProgress)
                {
                    UpdateRadialProgress(hitHUDRadialProgress, (maximal_hit_cooldown_time > float.Epsilon) ? (PlayerCharacterController.ElapsedHitCooldownTime / maximal_hit_cooldown_time) : 0.0f, Mathf.Abs(maximal_hit_cooldown_time - PlayerCharacterController.ElapsedHitCooldownTime) > float.Epsilon, false);
                }
                if (staminaHUDRadialProgress.RadialProgress)
                {
                    UpdateRadialProgress(staminaHUDRadialProgress, PlayerCharacterController.Stamina * 0.5f, PlayerCharacterController.Stamina < (1.0f - float.Epsilon), PlayerCharacterController.IsExhausted);
                }
                if (fullnessHUDRadialProgress.RadialProgress)
                {
                    float maximal_fullness = PlayerCharacterController.MaximalFullness;
                    UpdateRadialProgress(fullnessHUDRadialProgress, (maximal_fullness > float.Epsilon) ? (PlayerCharacterController.Fullness / maximal_fullness) : 0.0f, true, false);
                }
                if (healthHUDRadialProgress.RadialProgress)
                {
                    float maximal_health = PlayerCharacterController.MaximalHealth;
                    UpdateRadialProgress(healthHUDRadialProgress, (maximal_health > float.Epsilon) ? (PlayerCharacterController.Health / maximal_health) : 0.0f, true, PlayerCharacterController.Fullness <= float.Epsilon);
                }
                if (armorHUDRadialProgress.RadialProgress)
                {
                    float maximal_armor = PlayerCharacterController.MaximalArmor;
                    UpdateRadialProgress(armorHUDRadialProgress, (maximal_armor > float.Epsilon) ? ((PlayerCharacterController.Armor * 0.5f) / maximal_armor) : 0.0f, true, false);
                }
            }
        }
    }
}

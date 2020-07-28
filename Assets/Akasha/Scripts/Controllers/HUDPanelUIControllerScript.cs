using Akasha.Objects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// HUD panel UI controller script class
    /// </summary>
    public class HUDPanelUIControllerScript : MonoBehaviour, IHUDPanelUIController
    {
        /// <summary>
        /// Health speed
        /// </summary>
        [SerializeField]
        [Range(0.0f, 10000.0f)]
        private float healthSpeed = 50.0f;

        /// <summary>
        /// No weapon sprite
        /// </summary>
        [SerializeField]
        private Sprite noWeaponSprite;

        /// <summary>
        /// Health image
        /// </summary>
        [SerializeField]
        private Image healthImage;

        /// <summary>
        /// Weapon name text
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI weaponNameText = default;

        /// <summary>
        /// Weapon ammo text
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI weaponAmmoText = default;

        /// <summary>
        /// Weapon image
        /// </summary>
        [SerializeField]
        private Image weaponImage = default;

        /// <summary>
        /// Last health
        /// </summary>
        private float lastHealth;

        /// <summary>
        /// Last weapon
        /// </summary>
        private WeaponObjectScript lastWeapon;

        /// <summary>
        /// Last shots fired
        /// </summary>
        private uint lastShotsFired;

        /// <summary>
        /// Health speed
        /// </summary>
        public float HealthSpeed
        {
            get => Mathf.Max(healthSpeed, 0.0f);
            set => healthSpeed = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// No weapon sprite
        /// </summary>
        public Sprite NoWeaponSprite
        {
            get => noWeaponSprite;
            set => noWeaponSprite = value;
        }

        /// <summary>
        /// Health image
        /// </summary>
        public Image HealthImage
        {
            get => healthImage;
            set => healthImage = value;
        }

        /// <summary>
        /// Weapon name text
        /// </summary>
        public TextMeshProUGUI WeaponNameText
        {
            get => weaponNameText;
            set => weaponNameText = value;
        }

        /// <summary>
        /// Weapon ammo text
        /// </summary>
        public TextMeshProUGUI WeaponAmmoText
        {
            get => weaponAmmoText;
            set => weaponAmmoText = value;
        }

        /// <summary>
        /// Weapon image
        /// </summary>
        public Image WeaponImage
        {
            get => weaponImage;
            set => weaponImage = value;
        }

        /// <summary>
        /// Player character controller
        /// </summary>
        public OldCharacterControllerScript PlayerCharacterController { get; private set; }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            PlayerControllerScript player_controller = FindObjectOfType<PlayerControllerScript>();
            if (player_controller != null)
            {
                PlayerCharacterController = player_controller.GetComponent<OldCharacterControllerScript>();
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (PlayerCharacterController != null)
            {
                if (lastHealth != PlayerCharacterController.Health)
                {
                    if (lastHealth < PlayerCharacterController.Health)
                    {
                        lastHealth = Mathf.Clamp(lastHealth + (HealthSpeed * Time.deltaTime), lastHealth, PlayerCharacterController.Health);
                    }
                    else
                    {
                        lastHealth = Mathf.Clamp(lastHealth - (HealthSpeed * Time.deltaTime), PlayerCharacterController.Health, lastHealth);
                    }
                    if (healthImage != null)
                    {
                        healthImage.fillAmount = Mathf.Clamp(lastHealth / 100.0f, 0.0f, 1.0f);
                    }
                }
                if ((lastWeapon != PlayerCharacterController.Weapon) || (lastShotsFired != PlayerCharacterController.ShotsFired))
                {
                    lastWeapon = PlayerCharacterController.Weapon;
                    lastShotsFired = PlayerCharacterController.ShotsFired;
                    uint ammo_capacity = ((lastWeapon is IReloadableWeaponObject last_reloadable_weapon) ? last_reloadable_weapon.AmmoCapacity : 0U);
                    if (weaponNameText != null)
                    {
                        weaponNameText.text = (lastWeapon ? lastWeapon.ItemName : "-");
                    }
                    if (weaponAmmoText != null)
                    {
                        weaponAmmoText.text = Mathf.Max((int)ammo_capacity - (int)lastShotsFired, 0) + " / " + ammo_capacity;
                    }
                    if (weaponImage != null)
                    {
                        weaponImage.sprite = (lastWeapon ? lastWeapon.IconSprite : noWeaponSprite);
                    }
                }
            }
        }
    }
}

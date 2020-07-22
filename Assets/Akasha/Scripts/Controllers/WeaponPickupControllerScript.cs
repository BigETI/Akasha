using Akasha.Objects;
using UnityEngine;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Weapon pickup controller script class
    /// </summary>
    public class WeaponPickupControllerScript : MonoBehaviour, IWeaponPickupController
    {
        /// <summary>
        /// Weapon
        /// </summary>
        [SerializeField]
        private WeaponObjectScript weapon;

        /// <summary>
        /// Weapon
        /// </summary>
        public WeaponObjectScript Weapon
        {
            get => weapon;
            set => weapon = value;
        }

        /// <summary>
        /// On trigger enter
        /// </summary>
        /// <param name="collider">Collider</param>
        private void OnTriggerEnter(Collider collider)
        {
            if (weapon != null)
            {
                OldCharacterControllerScript character_controller = null;
                GameObject game_object = collider.gameObject;
                while ((character_controller == null) && (game_object != null))
                {
                    character_controller = game_object.GetComponent<OldCharacterControllerScript>();
                    if (game_object.transform.parent != null)
                    {
                        game_object = game_object.transform.parent.gameObject;
                    }
                }
                if (character_controller != null)
                {
                    character_controller.Weapon = weapon;
                    Destroy(gameObject);
                }
            }
        }
    }
}

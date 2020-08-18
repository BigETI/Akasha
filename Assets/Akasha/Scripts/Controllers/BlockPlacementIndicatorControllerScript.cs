using Akasha.Managers;
using UnityEngine;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Block placement indicator controller script class
    /// </summary>
    public class BlockPlacementIndicatorControllerScript : MonoBehaviour, IBlockPlacementIndicatorController
    {
        /// <summary>
        /// Origin game object
        /// </summary>
        [SerializeField]
        private GameObject originGameObject = default;

        /// <summary>
        /// Origin game object
        /// </summary>
        public GameObject OriginGameObject
        {
            get => originGameObject;
            set => originGameObject = value;
        }

        /// <summary>
        /// Player character controller
        /// </summary>
        public ICharacterController PlayerCharacterController { get; private set; }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            PlayerControllerScript player_controller = FindObjectOfType<PlayerControllerScript>();
            if (player_controller)
            {
                PlayerCharacterController = player_controller.GetComponent<CharacterControllerScript>();
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (originGameObject != null)
            {
                WorldManagerScript world_manager = WorldManagerScript.Instance;
                if (world_manager && (PlayerCharacterController != null))
                {
                    IInventoryItemData inventory_item = PlayerCharacterController.SelectedInventoryItem;
                    if ((inventory_item != null) && (inventory_item.Quantity > 0U) && (inventory_item.Item is IBlockObject))
                    {
                        ITargetedBlock targeted_block = PlayerCharacterController.GetTargetedBlock(1.0f);
                        bool show_block_indicator = ((targeted_block != null) && targeted_block.IsNothing);
                        originGameObject.SetActive(show_block_indicator);
                        if (show_block_indicator)
                        {
                            originGameObject.transform.position = world_manager.GetWorldPositionFromBlockID(targeted_block.ID);
                        }
                    }
                    else
                    {
                        originGameObject.SetActive(false);
                    }
                }
                else
                {
                    originGameObject.SetActive(false);
                }
            }
        }
    }
}

using Akasha.Data;
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
        /// Jump height
        /// </summary>
        float JumpHeight { get; set; }

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
        /// Selected inventory item slot index
        /// </summary>
        int SelectedInventoryItemSlotIndex { get; set; }

        /// <summary>
        /// Selected inventory item
        /// </summary>
        IInventoryItemData SelectedInventoryItem { get; }

        /// <summary>
        /// Place block
        /// </summary>
        void PlaceBlock();

        /// <summary>
        /// Destroy block
        /// </summary>
        void DestroyBlock();

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
    }
}

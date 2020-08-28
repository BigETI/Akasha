using Akasha.Data;
using System;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// World entity data interface
    /// </summary>
    public interface IWorldEntityData : IValidable
    {
        /// <summary>
        /// GUID
        /// </summary>
        Guid GUID { get; set; }

        /// <summary>
        /// Health
        /// </summary>
        float Health { get; set; }

        /// <summary>
        /// Armor
        /// </summary>
        float Armor { get; set; }

        /// <summary>
        /// Block ID
        /// </summary>
        BlockID BlockID { get; set; }

        /// <summary>
        /// Position offset
        /// </summary>
        Vector3 PositionOffset { get; set; }

        /// <summary>
        /// Rotation
        /// </summary>
        Vector2 Rotation { get; set; }

        /// <summary>
        /// Inventory
        /// </summary>
        InventoryData Inventory { get; set; }
    }
}

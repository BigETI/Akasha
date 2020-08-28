using Akasha.Objects;
using System;
using UnityEngine;

/// <summary>
/// Akasha data  namespace
/// </summary>
namespace Akasha.Data
{
    /// <summary>
    /// World entity data class
    /// </summary>
    [Serializable]
    public class WorldEntityData : IWorldEntityData
    {
        /// <summary>
        /// GUID
        /// </summary>
        [SerializeField]
        private string guid;

        /// <summary>
        /// Entity
        /// </summary>
        [SerializeField]
        private string entity;

        /// <summary>
        /// Health
        /// </summary>
        [SerializeField]
        private float health;

        /// <summary>
        /// Armor
        /// </summary>
        [SerializeField]
        private float armor;

        /// <summary>
        /// Block ID
        /// </summary>
        [SerializeField]
        private BlockID blockID;

        /// <summary>
        /// Position offset
        /// </summary>
        [SerializeField]
        private Vector3 positionOffset;

        /// <summary>
        /// Rotation
        /// </summary>
        [SerializeField]
        private Vector2 rotation;

        /// <summary>
        /// Inventory
        /// </summary>
        [SerializeField]
        private InventoryData inventory;

        /// <summary>
        /// Entity object
        /// </summary>
        private IEntityObject entityObject;

        /// <summary>
        /// GUID
        /// </summary>
        public Guid GUID
        {
            get
            {
                Guid ret;
                if ((guid == null) || !(Guid.TryParse(guid, out ret)))
                {
                    ret = Guid.NewGuid();
                }
                return ret;
            }
            set => guid = value.ToString();
        }

        /// <summary>
        /// Entity type
        /// </summary>
        public IEntityObject Entity
        {
            get
            {
                if ((entityObject == null) && (entity != null))
                {
                    entityObject = Resources.Load<EntityObjectScript>(entity);
                }
                return entityObject;
            }
            set
            {
                entityObject = value ?? throw new ArgumentNullException(nameof(value)); ;
                entity = entityObject.Key;
            }
        }

        /// <summary>
        /// Health
        /// </summary>
        public float Health
        {
            get => Mathf.Max(health, 0.0f);
            set => health = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Armor
        /// </summary>
        public float Armor
        {
            get => Mathf.Max(armor, 0.0f);
            set => armor = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Block ID
        /// </summary>
        public BlockID BlockID
        {
            get => blockID;
            set => blockID = value;
        }

        /// <summary>
        /// Position offset
        /// </summary>
        public Vector3 PositionOffset
        {
            get => positionOffset;
            set => positionOffset = value;
        }

        /// <summary>
        /// Rotation
        /// </summary>
        public Vector2 Rotation
        {
            get => rotation;
            set => rotation = value;
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
        /// Is valid
        /// </summary>
        public bool IsValid => (Entity != null);

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
        public WorldEntityData(Guid guid, IEntityObject entity, float health, float armor, BlockID blockID, Vector3 positionOffset, Vector2 rotation, InventoryData inventory)
        {
            GUID = guid;
            Entity = entity;
            Health = health;
            Armor = armor;
            this.blockID = blockID;
            this.positionOffset = positionOffset;
            this.rotation = rotation;
            Inventory = inventory;
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>"true" if equivalent, otherwise "false"</returns>
        public override bool Equals(object obj) => ((obj is WorldEntityData entity) && (GUID == entity.GUID));

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode() => ToString().GetHashCode();

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString() => GUID.ToString();

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>"true" if equivalent, otherwise "false"</returns>
        public static bool operator ==(WorldEntityData left, WorldEntityData right) => ((ReferenceEquals(left, null) ? string.Empty : left.GUID.ToString()) == (ReferenceEquals(right, null) ? string.Empty : right.GUID.ToString()));

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>"true" if not equivalent, otherwise "false"</returns>
        public static bool operator !=(WorldEntityData left, WorldEntityData right) => ((ReferenceEquals(left, null) ? string.Empty : left.GUID.ToString()) != (ReferenceEquals(right, null) ? string.Empty : right.GUID.ToString()));
    }
}

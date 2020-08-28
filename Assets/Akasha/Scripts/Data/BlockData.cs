using Akasha.Objects;
using System;
using UnityEngine;

/// <summary>
/// Akasha data namespace
/// </summary>
namespace Akasha.Data
{
    /// <summary>
    /// Block data structure
    /// </summary>
    [Serializable]
    public struct BlockData : IBlockData
    {
        /// <summary>
        /// Block
        /// </summary>
        [SerializeField]
        private BlockObjectScript block;

        /// <summary>
        /// Health
        /// </summary>
        [SerializeField]
        private ushort health;

        /// <summary>
        /// Block
        /// </summary>
        public BlockObjectScript Block
        {
            get => block;
            set => block = value;
        }

        /// <summary>
        /// Health
        /// </summary>
        public ushort Health
        {
            get => health;
            set => health = value;
        }

        /// <summary>
        /// Is a block
        /// </summary>
        public bool IsABlock => (block && (health > 0U));

        /// <summary>
        /// Is nothing
        /// </summary>
        public bool IsNothing => !IsABlock;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="block">Block</param>
        /// <param name="health">Health</param>
        public BlockData(BlockObjectScript block, ushort health)
        {
            this.block = block;
            this.health = health;
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>"true" if equivalent, otherwise "false"</returns>
        public override bool Equals(object obj) => ((obj is BlockData block) && (this == block));

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode() => ToString().GetHashCode();

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString() => (IsABlock ? $"({ block }, { health })" : string.Empty);

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>"true" if equivalent, otherwise "false"</returns>
        public static bool operator ==(BlockData left, BlockData right) => ((left.IsABlock) ? (right.IsABlock && ((left.block == right.block) && (left.health == right.health))) : right.IsNothing);

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>"true" if not equivalent, otherwise "false"</returns>
        public static bool operator !=(BlockData left, BlockData right) => ((left.IsABlock) ? (right.IsNothing || ((left.block != right.block) || (left.health != right.health))) : right.IsABlock);
    }
}

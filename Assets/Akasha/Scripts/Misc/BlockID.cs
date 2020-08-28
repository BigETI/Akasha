using System;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Block ID
    /// </summary>
    [Serializable]
    public struct BlockID : IComparable<BlockID>
    {
        /// <summary>
        /// X
        /// </summary>
        [SerializeField]
        private long x;

        /// <summary>
        /// Y
        /// </summary>
        [SerializeField]
        private long y;

        /// <summary>
        /// Z
        /// </summary>
        [SerializeField]
        private long z;

        /// <summary>
        /// Zero
        /// </summary>
        public static BlockID Zero => new BlockID(0L, 0L, 0L);

        /// <summary>
        /// One
        /// </summary>
        public static BlockID One => new BlockID(1L, 1L, 1L);

        /// <summary>
        /// Up
        /// </summary>
        public static BlockID Up => new BlockID(0L, 1L, 0L);

        /// <summary>
        /// Down
        /// </summary>
        public static BlockID Down => new BlockID(0L, -1L, 0L);

        /// <summary>
        /// Left
        /// </summary>
        public static BlockID Left => new BlockID(-1L, 0L, 0L);

        /// <summary>
        /// Right
        /// </summary>
        public static BlockID Right => new BlockID(1L, 0L, 0L);

        /// <summary>
        /// Forward
        /// </summary>
        public static BlockID Forward => new BlockID(0L, 0L, 1L);

        /// <summary>
        /// Back
        /// </summary>
        public static BlockID Back => new BlockID(0L, 0L, -1L);

        /// <summary>
        /// X
        /// </summary>
        public long X
        {
            get => x;
            set => x = value;
        }

        /// <summary>
        /// Y
        /// </summary>
        public long Y
        {
            get => y;
            set => y = value;
        }

        /// <summary>
        /// Z
        /// </summary>
        public long Z
        {
            get => z;
            set => z = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="z">Z</param>
        public BlockID(long x, long y, long z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Plus operator
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>Block ID</returns>
        public static BlockID operator +(BlockID left, BlockID right) => new BlockID(left.x + right.x, left.y + right.y, left.z + right.z);

        /// <summary>
        /// Minus operator
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>Block ID</returns>
        public static BlockID operator -(BlockID left, BlockID right) => new BlockID(left.x - right.x, left.y - right.y, left.z - right.z);

        /// <summary>
        /// Multiplication operator
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>Block ID</returns>
        public static BlockID operator *(BlockID left, BlockID right) => new BlockID(left.x * right.x, left.y * right.y, left.z * right.z);

        /// <summary>
        /// Multiplication operator
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>Block ID</returns>
        public static BlockID operator *(BlockID left, long right) => new BlockID(left.x * right, left.y * right, left.z * right);

        /// <summary>
        /// Division operator
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>Block ID</returns>
        public static BlockID operator /(BlockID left, BlockID right) => new BlockID(left.x / right.x, left.y / right.y, left.z / right.z);

        /// <summary>
        /// Division operator
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>Block ID</returns>
        public static BlockID operator /(BlockID left, long right) => new BlockID(left.x / right, left.y / right, left.z / right);

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>"true" if equal, otherwise "false"</returns>
        public static bool operator ==(BlockID left, BlockID right) => ((left.x == right.x) && (left.y == right.y) && (left.z == right.z));

        /// <summary>
        /// Not equals operator
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>"true" if not equal, otherwise "false"</returns>
        public static bool operator !=(BlockID left, BlockID right) => ((left.x != right.x) || (left.y != right.y) || (left.z != right.z));

        /// <summary>
        /// Explicit cast operator
        /// </summary>
        /// <param name="vector">Vector</param>
        public static explicit operator BlockID(Vector3Int vector) => new BlockID(vector.x, vector.y, vector.z);

        /// <summary>
        /// Explicit cast operator
        /// </summary>
        /// <param name="blockID">Block ID</param>
        public static explicit operator Vector3Int(BlockID blockID) => new Vector3Int((int)(blockID.x), (int)(blockID.y), (int)(blockID.z));

        /// <summary>
        /// Compare to
        /// </summary>
        /// <param name="other">Other</param>
        /// <returns>Comparison result</returns>
        public int CompareTo(BlockID other)
        {
            int ret = X.CompareTo(other.x);
            if (ret == 0)
            {
                ret = Y.CompareTo(other.y);
                if (ret == 0)
                {
                    ret = Z.CompareTo(other.z);
                }
            }
            return ret;
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString() => $"({ x }, { y }, { z })";

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode() => ToString().GetHashCode();

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>"true" if equal, otherwise "false"</returns>
        public override bool Equals(object obj) => ((obj is BlockID block_id) && (this == block_id));
    }
}

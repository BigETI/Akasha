using System;
using UnityEngine;

/// <summary>
/// Akasha
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Chunk ID structure
    /// </summary>
    [Serializable]
    public struct ChunkID : IComparable<ChunkID>
    {
        /// <summary>
        /// X
        /// </summary>
        [SerializeField]
        private int x;

        /// <summary>
        /// Y
        /// </summary>
        [SerializeField]
        private int y;

        /// <summary>
        /// Z
        /// </summary>
        [SerializeField]
        private int z;

        /// <summary>
        /// Zero
        /// </summary>
        public static ChunkID Zero => new ChunkID(0, 0, 0);

        /// <summary>
        /// One
        /// </summary>
        public static ChunkID One => new ChunkID(1, 1, 1);

        /// <summary>
        /// Up
        /// </summary>
        public static ChunkID Up => new ChunkID(0, 1, 0);

        /// <summary>
        /// Down
        /// </summary>
        public static ChunkID Down => new ChunkID(0, -1, 0);

        /// <summary>
        /// Left
        /// </summary>
        public static ChunkID Left => new ChunkID(-1, 0, 0);

        /// <summary>
        /// Right
        /// </summary>
        public static ChunkID Right => new ChunkID(1, 0, 0);

        /// <summary>
        /// Forward
        /// </summary>
        public static ChunkID Forward => new ChunkID(0, 0, 1);

        /// <summary>
        /// Back
        /// </summary>
        public static ChunkID Back => new ChunkID(0, 0, -1);

        /// <summary>
        /// X
        /// </summary>
        public int X
        {
            get => x;
            set => x = value;
        }

        /// <summary>
        /// Y
        /// </summary>
        public int Y
        {
            get => y;
            set => y = value;
        }

        /// <summary>
        /// Z
        /// </summary>
        public int Z
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
        public ChunkID(int x, int y, int z)
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
        public static ChunkID operator +(ChunkID left, ChunkID right) => new ChunkID(left.x + right.x, left.y + right.y, left.z + right.z);

        /// <summary>
        /// Minus operator
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>Block ID</returns>
        public static ChunkID operator -(ChunkID left, ChunkID right) => new ChunkID(left.x - right.x, left.y - right.y, left.z - right.z);

        /// <summary>
        /// Multiplication operator
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>Block ID</returns>
        public static ChunkID operator *(ChunkID left, ChunkID right) => new ChunkID(left.x * right.x, left.y * right.y, left.z * right.z);

        /// <summary>
        /// Multiplication operator
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>Block ID</returns>
        public static ChunkID operator *(ChunkID left, int right) => new ChunkID(left.x * right, left.y * right, left.z * right);

        /// <summary>
        /// Division operator
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>Block ID</returns>
        public static ChunkID operator /(ChunkID left, ChunkID right) => new ChunkID(left.x / right.x, left.y / right.y, left.z / right.z);

        /// <summary>
        /// Division operator
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>Block ID</returns>
        public static ChunkID operator /(ChunkID left, int right) => new ChunkID(left.x / right, left.y / right, left.z / right);

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>"true" if equals, otherwise "false"</returns>
        public static bool operator ==(ChunkID left, ChunkID right) => ((left.x == right.x) && (left.y == right.y) && (left.z == right.z));

        /// <summary>
        /// Not equals operator
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>"true" if not equal, otherwise "false"</returns>
        public static bool operator !=(ChunkID left, ChunkID right) => ((left.x != right.x) || (left.y != right.y) || (left.z != right.z));

        /// <summary>
        /// Explicit cast operator
        /// </summary>
        /// <param name="vector">Vector</param>
        public static explicit operator ChunkID(Vector3Int vector) => new ChunkID(vector.x, vector.y, vector.z);

        /// <summary>
        /// Explicit cast operator
        /// </summary>
        /// <param name="chunkID">Chunk ID</param>
        public static explicit operator Vector3Int(ChunkID chunkID) => new Vector3Int(chunkID.x, chunkID.y, chunkID.z);

        /// <summary>
        /// Compare to
        /// </summary>
        /// <param name="other">Other</param>
        /// <returns>Comparison result</returns>
        public int CompareTo(ChunkID other)
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
        public override string ToString() => ("(" + x + ", " + y + ", " + z + ")");

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
        public override bool Equals(object obj) => ((obj is ChunkID chunk_id) ? (this == chunk_id) : false);
    }
}

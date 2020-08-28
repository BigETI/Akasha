using System;
using System.IO;

/// <summary>
/// AKasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Cyclic redundancy checksum class
    /// </summary>
    public static class CRC32
    {
        /// <summary>
        /// Polynomial
        /// </summary>
        private static readonly uint polynomial = 0xEDB88320;

        /// <summary>
        /// Checksum lookup table
        /// </summary>
        private static readonly uint[] checksumLookupTable = new uint[0x100];

        /// <summary>
        /// Static constructor
        /// </summary>
        static CRC32()
        {
            int bit;
            for (uint index = 0U; index != checksumLookupTable.Length; index++)
            {
                ref uint checksum_lookup_table_element = ref checksumLookupTable[index];
                checksum_lookup_table_element = index;
                for (bit = 0; bit < 8; bit++)
                {
                    checksum_lookup_table_element = (((checksum_lookup_table_element & 1) != 0) ? (polynomial ^ (checksum_lookup_table_element >> 1)) : (checksum_lookup_table_element >> 1));
                }
            }
        }

        /// <summary>
        /// Compute hash
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns>Computed hash</returns>
        public static byte[] ComputeHash(Stream stream)
        {
            byte[] result = null;
            if (stream.CanRead)
            {
                uint computed_hash = uint.MaxValue;
                int current;
                while ((current = stream.ReadByte()) >= 0)
                {
                    computed_hash = checksumLookupTable[(computed_hash & 0xFF) ^ (byte)current] ^ (computed_hash >> 8);
                }
                result = BitConverter.GetBytes(~computed_hash);
                Array.Reverse(result);
            }
            return (result ?? BitConverter.GetBytes(0U));
        }

        /// <summary>
        /// Compute hash
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns>Computed hash</returns>
        public static byte[] ComputeHash(byte[] data)
        {
            byte[] ret;
            using (MemoryStream stream = new MemoryStream(data))
            {
                ret = ComputeHash(stream);
            }
            return ret;
        }
    }
}

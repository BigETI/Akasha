/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Math utilities
    /// </summary>
    static class MathUtils
    {
        /// <summary>
        /// Absolute
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Absolute value</returns>
        public static long Absolute(long value) => ((value < 0L) ? -value : value);
    }
}

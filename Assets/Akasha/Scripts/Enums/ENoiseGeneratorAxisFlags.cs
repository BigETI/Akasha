using System;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Noise generator axis flags enumerator
    /// </summary>
    [Flags]
    public enum ENoiseGeneratorAxisFlags
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0x0,

        /// <summary>
        /// X
        /// </summary>
        X = 0x1,

        /// <summary>
        /// Y
        /// </summary>
        Y = 0x2,

        /// <summary>
        /// Z
        /// </summary>
        Z = 0x4,

        /// <summary>
        /// X and Y
        /// </summary>
        XY = X | Y,

        /// <summary>
        /// X and Z
        /// </summary>
        XZ = X | Z,

        /// <summary>
        /// Y and Z
        /// </summary>
        YZ = Y | Z,

        /// <summary>
        /// X, Y and Z
        /// </summary>
        XYZ = X | Y | Z
    }
}

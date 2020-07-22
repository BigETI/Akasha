using System;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Direction flags
    /// </summary>
    [Flags]
    public enum EDirectionFlags
    {
        /// <summary>
        /// Nothing
        /// </summary>
        Nothing = 0x0,

        /// <summary>
        /// Top
        /// </summary>
        Top = 0x1,

        /// <summary>
        /// Bottom
        /// </summary>
        Bottom = 0x2,

        /// <summary>
        /// Left
        /// </summary>
        Left = 0x4,

        /// <summary>
        /// Right
        /// </summary>
        Right = 0x8,

        /// <summary>
        /// Front
        /// </summary>
        Front = 0x10,

        /// <summary>
        /// Behind
        /// </summary>
        Behind = 0x20
    }
}

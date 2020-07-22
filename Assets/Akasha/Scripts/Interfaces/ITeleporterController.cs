using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Tleporter controller interface
    /// </summary>
    public interface ITeleporterController : IBehaviour
    {
        /// <summary>
        /// Target transform
        /// </summary>
        Transform TargetTransform { get; set; }
    }
}

using UnityEngine.UI;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Radial progress UI controller interface
    /// </summary>
    public interface IRadialProgressUIController : IBehaviour
    {
        /// <summary>
        /// Value
        /// </summary>
        float Value { get; set; }

        /// <summary>
        /// Border rotation offset
        /// </summary>
        float BorderRotationOffset { get; set; }

        /// <summary>
        /// Border radial progress image
        /// </summary>
        Image BorderRadialProgressImage { get; set; }

        /// <summary>
        /// Inner radial progress image
        /// </summary>
        Image InnerRadialProgressImage { get; set; }
    }
}

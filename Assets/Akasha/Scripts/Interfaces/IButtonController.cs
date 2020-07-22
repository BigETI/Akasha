/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Button controller interface
    /// </summary>
    public interface IButtonController : IInteractableController
    {
        /// <summary>
        /// Is on
        /// </summary>
        bool IsOn { get; set; }

        /// <summary>
        /// Is a switch
        /// </summary>
        bool IsASwitch { get; set; }
    }
}

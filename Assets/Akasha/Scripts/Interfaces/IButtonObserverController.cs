using Akasha.Controllers;
using System.Collections.Generic;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Button observer controller interface
    /// </summary>
    public interface IButtonObserverController : IBehaviour
    {
        /// <summary>
        /// Button controllers
        /// </summary>
        IReadOnlyList<ButtonControllerScript> ButtonControllers { get; }
    }
}

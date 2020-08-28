using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Loading or saving world indicator UI controller interface
    /// </summary>
    public interface ILoadingOrSavingWorldIndicatorUIController : IBehaviour
    {
        /// <summary>
        /// Loading or saving world indicator panel game object
        /// </summary>
        GameObject LoadingOrSavingWorldIndicatorPanelGameObject { get; set; }
    }
}

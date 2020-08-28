using Akasha.Managers;
using UnityEngine;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Loading or saving world indicator UI controller script class
    /// </summary>
    public class LoadingOrSavingWorldIndicatorUIControllerScript : MonoBehaviour, ILoadingOrSavingWorldIndicatorUIController
    {
        /// <summary>
        /// Loading or saving world indicator panel game object
        /// </summary>
        [SerializeField]
        private GameObject loadingOrSavingWorldIndicatorPanelGameObject;

        /// <summary>
        /// Loading or saving world indicator panel game object
        /// </summary>
        public GameObject LoadingOrSavingWorldIndicatorPanelGameObject
        {
            get => loadingOrSavingWorldIndicatorPanelGameObject;
            set => loadingOrSavingWorldIndicatorPanelGameObject = value;
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (loadingOrSavingWorldIndicatorPanelGameObject)
            {
                WorldManagerScript world_manager = WorldManagerScript.Instance;
                if (world_manager)
                {
                    loadingOrSavingWorldIndicatorPanelGameObject.SetActive(world_manager.IsLoading || world_manager.IsSaving);
                }
            }
        }
    }
}

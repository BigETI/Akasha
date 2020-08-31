using UnityEngine;
using UnitySceneLoaderManager;
using UnityTiming.Data;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Intro controller script class
    /// </summary>
    [RequireComponent(typeof(InputControllerScript))]
    public class IntroControllerScript : MonoBehaviour, IIntroController
    {
        /// <summary>
        /// Timing
        /// </summary>
        [SerializeField]
        private TimingData timing = new TimingData(1.0f);

        /// <summary>
        /// Show main menu
        /// </summary>
        public void ShowMainMenu() => SceneLoaderManager.LoadScene("MainMenuScene");

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            InputControllerScript input_controller = GetComponent<InputControllerScript>();
            if (input_controller)
            {
                input_controller.OnAnyKeyPressed += ShowMainMenu;
            }
            WorldIO.CreateClearCacheTask();
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (timing.ProceedUpdate(false, false) > 0)
            {
                ShowMainMenu();
            }
        }
    }
}

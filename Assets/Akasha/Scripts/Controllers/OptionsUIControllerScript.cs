using UnityAudioManager;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Options UI controller script class
    /// </summary>
    public class OptionsUIControllerScript : MonoBehaviour, IOptionsUIController
    {
        /// <summary>
        /// Master volume slider
        /// </summary>
        [SerializeField]
        private Slider masterVolumeSlider = default;

        /// <summary>
        /// Music volume slider
        /// </summary>
        [SerializeField]
        private Slider musicVolumeSlider = default;

        /// <summary>
        /// Sound effects volume slider
        /// </summary>
        [SerializeField]
        private Slider soundEffectsVolumeSlider = default;

        /// <summary>
        /// Enable cheats toggle
        /// </summary>
        [SerializeField]
        private Toggle enableCheatsToggle = default;

        /// <summary>
        /// Master volume slider
        /// </summary>
        public Slider MasterVolumeSlider
        {
            get => masterVolumeSlider;
            set => masterVolumeSlider = value;
        }

        /// <summary>
        /// Music volume slider
        /// </summary>
        public Slider MusicVolumeSlider
        {
            get => musicVolumeSlider;
            set => musicVolumeSlider = value;
        }

        /// <summary>
        /// Sound effects volume slider
        /// </summary>
        public Slider SoundEffectsVolumeSlider
        {
            get => soundEffectsVolumeSlider;
            set => soundEffectsVolumeSlider = value;
        }

        /// <summary>
        /// Enable cheats toggle
        /// </summary>
        public Toggle EnableCheatsToggle
        {
            get => enableCheatsToggle;
            set => enableCheatsToggle = value;
        }

        /// <summary>
        /// On disable
        /// </summary>
        private void OnDisable() => GameManager.Save();

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            ISaveGameData save_game = GameManager.SaveGameData;
            if (save_game != null)
            {
                if (masterVolumeSlider)
                {
                    masterVolumeSlider.SetValueWithoutNotify(save_game.MasterVolume);
                }
                if (musicVolumeSlider)
                {
                    musicVolumeSlider.SetValueWithoutNotify(save_game.MusicVolume);
                }
                if (soundEffectsVolumeSlider)
                {
                    soundEffectsVolumeSlider.SetValueWithoutNotify(save_game.SoundEffectsVolume);
                }
                if (enableCheatsToggle)
                {
                    enableCheatsToggle.SetIsOnWithoutNotify(save_game.AreCheatsEnabled);
                }
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            ISaveGameData save_game = GameManager.SaveGameData;
            if (save_game != null)
            {
                if (masterVolumeSlider)
                {
                    save_game.MasterVolume = masterVolumeSlider.value;
                    AudioManager.MusicVolume = Mathf.Clamp(save_game.MasterVolume * 0.01f, 0.0f, 1.0f);
                }
                if (musicVolumeSlider)
                {
                    save_game.MusicVolume = musicVolumeSlider.value;
                    AudioManager.MusicVolume = Mathf.Clamp(save_game.MasterVolume * save_game.MusicVolume * 0.0001f, 0.0f, 1.0f);
                }
                if (soundEffectsVolumeSlider)
                {
                    save_game.SoundEffectsVolume = soundEffectsVolumeSlider.value;
                    AudioManager.SoundEffectsVolume = Mathf.Clamp(save_game.MasterVolume * save_game.SoundEffectsVolume * 0.0001f, 0.0f, 1.0f);
                }
                if (enableCheatsToggle)
                {
                    save_game.AreCheatsEnabled = enableCheatsToggle.isOn;
                }
            }
        }
    }
}

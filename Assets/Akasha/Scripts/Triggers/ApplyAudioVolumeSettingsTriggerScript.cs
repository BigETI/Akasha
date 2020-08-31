using UnityAudioManager;
using UnityAudioManager.Managers;
using UnityEngine;

/// <summary>
/// Akasha triggers namespace
/// </summary>
namespace Akasha.Triggers
{
    /// <summary>
    /// Apply audio volume settings trigger script class
    /// </summary>
    public class ApplyAudioVolumeSettingsTriggerScript : MonoBehaviour, IApplyAudioVolumeSettingsTrigger
    {
        /// <summary>
        /// Default music volume
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float defaultMusicVolume = 0.25f;

        /// <summary>
        /// Default sound effects volume
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float defaultSoundEffectsVolume = 0.5f;

        /// <summary>
        /// Default music volume
        /// </summary>
        public float DefaultMusicVolume
        {
            get => Mathf.Clamp(defaultMusicVolume, 0.0f, 1.0f);
            set => defaultMusicVolume = Mathf.Clamp(value, 0.0f, 1.0f);
        }

        /// <summary>
        /// Default sound effects volume
        /// </summary>
        public float DefaultSoundEffectsVolume
        {
            get => Mathf.Clamp(defaultSoundEffectsVolume, 0.0f, 1.0f);
            set => defaultSoundEffectsVolume = Mathf.Clamp(value, 0.0f, 1.0f);
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            AudioManagerScript audio_manager = FindObjectOfType<AudioManagerScript>();
            if (audio_manager)
            {
                ISaveGameData save_game = GameManager.SaveGameData;
                audio_manager.MusicVolume = ((save_game == null) ? DefaultMusicVolume : Mathf.Clamp(save_game.MasterVolume * save_game.MusicVolume * 0.0001f, 0.0f, 1.0f));
                audio_manager.SoundEffectsVolume = ((save_game == null) ? DefaultSoundEffectsVolume : Mathf.Clamp(save_game.MasterVolume * save_game.SoundEffectsVolume * 0.0001f, 0.0f, 1.0f));
            }
            Destroy(gameObject);
        }
    }
}

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Apply audio volume settings trigger interface
    /// </summary>
    public interface IApplyAudioVolumeSettingsTrigger : IBehaviour
    {
        /// <summary>
        /// Default music volume
        /// </summary>
        float DefaultMusicVolume { get; set; }

        /// <summary>
        /// Default sound effects volume
        /// </summary>
        float DefaultSoundEffectsVolume { get; set; }
    }
}

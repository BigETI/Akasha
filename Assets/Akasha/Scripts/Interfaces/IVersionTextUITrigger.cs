using UnityTranslator.Objects;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Version text UI trigger interface
    /// </summary>
    public interface IVersionTextUITrigger : IBehaviour
    {
        /// <summary>
        /// Version text string translation
        /// </summary>
        StringTranslationObjectScript VersionTextStringTranslation { get; set; }
    }
}

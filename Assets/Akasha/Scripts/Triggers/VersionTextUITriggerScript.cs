using TMPro;
using UnityEngine;
using UnityTranslator.Objects;

/// <summary>
/// Akasha triggers namespace
/// </summary>
namespace Akasha.Triggers
{
    /// <summary>
    /// Version text UI trigger script class
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class VersionTextUITriggerScript : MonoBehaviour, IVersionTextUITrigger
    {
        /// <summary>
        /// Version text string translation
        /// </summary>
        [SerializeField]
        private StringTranslationObjectScript versionTextStringTranslation;

        /// <summary>
        /// Version text string translation
        /// </summary>
        public StringTranslationObjectScript VersionTextStringTranslation
        {
            get => versionTextStringTranslation;
            set => versionTextStringTranslation = value;
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            if (versionTextStringTranslation && TryGetComponent(out TextMeshProUGUI version_text))
            {
                version_text.text = string.Format((versionTextStringTranslation == null) ? string.Empty : versionTextStringTranslation.ToString(), Application.productName, Application.version, Application.unityVersion);
            }
            Destroy(this);
        }
    }
}

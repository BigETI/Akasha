using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Radial progress UI controller script class
    /// </summary>
    [ExecuteInEditMode]
    public class RadialProgressUIControllerScript : MonoBehaviour, IRadialProgressUIController
    {
        /// <summary>
        /// Value
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float value = 1.0f;

        /// <summary>
        /// Border rotation offset
        /// </summary>
        [SerializeField]
        [Range(0.0f, 360.0f - float.Epsilon)]
        private float borderRotationOffset = 3.0f;

        /// <summary>
        /// Border radial progress image
        /// </summary>
        [SerializeField]
        private Image borderRadialProgressImage;

        /// <summary>
        /// Inner radial progress image
        /// </summary>
        [SerializeField]
        private Image innerRadialProgressImage;

        /// <summary>
        /// Value
        /// </summary>
        public float Value
        {
            get => value;
            set
            {
                float result = Mathf.Clamp(value, 0.0f, 1.0f);
                if (this.value != result)
                {
                    this.value = result;
                    UpdateVisuals();
                }
            }
        }

        /// <summary>
        /// Border rotation offset
        /// </summary>
        public float BorderRotationOffset
        {
            get => Mathf.Clamp(borderRotationOffset, 0.0f, 360.0f - float.Epsilon);
            set => borderRotationOffset = Mathf.Clamp(value, 0.0f, 360.0f - float.Epsilon);
        }

        /// <summary>
        /// Border radial progress image
        /// </summary>
        public Image BorderRadialProgressImage
        {
            get => borderRadialProgressImage;
            set => borderRadialProgressImage = value;
        }

        /// <summary>
        /// Inner radial progress image
        /// </summary>
        public Image InnerRadialProgressImage
        {
            get => innerRadialProgressImage;
            set => innerRadialProgressImage = value;
        }

        /// <summary>
        /// Update visuals
        /// </summary>
        private void UpdateVisuals()
        {
            float value = Value;
            if (innerRadialProgressImage)
            {
                innerRadialProgressImage.fillAmount = value;
            }
            if (borderRadialProgressImage)
            {
                float border_rotation_offset = BorderRotationOffset;
                if (innerRadialProgressImage)
                {
                    borderRadialProgressImage.fillClockwise = innerRadialProgressImage.fillClockwise;
                    borderRadialProgressImage.fillMethod = innerRadialProgressImage.fillMethod;
                    borderRadialProgressImage.fillOrigin = innerRadialProgressImage.fillOrigin;
                }
                borderRadialProgressImage.fillAmount = ((value > float.Epsilon) ? Mathf.Clamp(value + ((border_rotation_offset * 2.0f) / 360.0f), 0.0f, 1.0f) : 0.0f);
                borderRadialProgressImage.rectTransform.localRotation = (innerRadialProgressImage ? innerRadialProgressImage.rectTransform.localRotation : Quaternion.identity) * Quaternion.AngleAxis(borderRadialProgressImage.fillClockwise ? border_rotation_offset : -border_rotation_offset, Vector3.forward);
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update() => UpdateVisuals();
    }
}

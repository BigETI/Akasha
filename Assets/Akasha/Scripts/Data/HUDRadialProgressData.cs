using Akasha.Controllers;
using System;
using UnityEngine;

/// <summary>
/// AKasha data namespace
/// </summary>
namespace Akasha.Data
{
    /// <summary>
    /// HUD radial progress data structure
    /// </summary>
    [Serializable]
    public struct HUDRadialProgressData : IHUDRadialProgressData
    {
        /// <summary>
        /// Radial progress
        /// </summary>
        [SerializeField]
        private RadialProgressUIControllerScript radialProgress;

        /// <summary>
        /// Animate progress
        /// </summary>
        [SerializeField]
        private bool animateProgress;

        /// <summary>
        /// Progress speed
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float progressSpeed;

        /// <summary>
        /// Blink threshold
        /// </summary>
        [SerializeField]
        private float blinkThreshold;

        /// <summary>
        /// Radial progress
        /// </summary>
        public RadialProgressUIControllerScript RadialProgress
        {
            get => radialProgress;
            set => radialProgress = value;
        }

        /// <summary>
        /// Animate progress
        /// </summary>
        public bool AnimateProgress
        {
            get => animateProgress;
            set => animateProgress = value;
        }

        /// <summary>
        /// Progress speed
        /// </summary>
        public float ProgressSpeed
        {
            get => Mathf.Max(progressSpeed, 0.0f);
            set => progressSpeed = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Blink threshold
        /// </summary>
        public float BlinkThreshold
        {
            get => Mathf.Clamp(blinkThreshold, 0.0f, 1.0f);
            set => blinkThreshold = Mathf.Clamp(value, 0.0f, 1.0f);
        }

        /// <summary>
        /// Reset
        /// </summary>
        public void Reset()
        {
            if (radialProgress)
            {
                radialProgress.Value = 0.0f;
            }
        }
    }
}

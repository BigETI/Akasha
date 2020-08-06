using System;
using UnityEngine;

/// <summary>
/// Akasha data namespace
/// </summary>
namespace Akasha.Data
{
    /// <summary>
    /// Weather data class
    /// </summary>
    [Serializable]
    public class WeatherData : IWeatherData
    {
        /// <summary>
        /// Minimal required temperature in Kelvin
        /// </summary>
        [SerializeField]
        [Range(0.0f, 10000.0f)]
        private float minimalRequiredTemperature = 223.15f;

        /// <summary>
        /// Maximal required temperature in Kelvin
        /// </summary>
        [SerializeField]
        [Range(0.0f, 10000.0f)]
        private float maximalRequiredTemperature = 323.15f;

        /// <summary>
        /// Minimal required pressure in atmosphere
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float minimalRequiredPressure;

        /// <summary>
        /// Maximal required pressure in atmosphere
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float maximalRequiredPressure = 2.0f;

        /// <summary>
        /// Minimal wind speed in meters per second
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float minimalRequiredWindSpeed;

        /// <summary>
        /// Maximal wind speed in meters per second
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float maximalRequiredWindSpeed = 100.0f;

        /// <summary>
        /// Minimal required temperature in Kelvin
        /// </summary>
        public float MinimalRequiredTemperature
        {
            get => Mathf.Clamp(minimalRequiredTemperature, 0.0f, maximalRequiredTemperature);
            set => minimalRequiredTemperature = Mathf.Clamp(value, 0.0f, maximalRequiredTemperature);
        }

        /// <summary>
        /// Maximal required temperature in Kelvin
        /// </summary>
        public float MaximalRequiredTemperature
        {
            get => Mathf.Max(MinimalRequiredTemperature, maximalRequiredTemperature);
            set => maximalRequiredTemperature = Mathf.Min(MinimalRequiredTemperature, value);
        }

        /// <summary>
        /// Minimal required pressure in atmosphere
        /// </summary>
        public float MinimalRequiredPressure
        {
            get => Mathf.Clamp(minimalRequiredPressure, 0.0f, maximalRequiredPressure);
            set => minimalRequiredPressure = Mathf.Clamp(value, 0.0f, maximalRequiredPressure);
        }

        /// <summary>
        /// Maximal required pressure in atmosphere
        /// </summary>
        public float MaximalRequiredPressure
        {
            get => Mathf.Max(MinimalRequiredPressure, maximalRequiredPressure);
            set => maximalRequiredPressure = Mathf.Min(MinimalRequiredPressure, value);
        }

        /// <summary>
        /// Minimal required wind speed in meters per second
        /// </summary>
        public float MinimalRequiredWindSpeed
        {
            get => Mathf.Clamp(minimalRequiredWindSpeed, 0.0f, maximalRequiredWindSpeed);
            set => minimalRequiredWindSpeed = Mathf.Clamp(value, 0.0f, maximalRequiredWindSpeed);
        }

        /// <summary>
        /// Maximal required wind speed in meters per second
        /// </summary>
        public float MaximalRequiredWindSpeed
        {
            get => Mathf.Max(MinimalRequiredWindSpeed, maximalRequiredWindSpeed);
            set => maximalRequiredWindSpeed = Mathf.Min(MinimalRequiredWindSpeed, value);
        }
    }
}

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Weather data interface
    /// </summary>
    public interface IWeatherData
    {
        /// <summary>
        /// Minimal required temperature in Kelvin
        /// </summary>
        float MinimalRequiredTemperature { get; set; }

        /// <summary>
        /// Maximal required temperature in Kelvin
        /// </summary>
        float MaximalRequiredTemperature { get; set; }

        /// <summary>
        /// Minimal required pressure in atmosphere
        /// </summary>
        float MinimalRequiredPressure { get; set; }

        /// <summary>
        /// Maximal required pressure in atmosphere
        /// </summary>
        float MaximalRequiredPressure { get; set; }

        /// <summary>
        /// Minimal required wind speed in meters per second
        /// </summary>
        float MinimalRequiredWindSpeed { get; set; }

        /// <summary>
        /// Maximal required wind speed in meters per second
        /// </summary>
        float MaximalRequiredWindSpeed { get; set; }
    }
}

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// World meta data interface
    /// </summary>
    public interface IWorldMetaData
    {
        /// <summary>
        /// World name
        /// </summary>
        string WorldName { get; set; }

        /// <summary>
        /// World description
        /// </summary>
        string WorldDescription { get; set; }

        /// <summary>
        /// World seed
        /// </summary>
        int WorldSeed { get; set; }
    }
}

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Launcher controller interface
    /// </summary>
    public interface ILauncherController : IBehaviour
    {
        /// <summary>
        /// Jump height
        /// </summary>
        float JumpHeight { get; set; }
    }
}

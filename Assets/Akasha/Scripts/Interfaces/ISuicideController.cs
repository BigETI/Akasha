/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Suicide controller interface
    /// </summary>
    public interface ISuicideController : IBehaviour
    {
        /// <summary>
        /// Destroy myself
        /// </summary>
        void DestroyMyself();
    }
}

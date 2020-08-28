/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Scriptable object interface
    /// </summary>
    public interface IScriptableObject
    {
        /// <summary>
        /// Name
        /// </summary>
#pragma warning disable IDE1006 // Naming Styles
        string name { get; }
#pragma warning restore IDE1006 // Naming Styles
    }
}

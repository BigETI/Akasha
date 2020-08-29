#if UNITY_EDITOR
using UnityEditor;
#else
using UnityEngine;
#endif

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Game class
    /// </summary>
    public static class Game
    {
        /// <summary>
        /// Quit game
        /// </summary>
        public static void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}

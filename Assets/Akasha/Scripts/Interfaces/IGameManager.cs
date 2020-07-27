using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    public interface IGameManager
    {
        /// <summary>
        /// Game state
        /// </summary>
        EGameState GameState { get; set; }

        /// <summary>
        /// On game started
        /// </summary>
        event GameStateChangedDelegate OnGameStarted;

        /// <summary>
        /// On datapad shown
        /// </summary>
        event GameStateChangedDelegate OnDatapadShown;

        /// <summary>
        /// On datapad hidden
        /// </summary>
        event GameStateChangedDelegate OnDatapadHidden;

        /// <summary>
        /// On game paused
        /// </summary>
        event GameStateChangedDelegate OnGamePaused;

        /// <summary>
        /// On game resumed
        /// </summary>
        event GameStateChangedDelegate OnGameResumed;
    }
}

using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// AKasha managers namespace
/// </summary>
namespace Akasha.Managers
{
    /// <summary>
    /// Game manager script class
    /// </summary>
    public class GameManagerScript : AManagerScript<GameManagerScript>, IGameManager
    {
        /// <summary>
        /// On game started
        /// </summary>
        [SerializeField]
        private UnityEvent onGameStarted = default;

        /// <summary>
        /// On datapad shown
        /// </summary>
        [SerializeField]
        private UnityEvent onDatapadShown = default;

        /// <summary>
        /// On datapad hidden
        /// </summary>
        [SerializeField]
        private UnityEvent onDatapadHidden = default;

        /// <summary>
        /// On game paused
        /// </summary>
        [SerializeField]
        private UnityEvent onGamePaused = default;

        /// <summary>
        /// On game resumed
        /// </summary>
        [SerializeField]
        private UnityEvent onGameResumed = default;

        /// <summary>
        /// Game state
        /// </summary>
        private EGameState gameState = EGameState.Nothing;

        /// <summary>
        /// Game state
        /// </summary>
        public EGameState GameState
        {
            get => gameState;
            set
            {
                if (gameState != value)
                {
                    switch (gameState)
                    {
                        case EGameState.Nothing:
                            if (value == EGameState.Playing)
                            {
                                gameState = EGameState.Playing;
                                if (onGameStarted != null)
                                {
                                    onGameStarted.Invoke();
                                }
                                OnGameStarted?.Invoke();
                            }
                            break;
                        case EGameState.Playing:
                            switch (value)
                            {
                                case EGameState.ShowingDatapad:
                                    gameState = EGameState.ShowingDatapad;
                                    if (onDatapadShown != null)
                                    {
                                        onDatapadShown.Invoke();
                                    }
                                    OnDatapadShown?.Invoke();
                                    break;
                                case EGameState.Paused:
                                    gameState = EGameState.Paused;
                                    if (onGamePaused != null)
                                    {
                                        onGamePaused.Invoke();
                                    }
                                    OnGamePaused?.Invoke();
                                    break;
                            }
                            break;
                        case EGameState.ShowingDatapad:
                            switch (value)
                            {
                                case EGameState.Playing:
                                    gameState = EGameState.Playing;
                                    if (onDatapadHidden != null)
                                    {
                                        onDatapadHidden.Invoke();
                                    }
                                    OnDatapadHidden?.Invoke();
                                    break;
                                case EGameState.Paused:
                                    gameState = EGameState.Paused;
                                    if (onDatapadHidden != null)
                                    {
                                        onDatapadHidden.Invoke();
                                    }
                                    OnDatapadHidden?.Invoke();
                                    if (onGamePaused != null)
                                    {
                                        onGamePaused.Invoke();
                                    }
                                    OnGamePaused?.Invoke();
                                    break;
                            }
                            break;
                        case EGameState.Paused:
                            switch (value)
                            {
                                case EGameState.Playing:
                                    gameState = EGameState.Playing;
                                    if (onGameResumed != null)
                                    {
                                        onGameResumed.Invoke();
                                    }
                                    OnGameResumed?.Invoke();
                                    break;
                                case EGameState.ShowingDatapad:
                                    gameState = EGameState.ShowingDatapad;
                                    if (onGameResumed != null)
                                    {
                                        onGameResumed.Invoke();
                                    }
                                    OnGameResumed?.Invoke();
                                    if (onDatapadShown != null)
                                    {
                                        onDatapadShown.Invoke();
                                    }
                                    OnDatapadShown?.Invoke();
                                    break;
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// On game started
        /// </summary>
        public event GameStateChangedDelegate OnGameStarted;

        /// <summary>
        /// On datapad shown
        /// </summary>
        public event GameStateChangedDelegate OnDatapadShown;

        /// <summary>
        /// On datapad hidden
        /// </summary>
        public event GameStateChangedDelegate OnDatapadHidden;

        /// <summary>
        /// On game paused
        /// </summary>
        public event GameStateChangedDelegate OnGamePaused;

        /// <summary>
        /// On game resumed
        /// </summary>
        public event GameStateChangedDelegate OnGameResumed;

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            Cursor.visible = (gameState != EGameState.Playing);
            Cursor.lockState = ((gameState == EGameState.Playing) ? CursorLockMode.Locked : CursorLockMode.None);
        }
    }
}

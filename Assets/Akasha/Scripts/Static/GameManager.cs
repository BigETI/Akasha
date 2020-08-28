using Akasha.Data;
using Akasha.Managers;
using System;
using UnityEngine;
using UnitySaveGame;
using UnitySceneLoaderManager;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Game manager class
    /// </summary>
    public static class GameManager
    {
        /// <summary>
        /// World GUID
        /// </summary>
        public static Guid WorldGUID { get; private set; } = Guid.NewGuid();

        /// <summary>
        /// World name
        /// </summary>
        public static string WorldName { get; private set; } = string.Empty;

        /// <summary>
        /// World description
        /// </summary>
        public static string WorldDescription { get; private set; } = string.Empty;

        /// <summary>
        /// World seed
        /// </summary>
        public static int WorldSeed { get; private set; }

        /// <summary>
        /// Game state
        /// </summary>
        public static EGameState GameState
        {
            get => (GameManagerScript.Instance ? GameManagerScript.Instance.GameState : EGameState.Nothing);
            set
            {
                if (GameManagerScript.Instance)
                {
                    GameManagerScript.Instance.GameState = value;
                }
            }
        }

        /// <summary>
        /// Save game data
        /// </summary>
        public static ISaveGameData SaveGameData
        {
            get
            {
                SaveGame<SaveGameData> save_game = SaveGames.Get<SaveGameData>();
                if (save_game == null)
                {
                    throw new MissingReferenceException("Save game data couldn't be instantiated.");
                }
                return save_game.Data;
            }
        }

        /// <summary>
        /// Create new world
        /// </summary>
        /// <param name="worldName">World name</param>
        /// <param name="worldDescription">World description</param>
        /// <param name="worldSeed">World seed</param>
        public static void CreateNewWorld(string worldName, string worldDescription, int worldSeed)
        {
            if (worldName == null)
            {
                throw new ArgumentNullException(nameof(worldName));
            }
            if (worldDescription == null)
            {
                throw new ArgumentNullException(nameof(worldDescription));
            }
            WorldGUID = Guid.NewGuid();
            WorldName = worldName;
            WorldDescription = worldDescription;
            WorldSeed = worldSeed;
            SceneLoaderManager.LoadScene("GameScene");
        }

        /// <summary>
        /// Load world
        /// </summary>
        /// <param name="worldGUID">World GUID</param>
        public static void LoadWorld(Guid worldGUID)
        {
            if (WorldIO.DoesWorldFileExist(worldGUID))
            {
                WorldGUID = worldGUID;
                WorldName = string.Empty;
                WorldDescription = string.Empty;
                WorldSeed = 0;
                SceneLoaderManager.LoadScene("GameScene");
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        public static void Save()
        {
            SaveGame<SaveGameData> save_game = SaveGames.Get<SaveGameData>();
            if (save_game == null)
            {
                throw new MissingReferenceException("Save game data couldn't be instantiated.");
            }
            save_game.Save();
            WorldManagerScript world_manager = WorldManagerScript.Instance;
            if (world_manager)
            {
                world_manager.Save();
            }
        }
    }
}

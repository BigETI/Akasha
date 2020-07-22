using Akasha.Data;
using UnityEngine;
using UnitySaveGame;

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
        }
    }
}

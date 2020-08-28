using System;
using UnityEngine;
using UnitySaveGame;

/// <summary>
/// Akasha data namespace
/// </summary>
namespace Akasha.Data
{
    /// <summary>
    /// Save game data class
    /// </summary>
    [Serializable]
    public class SaveGameData : ASaveGameData, ISaveGameData
    {
        /// <summary>
        /// Invert X axis
        /// </summary>
        [SerializeField]
        private bool invertXAxis = default;

        /// <summary>
        /// Invert Y axis
        /// </summary>
        [SerializeField]
        private bool invertYAxis = default;

        /// <summary>
        /// Enable view bobbing
        /// </summary>
        [SerializeField]
        private bool enableViewBobbing = true;

        /// <summary>
        /// Sensitivity
        /// </summary>
        [SerializeField]
        private Vector2 sensitivity = Vector2.one * 15.0f;

        /// <summary>
        /// Player character GUID
        /// </summary>
        [SerializeField]
        private string playerCharacterGUID = string.Empty;

        /// <summary>
        /// Invert X axis
        /// </summary>
        public bool InvertXAxis
        {
            get => invertXAxis;
            set => invertXAxis = value;
        }

        /// <summary>
        /// Invert Y axis
        /// </summary>
        public bool InvertYAxis
        {
            get => invertYAxis;
            set => invertYAxis = value;
        }

        /// <summary>
        /// Enable view bobbing
        /// </summary>
        public bool EnableViewBobbing
        {
            get => enableViewBobbing;
            set => enableViewBobbing = value;
        }

        /// <summary>
        /// Sensitivity
        /// </summary>
        public Vector2 Sensitivity
        {
            get => new Vector2(Mathf.Max(sensitivity.x, 0.0f), Mathf.Max(sensitivity.y, 0.0f));
            set => sensitivity = new Vector2(Mathf.Max(value.x, 0.0f), Mathf.Max(value.y, 0.0f));
        }

        /// <summary>
        /// Player character GUID
        /// </summary>
        public Guid PlayerCharacterGUID
        {
            get
            {
                Guid ret;
                if ((playerCharacterGUID == null) || !(Guid.TryParse(playerCharacterGUID, out ret)))
                {
                    ret = Guid.NewGuid();
                    playerCharacterGUID = ret.ToString();
                }
                return ret;
            }
            set => playerCharacterGUID = value.ToString();
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SaveGameData() : base(null)
        {
            // ...
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="saveGameData">Save game data</param>
        public SaveGameData(ASaveGameData saveGameData) : base(saveGameData)
        {
            if (saveGameData is SaveGameData save_game_data)
            {
                invertXAxis = save_game_data.invertXAxis;
                invertYAxis = save_game_data.invertYAxis;
                enableViewBobbing = save_game_data.enableViewBobbing;
                Sensitivity = save_game_data.Sensitivity;
                PlayerCharacterGUID = save_game_data.PlayerCharacterGUID;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Worlds panel UI controller script class
    /// </summary>
    public class WorldsPanelUIControllerScript : MonoBehaviour, IWorldsPanelUIController
    {
        /// <summary>
        /// World panel asset
        /// </summary>
        [SerializeField]
        private GameObject worldPanelAsset;

        /// <summary>
        /// Level panel game objects
        /// </summary>
        private readonly List<GameObject> levelPanelGameObjects = new List<GameObject>();

        /// <summary>
        /// World panel asset
        /// </summary>
        public GameObject WorldPanelAsset
        {
            get => worldPanelAsset;
            set => worldPanelAsset = value;
        }

        /// <summary>
        /// Rectangle transform
        /// </summary>
        public RectTransform RectangleTransform { get; private set; }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            RectangleTransform = GetComponent<RectTransform>();
            foreach (GameObject world_panel_game_object in levelPanelGameObjects)
            {
                if (world_panel_game_object)
                {
                    Destroy(world_panel_game_object);
                }
            }
            if (worldPanelAsset)
            {
                foreach (Guid world_file_guid in Worlds.WorldFileGUIDs)
                {
                    GameObject game_object = Instantiate(worldPanelAsset);
                    if (game_object)
                    {
                        if (game_object.TryGetComponent(out RectTransform world_panel_rectangle_transform) && game_object.TryGetComponent(out WorldPanelUIControllerScript world_panel_ui_controller))
                        {
                            // TODO
                        }
                        else
                        {
                            Destroy(game_object);
                        }
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Worlds panel UI controller script class
    /// </summary>
    [RequireComponent(typeof(VerticalLayoutGroup))]
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
        /// Vertical layout group
        /// </summary>
        public VerticalLayoutGroup VerticalLayoutGroup { get; private set; }

        /// <summary>
        /// Update visuals
        /// </summary>
        public void UpdateVisuals()
        {
            foreach (GameObject world_panel_game_object in levelPanelGameObjects)
            {
                if (world_panel_game_object)
                {
                    Destroy(world_panel_game_object);
                }
            }
            levelPanelGameObjects.Clear();
            if (worldPanelAsset)
            {
                float height = 0.0f;
                uint count = 0U;
                foreach (Guid world_file_guid in Worlds.WorldFileGUIDs)
                {
                    GameObject game_object = Instantiate(worldPanelAsset);
                    if (game_object)
                    {
                        if (game_object.TryGetComponent(out RectTransform world_panel_rectangle_transform) && game_object.TryGetComponent(out WorldPanelUIControllerScript world_panel_ui_controller))
                        {
                            world_panel_rectangle_transform.SetParent(transform, false);
                            world_panel_ui_controller.SetValues(world_file_guid, this);
                            levelPanelGameObjects.Add(game_object);
                            height += world_panel_rectangle_transform.rect.height;
                            ++count;
                        }
                        else
                        {
                            Destroy(game_object);
                        }
                    }
                }
                if (RectangleTransform && VerticalLayoutGroup)
                {
                    Vector2 size = RectangleTransform.sizeDelta;
                    RectangleTransform.sizeDelta = new Vector2(size.x, ((count > 1U) ? (VerticalLayoutGroup.spacing * (count - 1U)) : 0.0f) + VerticalLayoutGroup.padding.top + VerticalLayoutGroup.padding.bottom);
                }
            }
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            RectangleTransform = GetComponent<RectTransform>();
            VerticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
            UpdateVisuals();
        }
    }
}

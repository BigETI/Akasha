using Akasha.Managers;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// World panel UI controller script class
    /// </summary>
    public class WorldPanelUIControllerScript : MonoBehaviour, IWorldPanelUIController
    {
        /// <summary>
        /// Preview image
        /// </summary>
        [SerializeField]
        private Image previewImage;

        /// <summary>
        /// World name text
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI worldNameText;

        /// <summary>
        /// World description text
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI worldDescriptionText;

        /// <summary>
        /// Read world preview task
        /// </summary>
        private Task<WorldPreview> readWorldPreviewTask;

        /// <summary>
        /// Preview image
        /// </summary>
        public Image PreviewImage
        {
            get => previewImage;
            set => previewImage = value;
        }

        /// <summary>
        /// World name text
        /// </summary>
        public TextMeshProUGUI WorldNameText
        {
            get => worldNameText;
            set => worldNameText = value;
        }

        /// <summary>
        /// World description text
        /// </summary>
        public TextMeshProUGUI WorldDescriptionText
        {
            get => worldDescriptionText;
            set => worldDescriptionText = value;
        }

        /// <summary>
        /// Set values
        /// </summary>
        /// <param name="worldGUID">World GUID</param>
        public void SetValues(Guid worldGUID)
        {
            if (readWorldPreviewTask != null)
            {
                try
                {
                    readWorldPreviewTask.Wait();
                    readWorldPreviewTask.Dispose();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
            readWorldPreviewTask = WorldIO.CreateReadWorldPreviewTask(worldGUID);
        }

        /// <summary>
        /// Click
        /// </summary>
        public void Click()
        {
            // TODO
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (readWorldPreviewTask != null)
            {
                TaskStatus task_status = readWorldPreviewTask.Status;
                if (task_status != TaskStatus.Running)
                {
                    if (task_status == TaskStatus.RanToCompletion)
                    {
                        WorldPreview world_preview = readWorldPreviewTask.Result;
                        if (world_preview.IsValid)
                        {
                            if (previewImage && (world_preview.PreviewImageData != null))
                            {
                                Texture2D texture = new Texture2D(240, 135, TextureFormat.RGB24, false);
                                if (texture.LoadImage(world_preview.PreviewImageData))
                                {
                                    texture.Apply();
                                    previewImage.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(texture.width * 0.5f, texture.height * 0.5f));
                                }
                            }
                            if (worldNameText)
                            {
                                worldNameText.text = world_preview.MetaData.WorldName;
                            }
                            if (worldDescriptionText)
                            {
                                worldDescriptionText.text = world_preview.MetaData.WorldName;
                            }
                        }
                    }
                    try
                    {
                        readWorldPreviewTask.Dispose();
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }
                    readWorldPreviewTask = null;
                }
            }
        }
    }
}

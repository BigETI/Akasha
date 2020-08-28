using UnityEngine;

/// <summary>
/// Akasha objects namespace
/// </summary>
namespace Akasha.Objects
{
    /// <summary>
    /// Entity object script class
    /// </summary>
    [CreateAssetMenu(fileName = "Entity", menuName = "Akasha/Entity")]
    public class EntityObjectScript : ScriptableObject, IEntityObject
    {
        /// <summary>
        /// Asset
        /// </summary>
        [SerializeField]
        private GameObject asset = default;

        /// <summary>
        /// Asset
        /// </summary>
        public GameObject Asset => asset;

        /// <summary>
        /// Key
        /// </summary>
        public virtual string Key => $"Misc/{ name }";
    }
}

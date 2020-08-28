using UnityEngine;

/// <summary>
/// Akasha objects namespace
/// </summary>
namespace Akasha.Objects
{
    /// <summary>
    /// Furniture object script class
    /// </summary>
    [CreateAssetMenu(fileName = "Furniture", menuName = "Akasha/Furniture")]
    public class FurnitureObjectScript : EntityObjectScript, IFurnitureObject
    {
        /// <summary>
        /// Key
        /// </summary>
        public override string Key => $"Furniture/{ name }";
    }
}

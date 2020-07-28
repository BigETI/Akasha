using UnityEngine;
/// <summary>
/// Akasha objects namespace
/// </summary>
namespace Akasha.Objects
{
    /// <summary>
    /// Ammo object script class
    /// </summary>
    [CreateAssetMenu(fileName = "Ammo", menuName = "Akasha/Ammo")]
    public class AmmoObjectScript : ItemObjectScript, IAmmoObject
    {
        /// <summary>
        /// Key
        /// </summary>
        public override string Key => "Ammo/" + name;
    }
}

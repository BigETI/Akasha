using UnityEngine;

/// <summary>
/// Akasha objects namespace
/// </summary>
namespace Akasha.Objects
{
    /// <summary>
    /// Character object script class
    /// </summary>
    [CreateAssetMenu(fileName = "Character", menuName = "Akasha/Character")]
    public class CharacterObjectScript : EntityObjectScript, ICharacterObject
    {
        /// <summary>
        /// Key
        /// </summary>
        public override string Key => $"Characters/{ name }";
    }
}

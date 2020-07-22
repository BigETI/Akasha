using UnityEngine;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Suicide controller script class
    /// </summary>
    public class SuicideControllerScript : MonoBehaviour, ISuicideController
    {
        /// <summary>
        /// Destroy myself
        /// </summary>
        public void DestroyMyself()
        {
            Destroy(gameObject);
        }
    }
}

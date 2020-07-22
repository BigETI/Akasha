using UnityEngine;

/// <summary>
/// Akasha managers namespace
/// </summary>
namespace Akasha.Managers
{
    /// <summary>
    /// Manager abstract class
    /// </summary>
    public abstract class AManagerScript<T> : MonoBehaviour, IBehaviour where T : AManagerScript<T>
    {
        /// <summary>
        /// Instance
        /// </summary>
        public static T Instance { get; private set; }

        /// <summary>
        /// On enable
        /// </summary>
        protected virtual void OnEnable()
        {
            if (Instance == null)
            {
                Instance = (T)this;
            }
        }

        /// <summary>
        /// On disable
        /// </summary>
        protected virtual void OnDisable()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}

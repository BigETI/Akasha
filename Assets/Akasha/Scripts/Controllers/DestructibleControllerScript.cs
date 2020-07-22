using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Destructlibe controller script class
    /// </summary>
    public class DestructibleControllerScript : MonoBehaviour, IDestructibleController
    {
        /// <summary>
        /// Health
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float health = 100.0f;

        /// <summary>
        /// On destroyed
        /// </summary>
        [SerializeField]
        private UnityEvent onDestroyed = default;

        /// <summary>
        /// Last health
        /// </summary>
        private float lastHealth;

        /// <summary>
        /// Health
        /// </summary>
        public float Health
        {
            get => Mathf.Max(health, 0.0f);
            set => health = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// On destroyed
        /// </summary>
        public event Action OnDestroyed;

        /// <summary>
        /// Start
        /// </summary>
        protected virtual void Start()
        {
            lastHealth = Health;
            if (lastHealth <= float.Epsilon)
            {
                OnDestroyed?.Invoke();
                if (onDestroyed != null)
                {
                    onDestroyed.Invoke();
                }
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            float health = Health;
            if (lastHealth != health)
            {
                lastHealth = health;
                if (health <= float.Epsilon)
                {
                    OnDestroyed?.Invoke();
                    if (onDestroyed != null)
                    {
                        onDestroyed.Invoke();
                    }
                }
            }
        }
    }
}

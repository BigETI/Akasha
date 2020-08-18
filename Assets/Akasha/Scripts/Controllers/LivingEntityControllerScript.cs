using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Living entity controller script class
    /// </summary>
    public class LivingEntityControllerScript : EntityControllerScript, ILivingEntityController
    {
        /// <summary>
        /// Health
        /// </summary>
        [SerializeField]
        [Range(0.0f, float.PositiveInfinity)]
        private float health = 100.0f;

        /// <summary>
        /// Armor
        /// </summary>
        [SerializeField]
        [Range(0.0f, float.PositiveInfinity)]
        private float armor = default;

        /// <summary>
        /// Maximal health
        /// </summary>
        [Range(0.0f, float.PositiveInfinity)]
        private float maximalHealth = 100.0f;

        /// <summary>
        /// Maximal armor
        /// </summary>
        [Range(0.0f, float.PositiveInfinity)]
        private float maximalArmor = 100.0f;

        /// <summary>
        /// On died
        /// </summary>
        [SerializeField]
        private UnityEvent onDied = default;

        /// <summary>
        /// Last health
        /// </summary>
        private float lastHealth;

        /// <summary>
        /// Health
        /// </summary>
        public float Health
        {
            get => Mathf.Clamp(health, 0.0f, MaximalHealth);
            set => health = Mathf.Clamp(value, 0.0f, MaximalHealth);
        }

        /// <summary>
        /// Armor
        /// </summary>
        public float Armor
        {
            get => Mathf.Clamp(armor, 0.0f, MaximalArmor);
            set => armor = Mathf.Clamp(value, 0.0f, MaximalArmor);
        }

        /// <summary>
        /// Maximal health
        /// </summary>
        public float MaximalHealth
        {
            get => Mathf.Max(maximalHealth, 0.0f);
            set => maximalHealth = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Maximal armor
        /// </summary>
        public float MaximalArmor
        {
            get => Mathf.Max(maximalArmor, 0.0f);
            set => maximalArmor = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Is alive
        /// </summary>
        public bool IsAlive => Health > float.Epsilon;

        /// <summary>
        /// On died
        /// </summary>
        public event DiedDelegete OnDied;

        /// <summary>
        /// Damage
        /// </summary>
        /// <param name="value">Value</param>
        public void Damage(float value)
        {
            if (value > float.Epsilon)
            {
                float remaining;
                float armor = Armor;
                if (armor <= value)
                {
                    remaining = value - armor;
                    Armor = 0.0f;
                }
                else
                {
                    remaining = 0.0f;
                    Armor -= value;
                }
                Health -= remaining;
            }
        }

        /// <summary>
        /// Start
        /// </summary>
        protected override void Start()
        {
            base.Start();
            lastHealth = Health;
            if (lastHealth <= float.Epsilon)
            {
                OnDied?.Invoke();
                if (onDied != null)
                {
                    onDied.Invoke();
                }
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        protected virtual void Update()
        {
            float health = Health;
            if (lastHealth != health)
            {
                lastHealth = health;
                if (health <= float.Epsilon)
                {
                    OnDied?.Invoke();
                    if (onDied != null)
                    {
                        onDied.Invoke();
                    }
                }
            }
        }
    }
}

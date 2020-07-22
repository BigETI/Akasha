using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Explosive controller script class
    /// </summary>
    public class ExplosiveControllerScript : DestructibleControllerScript, IExplosiveController
    {
        /// <summary>
        /// Explosion radius
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float explosionRadius = 20.0f;

        /// <summary>
        /// Maximal damage
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float maximalDamage = 200.0f;

        /// <summary>
        /// Maximal knockback impulse
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100000.0f)]
        private float maximalKnockbackImpulse = 10000.0f;

        /// <summary>
        /// Explosion radius
        /// </summary>
        public float ExplosionRadius
        {
            get => Mathf.Max(explosionRadius, 0.0f);
            set => explosionRadius = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Maximal damage
        /// </summary>
        public float MaximalDamage
        {
            get => Mathf.Max(maximalDamage, 0.0f);
            set => maximalDamage = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Maximal knockback impulse
        /// </summary>
        public float MaximalKnockbackImpulse
        {
            get => Mathf.Max(maximalKnockbackImpulse, 0.0f);
            set => maximalKnockbackImpulse = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Explode
        /// </summary>
        public void Explode()
        {
            float explosion_radius = ExplosionRadius;
            Health = 0.0f;
            if (explosion_radius > float.Epsilon)
            {
                float explosion_radius_squared = explosion_radius * explosion_radius;
                DestructibleControllerScript[] destructible_controllers = FindObjectsOfType<DestructibleControllerScript>();
                if (destructible_controllers != null)
                {
                    float maximal_damage = MaximalDamage;
                    foreach (DestructibleControllerScript destructible_controller in destructible_controllers)
                    {
                        if (destructible_controller != null)
                        {
                            Vector3 delta = destructible_controller.transform.position - transform.position;
                            if (delta.sqrMagnitude < explosion_radius_squared)
                            {
                                GameObject game_object = destructible_controller.gameObject;
                                bool success = true;
                                while (game_object != null)
                                {
                                    if (game_object == gameObject)
                                    {
                                        success = false;
                                        break;
                                    }
                                    if (game_object.transform.parent == null)
                                    {
                                        break;
                                    }
                                    game_object = game_object.transform.parent.gameObject;
                                }
                                if (success)
                                {
                                    destructible_controller.Health -= Mathf.Lerp(maximal_damage, 0.0f, Mathf.Sqrt(delta.magnitude / explosion_radius));
                                }
                            }
                        }
                    }
                }
                Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();
                if (rigidbodies != null)
                {
                    float maximal_knockback_impulse = MaximalKnockbackImpulse;
                    foreach (Rigidbody rigidbody in rigidbodies)
                    {
                        if (rigidbody != null)
                        {
                            Vector3 delta = rigidbody.transform.position - transform.position;
                            if (delta.sqrMagnitude < explosion_radius_squared)
                            {
                                GameObject game_object = rigidbody.gameObject;
                                bool success = true;
                                while (game_object != null)
                                {
                                    if (game_object == gameObject)
                                    {
                                        success = false;
                                        break;
                                    }
                                    if (game_object.transform.parent == null)
                                    {
                                        break;
                                    }
                                    game_object = game_object.transform.parent.gameObject;
                                }
                                if (success)
                                {
                                    float delta_magnitude_squared = delta.sqrMagnitude;
                                    rigidbody.AddForceAtPosition(((delta_magnitude_squared > float.Epsilon) ? (delta / (delta_magnitude_squared * delta_magnitude_squared)) : Vector3.up) * Mathf.Lerp(maximal_knockback_impulse, 0.0f, Mathf.Sqrt(delta.magnitude / explosion_radius)), transform.position, ForceMode.Impulse);
                                }
                            }
                        }
                    }
                }
            }
            Destroy(gameObject);
        }

        /// <summary>
        /// Start
        /// </summary>
        protected override void Start()
        {
            base.Start();
            OnDestroyed += Explode;
        }
    }
}

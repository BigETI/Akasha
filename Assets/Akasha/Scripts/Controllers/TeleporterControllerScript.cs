using UnityEngine;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Teleporter conroller script class
    /// </summary>
    public class TeleporterControllerScript : MonoBehaviour, ITeleporterController
    {
        /// <summary>
        /// Target transform
        /// </summary>
        [SerializeField]
        private Transform targetTransform;

        /// <summary>
        /// Target transform
        /// </summary>
        public Transform TargetTransform
        {
            get => targetTransform;
            set => targetTransform = value;
        }

        /// <summary>
        /// On trigger enter
        /// </summary>
        /// <param name="collider">Collider</param>
        private void OnTriggerEnter(Collider collider)
        {
            if (targetTransform != null)
            {
                GameObject game_object = collider.gameObject;
                while (game_object != null)
                {
                    Rigidbody rigidbody = game_object.GetComponent<Rigidbody>();
                    if (rigidbody != null)
                    {
                        Vector3 delta = rigidbody.position - transform.position;
                        rigidbody.position = targetTransform.position + delta;
                        break;
                    }
                    if (game_object.transform.parent == null)
                    {
                        break;
                    }
                    game_object = game_object.transform.parent.gameObject;
                }
            }
        }

        /// <summary>
        /// On draw gizmos
        /// </summary>
        private void OnDrawGizmos()
        {
            if (targetTransform != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, targetTransform.position);
                Gizmos.DrawWireSphere(targetTransform.position, 1.0f);
            }
        }
    }
}

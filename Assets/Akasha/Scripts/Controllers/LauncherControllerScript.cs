using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Launcher controller script class
    /// </summary>
    public class LauncherControllerScript : MonoBehaviour, ILauncherController
    {
        /// <summary>
        /// Jump height
        /// </summary>
        [SerializeField]
        private float jumpHeight = 10.0f;

        /// <summary>
        /// On launch
        /// </summary>
        [SerializeField]
        private UnityEvent onLaunch = default;

        /// <summary>
        /// Jump height
        /// </summary>
        public float JumpHeight
        {
            get => Mathf.Max(jumpHeight, 0.0f);
            set => jumpHeight = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// On trigger enter
        /// </summary>
        /// <param name="collider">Collider</param>
        private void OnTriggerEnter(Collider collider)
        {
            GameObject game_object = collider.gameObject;
            while ((game_object != gameObject) && (game_object != null))
            {
                Rigidbody rigidbody = game_object.GetComponent<Rigidbody>();
                if (rigidbody == null)
                {
                    if (game_object.transform.parent == null)
                    {
                        break;
                    }
                    game_object = game_object.transform.parent.gameObject;
                }
                else
                {
                    rigidbody.AddForce(transform.up * (Mathf.Sqrt(-2.0f * JumpHeight * Physics.gravity.y) - Vector3.Dot(transform.up, rigidbody.velocity)), ForceMode.VelocityChange);
                    if (onLaunch != null)
                    {
                        onLaunch.Invoke();
                    }
                    break;
                }
            }
        }
    }
}

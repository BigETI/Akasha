using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Modify parent transform controller script class
    /// </summary>
    public class ModifyParentTransformControllerScript : MonoBehaviour, IModifyParentTransformController
    {
        /// <summary>
        /// Children transforms
        /// </summary>
        private Dictionary<int, ITransformAndOldParent> childrenTransforms = new Dictionary<int, ITransformAndOldParent>();

        /// <summary>
        /// Children transforms
        /// </summary>
        public IReadOnlyDictionary<int, ITransformAndOldParent> ChildrenTransforms => childrenTransforms;

        /// <summary>
        /// On trigger enter
        /// </summary>
        /// <param name="collider">Collider</param>
        private void OnTriggerEnter(Collider collider)
        {
            bool success = true;
            GameObject game_object = collider.gameObject;
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
                game_object = collider.gameObject;
                while (game_object != null)
                {
                    if (game_object.GetComponent<Rigidbody>() != null)
                    {
                        int id = game_object.GetInstanceID();
                        if (!(childrenTransforms.ContainsKey(id)))
                        {
                            TransformAndOldParent transform_and_old_parent = new TransformAndOldParent(game_object.transform, game_object.transform.parent);
                            game_object.transform.parent = transform;
                            childrenTransforms.Add(id, transform_and_old_parent);
                        }
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
        /// On trigger exit
        /// </summary>
        /// <param name="collider">Collider</param>
        private void OnTriggerExit(Collider collider)
        {
            bool success = true;
            GameObject game_object = collider.gameObject;
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
                game_object = collider.gameObject;
                while (game_object != null)
                {
                    if (game_object.GetComponent<Rigidbody>() != null)
                    {
                        int id = game_object.GetInstanceID();
                        if (childrenTransforms.ContainsKey(id))
                        {
                            game_object.transform.parent = childrenTransforms[id].OldParent;
                            childrenTransforms.Remove(id);
                        }
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
    }
}

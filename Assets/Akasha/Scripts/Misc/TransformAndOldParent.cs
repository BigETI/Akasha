using System;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Transform and old arent structure
    /// </summary>
    public readonly struct TransformAndOldParent : ITransformAndOldParent
    {
        /// <summary>
        /// Transform
        /// </summary>
        public Transform Transform { get; }

        /// <summary>
        /// Old parent transform
        /// </summary>
        public Transform OldParent { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transform">Transform</param>
        /// <param name="oldParent">Old parent transform</param>
        public TransformAndOldParent(Transform transform, Transform oldParent)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }
            Transform = transform;
            OldParent = oldParent;
        }
    }
}

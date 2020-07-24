using Akasha.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Block object script class
    /// </summary>
    [CreateAssetMenu(fileName = "Block", menuName = "Akasha/Block")]
    public class BlockObjectScript : ItemObjectScript, IBlockObject
    {
        /// <summary>
        /// Block material
        /// </summary>
        [SerializeField]
        private Material material = default;

        /// <summary>
        /// Mesh variants
        /// </summary>
        [SerializeField]
        private BlockMeshVariantsObjectScript meshVariants = default;

        /// <summary>
        /// Collision bounds
        /// </summary>
        [SerializeField]
        private Bounds[] collisionBounds = new Bounds[]
        {
            new Bounds(Vector3.zero, Vector3.one)
        };

        /// <summary>
        /// Block material
        /// </summary>
        public Material Material => material;

        /// <summary>
        /// Mesh variants
        /// </summary>
        public IBlockMeshVariantsObject MeshVariants => meshVariants;

        /// <summary>
        /// Collision bounds
        /// </summary>
        public IReadOnlyList<Bounds> CollisionBounds
        {
            get
            {
                if (collisionBounds == null)
                {
                    collisionBounds = Array.Empty<Bounds>();
                }
                return collisionBounds;
            }
        }
    }
}

using Akasha.Objects;
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
    public class BlockObjectScript : ScriptableObject, IBlockObject
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
        /// Block material
        /// </summary>
        public Material Material => material;

        /// <summary>
        /// Mesh variants
        /// </summary>
        public IBlockMeshVariantsObject MeshVariants => meshVariants;
    }
}

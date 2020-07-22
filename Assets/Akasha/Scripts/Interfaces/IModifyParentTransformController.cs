using System.Collections.Generic;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Modify parent transform controller interface
    /// </summary>
    public interface IModifyParentTransformController : IBehaviour
    {
        /// <summary>
        /// Children transforms
        /// </summary>
        IReadOnlyDictionary<int, ITransformAndOldParent> ChildrenTransforms { get; }
    }
}

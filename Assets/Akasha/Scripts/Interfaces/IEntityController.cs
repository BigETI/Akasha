using Akasha.Objects;
using System;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Entity controller interface
    /// </summary>
    public interface IEntityController : IBehaviour
    {
        /// <summary>
        /// Bounds
        /// </summary>
        Bounds Bounds { get; set; }

        /// <summary>
        /// Maximal sample collision count
        /// </summary>
        uint MaximalSampleCollisionCount { get; set; }

        /// <summary>
        /// Maximal sample collision count
        /// </summary>
        float SampleCollisionDistanceMultiplier { get; set; }

        /// <summary>
        /// Entity object
        /// </summary>
        EntityObjectScript EntityObject { get; set; }

        /// <summary>
        /// Bounds chunk blocks size
        /// </summary>
        Vector3Int BoundsChunkBlocksSize { get; }

        /// <summary>
        /// GUID
        /// </summary>
        Guid GUID { get; set; }

        /// <summary>
        /// World transform controller
        /// </summary>
        IWorldTransformController WorldTransformController { get; }

        /// <summary>
        /// Test collision
        /// </summary>
        /// <param name="worldPosition">World position</param>
        /// <returns>"true" if colliding, otherwise "false"</returns>
        bool TestCollision(Vector3 worldPosition);

        /// <summary>
        /// Move
        /// </summary>
        /// <param name="motion">Motion</param>
        /// <returns>"true" if collision happened, otherwise "false"</returns>
        bool Move(Vector3 motion);
    }
}

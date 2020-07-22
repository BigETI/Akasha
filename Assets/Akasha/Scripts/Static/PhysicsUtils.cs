using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Physics utilities class
    /// </summary>
    public static class PhysicsUtils
    {
        /// <summary>
        /// Ray cast
        /// </summary>
        /// <param name="origin">Origin</param>
        /// <param name="direction">Direction</param>
        /// <param name="distance">Distance</param>
        /// <param name="results">Results</param>
        /// <returns>Number of hits</returns>
        public static int Raycast(Vector3 origin, Vector3 direction, float distance, ref RaycastHit[] results)
        {
            int ret;
            bool allocate_more_raycast_hits = false;
            if (results == null)
            {
                results = new RaycastHit[128];
            }
            else if (results.Length <= 0)
            {
                results = new RaycastHit[128];
            }
            do
            {
                if (allocate_more_raycast_hits)
                {
                    results = new RaycastHit[results.Length * 2];
                }
                ret = Physics.RaycastNonAlloc(origin, direction, results, distance);
                allocate_more_raycast_hits = (ret >= results.Length);
            }
            while (allocate_more_raycast_hits);
            return ret;
        }

        /// <summary>
        /// Sphere cast
        /// </summary>
        /// <param name="origin">Origin</param>
        /// <param name="radius">Radius</param>
        /// <param name="direction">Direction</param>
        /// <param name="distance">Distance</param>
        /// <param name="results">Results</param>
        /// <returns>Number of hits</returns>
        public static int SphereCast(Vector3 origin, float radius, Vector3 direction, float distance, ref RaycastHit[] results)
        {
            int ret;
            bool allocate_more_raycast_hits = false;
            if (results == null)
            {
                results = new RaycastHit[128];
            }
            else if (results.Length <= 0)
            {
                results = new RaycastHit[128];
            }
            do
            {
                if (allocate_more_raycast_hits)
                {
                    results = new RaycastHit[results.Length * 2];
                }
                ret = Physics.SphereCastNonAlloc(origin, radius, direction, results, distance);
                allocate_more_raycast_hits = (ret >= results.Length);
            }
            while (allocate_more_raycast_hits);
            return ret;
        }

        /// <summary>
        /// Capsule cast
        /// </summary>
        /// <param name="pointOne">Point one</param>
        /// <param name="pointTwo">Point two</param>
        /// <param name="radius">Radius</param>
        /// <param name="direction">Direction</param>
        /// <param name="distance">Distance</param>
        /// <param name="results">Results</param>
        /// <returns>Number of hits</returns>
        public static int CapsuleCast(Vector3 pointOne, Vector3 pointTwo, float radius, Vector3 direction, float distance, ref RaycastHit[] results)
        {
            int ret;
            bool allocate_more_raycast_hits = false;
            if (results == null)
            {
                results = new RaycastHit[128];
            }
            else if (results.Length <= 0)
            {
                results = new RaycastHit[128];
            }
            do
            {
                if (allocate_more_raycast_hits)
                {
                    results = new RaycastHit[results.Length * 2];
                }
                ret = Physics.CapsuleCastNonAlloc(pointOne, pointTwo, radius, direction, results, distance);
                allocate_more_raycast_hits = (ret >= results.Length);
            }
            while (allocate_more_raycast_hits);
            return ret;
        }
    }
}

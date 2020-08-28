using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// Worlds
    /// </summary>
    public static class Worlds
    {
        /// <summary>
        /// World file GUIDs
        /// </summary>
        public static IEnumerable<Guid> WorldFileGUIDs
        {
            get
            {
                List<Guid> world_file_guids = new List<Guid>();
                try
                {
                    string worlds_directory_path = WorldIO.WorldsDirectoryPath;
                    string[] world_file_paths = (Directory.Exists(worlds_directory_path) ? Directory.GetFiles(WorldIO.WorldsDirectoryPath, "*.world", SearchOption.TopDirectoryOnly) : null);
                    if (world_file_paths != null)
                    {
                        foreach (string world_file_path in world_file_paths)
                        {
                            if (world_file_path != null)
                            {
                                if (Guid.TryParse(Path.GetFileNameWithoutExtension(world_file_path), out Guid world_file_guid))
                                {
                                    world_file_guids.Add(world_file_guid);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
                return world_file_guids;
            }
        }
    }
}

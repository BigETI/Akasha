using Akasha.Data;
using Akasha.Objects;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Akasha namespace
/// </summary>
namespace Akasha
{
    /// <summary>
    /// World IO class
    /// </summary>
    public class WorldIO : IWorldIO
    {
        /// <summary>
        /// Persistent data path
        /// </summary>
        private static readonly string persistentDataPath = Application.persistentDataPath;

        /// <summary>
        /// Temporary cache path
        /// </summary>
        private static readonly string temporaryCachePath = Application.temporaryCachePath;

        /// <summary>
        /// Meta JSON file path
        /// </summary>
        private static readonly string metaJSONFilePath = "meta.json";

        /// <summary>
        /// Preview PNG file path
        /// </summary>
        private static readonly string previewPNGFilePath = "preview.png";

        /// <summary>
        /// Players directory file path
        /// </summary>
        private static readonly string playersDirectoryPath = "players";

        /// <summary>
        /// World chunks file path
        /// </summary>
        private static readonly string worldChunksFilePath = "world.chunks";

        /// <summary>
        /// World entities file path
        /// </summary>
        private static readonly string worldEntitiesFilePath = "world.entities";

        /// <summary>
        /// Get world chunks temporary file path
        /// </summary>
        private static readonly string worldChunksTemporaryFilePath = $"{ worldChunksFilePath }.temp";

        /// <summary>
        /// Get world entities temporary file path
        /// </summary>
        private static readonly string worldEntitiesTemporaryFilePath = $"{ worldEntitiesFilePath }.temp";

        /// <summary>
        /// World chunks file signature "AkashaC0"
        /// </summary>
        private static readonly long worldChunksFileSignature = 0x3043616873616B41;

        /// <summary>
        /// World entities file signature "AkashaE0"
        /// </summary>
        private static readonly long worldEntitiesFileSignature = 0x3045616873616B41;

        /// <summary>
        /// Chunk data lookup
        /// </summary>
        private Dictionary<ChunkID, long> chunkDataLookup = new Dictionary<ChunkID, long>();

        /// <summary>
        /// Entity data lookup
        /// </summary>
        private Dictionary<string, long> entityDataLookup = new Dictionary<string, long>();

        /// <summary>
        /// Chunk entities lookup
        /// </summary>
        private Dictionary<ChunkID, List<string>> chunkEntitiesLookup = new Dictionary<ChunkID, List<string>>();

        /// <summary>
        /// Block palette
        /// </summary>
        private BlockObjectScript[] blockPalette = Array.Empty<BlockObjectScript>();

        /// <summary>
        /// Worlds directory path
        /// </summary>
        public static string WorldsDirectoryPath => Path.Combine(persistentDataPath, "worlds");

        /// <summary>
        /// Worlds cache directory path
        /// </summary>
        public static string WorldsCacheDirectoryPath => Path.Combine(temporaryCachePath, "worlds");

        /// <summary>
        /// World GUID
        /// </summary>
        public Guid WorldGUID { get; private set; }

        /// <summary>
        /// World name
        /// </summary>
        public string WorldName { get; private set; } = string.Empty;

        /// <summary>
        /// World description
        /// </summary>
        public string WorldDescription { get; private set; } = string.Empty;

        /// <summary>
        /// World seed
        /// </summary>
        public int WorldSeed { get; private set; }

        /// <summary>
        /// Chunk size
        /// </summary>
        public Vector3Int ChunkSize { get; private set; }

        /// <summary>
        /// World chunks file stream
        /// </summary>
        public ReopenableFileStream WorldChunksFileStream { get; private set; }

        /// <summary>
        /// World entities file stream
        /// </summary>
        public ReopenableFileStream WorldEntitiesFileStream { get; private set; }

        /// <summary>
        /// World manager
        /// </summary>
        public IWorldManager WorldManager { get; private set; }

        /// <summary>
        /// Can read
        /// </summary>
        public bool CanRead => (WorldChunksFileStream.CanRead && WorldChunksFileStream.CanSeek && WorldEntitiesFileStream.CanRead && WorldEntitiesFileStream.CanSeek);

        /// <summary>
        /// Can write
        /// </summary>
        public bool CanWrite => (WorldChunksFileStream.CanWrite && WorldChunksFileStream.CanSeek && WorldEntitiesFileStream.CanWrite && WorldEntitiesFileStream.CanSeek);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="worldGUID">World GUID</param>
        /// <param name="worldName">World name</param>
        /// <param name="worldSeed">World seed</param>
        /// <param name="chunkSize">Chunk size</param>
        /// <param name="chunkDataLookup">Chunk data lookup</param>
        /// <param name="entityDataLookup">Entity data lookup</param>
        /// <param name="chunkEntitiesLookup">Chunk entities lookup</param>
        /// <param name="blockPalette">Block palette</param>
        /// <param name="worldChunksFileStream">World chunks file stream</param>
        /// <param name="worldEntitiesFileStream">World entities file stream</param>
        /// <param name="worldManager">World manager</param>
        private WorldIO(Guid worldGUID, string worldName, string worldDescription, int worldSeed, Vector3Int chunkSize, Dictionary<ChunkID, long> chunkDataLookup, Dictionary<string, long> entityDataLookup, Dictionary<ChunkID, List<string>> chunkEntitiesLookup, BlockObjectScript[] blockPalette, ReopenableFileStream worldChunksFileStream, ReopenableFileStream worldEntitiesFileStream, IWorldManager worldManager)
        {
            WorldGUID = worldGUID;
            WorldName = worldName ?? throw new ArgumentNullException(nameof(worldName));
            WorldDescription = worldDescription ?? throw new ArgumentNullException(nameof(worldDescription));
            WorldSeed = worldSeed;
            ChunkSize = chunkSize;
            this.chunkDataLookup = chunkDataLookup ?? throw new ArgumentNullException(nameof(chunkDataLookup));
            this.entityDataLookup = entityDataLookup ?? throw new ArgumentNullException(nameof(entityDataLookup));
            this.chunkEntitiesLookup = chunkEntitiesLookup ?? throw new ArgumentNullException(nameof(chunkEntitiesLookup));
            this.blockPalette = blockPalette ?? throw new ArgumentNullException(nameof(blockPalette));
            WorldChunksFileStream = worldChunksFileStream ?? throw new ArgumentNullException(nameof(worldChunksFileStream));
            WorldEntitiesFileStream = worldEntitiesFileStream ?? throw new ArgumentNullException(nameof(worldEntitiesFileStream));
            WorldManager = worldManager ?? throw new ArgumentNullException(nameof(worldManager));
        }

        /// <summary>
        /// Create world
        /// </summary>
        /// <param name="worldGUID">World GUID</param>
        /// <param name="worldName">World name</param>
        /// <param name="worldDescription">World description</param>
        /// <param name="worldSeed">World seed</param>
        /// <param name="worldManager">World manager</param>
        /// <returns>World IO if successful, otherwise "null"</returns>
        private static WorldIO Create(Guid worldGUID, string worldName, string worldDescription, int worldSeed, IWorldManager worldManager)
        {
            WorldIO ret = null;
            string world_cache_directory_path = GetWorldCacheDirectoryPath(worldGUID);
            Vector3Int chunk_size = worldManager.ChunkSize;
            ReopenableFileStream cache_world_chunks_file_stream = null;
            ReopenableFileStream cache_world_entities_file_stream = null;
            BlockObjectScript[] block_palette = new BlockObjectScript[worldManager.BlockLookup.Count];
            try
            {
                if (Directory.Exists(world_cache_directory_path))
                {
                    Directory.Delete(world_cache_directory_path, true);
                }
                Directory.CreateDirectory(Path.Combine(world_cache_directory_path, playersDirectoryPath));
                using (FileStream meta_json_file_stream = File.OpenWrite(Path.Combine(world_cache_directory_path, metaJSONFilePath)))
                {
                    using (StreamWriter meta_json_file_stream_writer = new StreamWriter(meta_json_file_stream))
                    {
                        meta_json_file_stream_writer.Write(JsonUtility.ToJson(new WorldMetaData(worldName, worldDescription, worldSeed)));
                    }
                }
                cache_world_chunks_file_stream = ReopenableFileStream.Open(Path.Combine(world_cache_directory_path, worldChunksFilePath), FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                cache_world_entities_file_stream = ReopenableFileStream.Open(Path.Combine(world_cache_directory_path, worldEntitiesFilePath), FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                if ((cache_world_chunks_file_stream != null) && (cache_world_entities_file_stream != null))
                {
                    using (BinaryWriter cache_world_chunks_file_stream_binary_writer = new BinaryWriter(cache_world_chunks_file_stream, Encoding.UTF8, true))
                    {
                        uint count = 0U;
                        cache_world_chunks_file_stream_binary_writer.Write(worldChunksFileSignature);
                        cache_world_chunks_file_stream_binary_writer.Write(chunk_size.x);
                        cache_world_chunks_file_stream_binary_writer.Write(chunk_size.y);
                        cache_world_chunks_file_stream_binary_writer.Write(chunk_size.z);
                        cache_world_chunks_file_stream_binary_writer.Write((uint)(block_palette.Length));
                        cache_world_chunks_file_stream_binary_writer.Write(0U);
                        foreach (KeyValuePair<string, BlockObjectScript> block in worldManager.BlockLookup)
                        {
                            cache_world_chunks_file_stream_binary_writer.Write(block.Key);
                            block_palette[count] = block.Value;
                            ++count;
                        }
                        cache_world_chunks_file_stream_binary_writer.Flush();
                    }
                    using (BinaryWriter cache_world_entities_file_stream_binary_writer = new BinaryWriter(cache_world_entities_file_stream, Encoding.UTF8, true))
                    {
                        cache_world_entities_file_stream_binary_writer.Write(worldEntitiesFileSignature);
                        cache_world_entities_file_stream_binary_writer.Write(0U);
                        cache_world_entities_file_stream_binary_writer.Flush();
                    }
                }
                else
                {
                    if (cache_world_chunks_file_stream != null)
                    {
                        cache_world_chunks_file_stream.Dispose();
                        cache_world_chunks_file_stream = null;
                    }
                    if (cache_world_entities_file_stream != null)
                    {
                        cache_world_entities_file_stream.Dispose();
                        cache_world_entities_file_stream = null;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                if (cache_world_chunks_file_stream != null)
                {
                    cache_world_chunks_file_stream.Dispose();
                    cache_world_chunks_file_stream = null;
                }
                if (cache_world_entities_file_stream != null)
                {
                    cache_world_entities_file_stream.Dispose();
                    cache_world_entities_file_stream = null;
                }
            }
            if ((cache_world_chunks_file_stream != null) && (cache_world_entities_file_stream != null))
            {
                ret = new WorldIO(worldGUID, worldName, worldDescription, worldSeed, chunk_size, new Dictionary<ChunkID, long>(), new Dictionary<string, long>(), new Dictionary<ChunkID, List<string>>(), block_palette, cache_world_chunks_file_stream, cache_world_entities_file_stream, worldManager);
            }
            else
            {
                cache_world_chunks_file_stream?.Dispose();
                cache_world_entities_file_stream?.Dispose();
            }
            return ret;
        }

        /// <summary>
        /// Open world file
        /// </summary>
        /// <param name="worldGUID">World GUID</param>
        /// <param name="worldManager">World manager</param>
        /// <returns>World IO if successful, otherwise "null"</returns>
        private static WorldIO Open(Guid worldGUID, IWorldManager worldManager)
        {
            WorldIO ret = null;
            string world_file_path = GetWorldFilePath(worldGUID);
            string cache_world_directory_path = GetWorldCacheDirectoryPath(worldGUID);
            if (File.Exists(world_file_path))
            {
                ReopenableFileStream cache_world_chunks_file_stream = null;
                ReopenableFileStream cache_world_entities_file_stream = null;
                WorldMetaData world_meta_data = null;
                Vector3Int chunk_size = Vector3Int.zero;
                Dictionary<ChunkID, long> chunk_data_lookup = null;
                Dictionary<string, long> entity_data_lookup = null;
                Dictionary<ChunkID, List<string>> chunk_entities_lookup = null;
                BlockObjectScript[] block_palette = Array.Empty<BlockObjectScript>();
                try
                {
                    if (Directory.Exists(cache_world_directory_path))
                    {
                        Directory.Delete(cache_world_directory_path, true);
                    }
                    Directory.CreateDirectory(cache_world_directory_path);
                    using (FileStream world_file_stream = File.OpenRead(world_file_path))
                    {
                        if (world_file_stream != null)
                        {
                            using (ZipFile zip_file = new ZipFile(world_file_stream))
                            {
                                foreach (ZipEntry zip_file_entry in zip_file)
                                {
                                    if (zip_file_entry.IsFile)
                                    {
                                        string destination_file_path = Path.Combine(cache_world_directory_path, zip_file_entry.Name);
                                        string destination_directory_path = Path.GetDirectoryName(Path.Combine(cache_world_directory_path, zip_file_entry.Name));
                                        if (!(Directory.Exists(destination_directory_path)))
                                        {
                                            Directory.CreateDirectory(destination_directory_path);
                                        }
                                        if (File.Exists(destination_file_path))
                                        {
                                            File.Delete(destination_file_path);
                                        }
                                        using (Stream input_stream = zip_file.GetInputStream(zip_file_entry))
                                        {
                                            if (input_stream != null)
                                            {
                                                using (FileStream destination_file_stream = File.OpenWrite(destination_file_path))
                                                {
                                                    if (destination_file_stream != null)
                                                    {
                                                        input_stream.CopyTo(destination_file_stream);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    string cache_meta_json_file_path = Path.Combine(cache_world_directory_path, metaJSONFilePath);
                    string cache_world_chunks_file_path = Path.Combine(cache_world_directory_path, worldChunksFilePath);
                    string cache_world_entities_file_path = Path.Combine(cache_world_directory_path, worldEntitiesFilePath);
                    if (File.Exists(cache_meta_json_file_path) && File.Exists(cache_world_chunks_file_path) && File.Exists(cache_world_entities_file_path))
                    {
                        using (FileStream meta_json_file_stream = File.OpenRead(cache_meta_json_file_path))
                        {
                            using (StreamReader meta_json_file_stream_reader = new StreamReader(meta_json_file_stream))
                            {
                                world_meta_data = JsonUtility.FromJson<WorldMetaData>(meta_json_file_stream_reader.ReadToEnd());
                            }
                        }
                    }
                    if (world_meta_data != null)
                    {
                        cache_world_chunks_file_stream = ReopenableFileStream.Open(Path.Combine(cache_world_directory_path, worldChunksFilePath), FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                        cache_world_entities_file_stream = ReopenableFileStream.Open(Path.Combine(cache_world_directory_path, worldEntitiesFilePath), FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                        if ((cache_world_chunks_file_stream != null) && (cache_world_entities_file_stream != null))
                        {
                            cache_world_chunks_file_stream.Seek(0L, SeekOrigin.Begin);
                            cache_world_entities_file_stream.Seek(0L, SeekOrigin.Begin);
                            using (BinaryReader cache_world_chunks_file_stream_binary_reader = new BinaryReader(cache_world_chunks_file_stream, Encoding.UTF8, true))
                            {
                                long signature = cache_world_chunks_file_stream_binary_reader.ReadInt64();
                                if (signature != worldChunksFileSignature)
                                {
                                    throw new InvalidDataException("Signature does not contain \"AkashaC0\".");
                                }
                                int chunk_width = cache_world_chunks_file_stream_binary_reader.ReadInt32();
                                int chunk_height = cache_world_chunks_file_stream_binary_reader.ReadInt32();
                                int chunk_depth = cache_world_chunks_file_stream_binary_reader.ReadInt32();
                                if ((chunk_width <= 0) || (chunk_height <= 0) || (chunk_depth <= 0))
                                {
                                    throw new InvalidDataException($"Invalid chunk size: ({ chunk_width }, { chunk_height }, { chunk_depth }).");
                                }
                                chunk_size = new Vector3Int(chunk_width, chunk_height, chunk_depth);
                                if (worldManager.ChunkSize != chunk_size)
                                {
                                    throw new InvalidDataException($"Incompatible chunk size. Read: { chunk_size }; Expected: { worldManager.ChunkSize }");
                                }
                                long chunk_data_size = (long)(chunk_width * chunk_height * chunk_depth) * (sizeof(uint) + sizeof(ushort));
                                uint blocks_in_palette_count = cache_world_chunks_file_stream_binary_reader.ReadUInt32();
                                uint chunk_count = cache_world_chunks_file_stream_binary_reader.ReadUInt32();
                                block_palette = new BlockObjectScript[blocks_in_palette_count];
                                for (int index = 0; index < block_palette.Length; index++)
                                {
                                    string key = cache_world_chunks_file_stream_binary_reader.ReadString();
                                    if (worldManager.BlockLookup.ContainsKey(key))
                                    {
                                        block_palette[index] = worldManager.BlockLookup[key];
                                    }
                                    else
                                    {
                                        Debug.LogError($"Invalid block { key } in block palette.");
                                    }
                                }
                                chunk_data_lookup = new Dictionary<ChunkID, long>();
                                for (uint index = 0U; index != chunk_count; index++)
                                {
                                    int chunk_id_x = cache_world_chunks_file_stream_binary_reader.ReadInt32();
                                    int chunk_id_y = cache_world_chunks_file_stream_binary_reader.ReadInt32();
                                    int chunk_id_z = cache_world_chunks_file_stream_binary_reader.ReadInt32();
                                    ChunkID chunk_id = new ChunkID(chunk_id_x, chunk_id_y, chunk_id_z);
                                    if (chunk_data_lookup.ContainsKey(chunk_id))
                                    {
                                        Debug.LogError($"Skipping duplicate chunk ID entry { chunk_id }...");
                                    }
                                    else
                                    {
                                        chunk_data_lookup.Add(chunk_id, cache_world_chunks_file_stream.Position);
                                    }
                                    cache_world_chunks_file_stream.Seek(chunk_data_size, SeekOrigin.Current);
                                }
                            }
                            using (BinaryReader cache_world_entities_file_stream_binary_reader = new BinaryReader(cache_world_entities_file_stream, Encoding.UTF8, true))
                            {
                                long signature = cache_world_entities_file_stream_binary_reader.ReadInt64();
                                if (signature != worldEntitiesFileSignature)
                                {
                                    throw new InvalidDataException("Signature does not contain \"AkashaE0\".");
                                }
                                uint entity_count = cache_world_entities_file_stream_binary_reader.ReadUInt32();
                                entity_data_lookup = new Dictionary<string, long>();
                                chunk_entities_lookup = new Dictionary<ChunkID, List<string>>();
                                for (uint index = 0U; index != entity_count; index++)
                                {
                                    long entity_data_position = cache_world_chunks_file_stream.Position;
                                    string entity_json = cache_world_entities_file_stream_binary_reader.ReadString();
                                    WorldEntityData entity = JsonUtility.FromJson<WorldEntityData>(entity_json);
                                    if ((entity != null) && entity.IsValid)
                                    {
                                        string key = entity.GUID.ToString();
                                        if (entity_data_lookup.ContainsKey(key))
                                        {
                                            Debug.LogError($"Skipping duplicate entity with GUID \"{ key }\"...");
                                        }
                                        else
                                        {
                                            entity_data_lookup.Add(key, entity_data_position);
                                            ChunkID chunk_id = worldManager.GetChunkIDFromBlockID(entity.BlockID);
                                            List<string> entity_list;
                                            if (chunk_entities_lookup.ContainsKey(chunk_id))
                                            {
                                                entity_list = chunk_entities_lookup[chunk_id];
                                            }
                                            else
                                            {
                                                entity_list = new List<string>();
                                                chunk_entities_lookup.Add(chunk_id, entity_list);
                                            }
                                            entity_list.Add(key);
                                        }
                                    }
                                    else
                                    {
                                        Debug.LogError($"Invalid entity data entry at file position { entity_data_position }.{ Environment.NewLine }Data:{ entity_json }");
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (cache_world_chunks_file_stream != null)
                            {
                                cache_world_chunks_file_stream.Dispose();
                                cache_world_chunks_file_stream = null;
                            }
                            if (cache_world_entities_file_stream != null)
                            {
                                cache_world_entities_file_stream.Dispose();
                                cache_world_entities_file_stream = null;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
                if ((world_meta_data != null) && (cache_world_chunks_file_stream != null) && (cache_world_entities_file_stream != null))
                {
                    ret = new WorldIO(worldGUID, world_meta_data.WorldName, world_meta_data.WorldDescription, world_meta_data.WorldSeed, chunk_size, chunk_data_lookup, entity_data_lookup, chunk_entities_lookup, block_palette, cache_world_chunks_file_stream, cache_world_entities_file_stream, worldManager);
                }
                else
                {
                    cache_world_chunks_file_stream?.Dispose();
                    cache_world_entities_file_stream?.Dispose();
                }
                if ((ret == null) && (cache_world_chunks_file_stream != null))
                {
                    cache_world_chunks_file_stream.Dispose();
                    try
                    {
                        File.Delete(cache_world_directory_path);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Read world preview
        /// </summary>
        /// <param name="worldGUID">World GUID</param>
        /// <returns>Valid world preview if success, otherwise an invalid world preview</returns>
        private static WorldPreview ReadWorldPreview(Guid worldGUID)
        {
            byte[] preview_image_data = null;
            WorldMetaData meta_data = null;
            try
            {
                string world_file_path = GetWorldFilePath(worldGUID);
                if (File.Exists(world_file_path))
                {
                    using (FileStream world_file_stream = File.OpenRead(world_file_path))
                    {
                        if (world_file_stream != null)
                        {
                            using (ZipFile zip_file = new ZipFile(world_file_stream))
                            {
                                ZipEntry preview_png_zip_file_entry = zip_file.GetEntry(previewPNGFilePath);
                                ZipEntry meta_json_zip_file_entry = zip_file.GetEntry(metaJSONFilePath);
                                if (meta_json_zip_file_entry != null)
                                {
                                    using (Stream meta_json_zip_file_entry_input_stream = zip_file.GetInputStream(meta_json_zip_file_entry))
                                    {
                                        if (meta_json_zip_file_entry_input_stream != null)
                                        {
                                            using (StreamReader meta_json_zip_file_entry_input_stream_reader = new StreamReader(meta_json_zip_file_entry_input_stream))
                                            {
                                                meta_data = JsonUtility.FromJson<WorldMetaData>(meta_json_zip_file_entry_input_stream_reader.ReadToEnd());
                                            }
                                        }
                                    }
                                }
                                if (preview_png_zip_file_entry != null)
                                {
                                    using (Stream preview_png_zip_file_entry_input_stream = zip_file.GetInputStream(preview_png_zip_file_entry))
                                    {
                                        if (preview_png_zip_file_entry_input_stream != null)
                                        {
                                            preview_image_data = new byte[preview_png_zip_file_entry_input_stream.Length];
                                            preview_png_zip_file_entry_input_stream.Read(preview_image_data, 0, preview_image_data.Length);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            return new WorldPreview(preview_image_data, meta_data);
        }

        /// <summary>
        /// Clear cache
        /// </summary>
        private static void ClearCache()
        {
            try
            {
                string[] directory_paths = Directory.GetDirectories(WorldsCacheDirectoryPath);
                if (directory_paths != null)
                {
                    foreach (string directory_path in directory_paths)
                    {
                        try
                        {
                            if (Directory.Exists(directory_path))
                            {
                                Directory.Delete(directory_path, true);
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError(ex);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        /// <summary>
        /// Create read world preview task
        /// </summary>
        /// <param name="worldGUID">World GUID</param>
        /// <returns>Valid world preview task if success, otherwise an invalid world preview task</returns>
        public static Task<WorldPreview> CreateReadWorldPreviewTask(Guid worldGUID) => Task.Run(() => ReadWorldPreview(worldGUID));

        /// <summary>
        /// Get world file path
        /// </summary>
        /// <param name="worldGUID">World GUID</param>
        /// <returns>World file path</returns>
        public static string GetWorldFilePath(Guid worldGUID)
        {
            if (worldGUID == null)
            {
                throw new ArgumentNullException(nameof(worldGUID));
            }
            return Path.Combine(WorldsDirectoryPath, $"{ worldGUID }.world");
        }

        /// <summary>
        /// Get world cache directory path
        /// </summary>
        /// <param name="worldGUID">World GUID</param>
        /// <returns>World file path</returns>
        public static string GetWorldCacheDirectoryPath(Guid worldGUID)
        {
            if (worldGUID == null)
            {
                throw new ArgumentNullException(nameof(worldGUID));
            }
            return Path.Combine(WorldsCacheDirectoryPath, worldGUID.ToString());
        }

        /// <summary>
        /// Does world file exist
        /// </summary>
        /// <param name="worldGUID">World GUID</param>
        /// <returns>"true" if world file exists, otherwise "false"</returns>
        public static bool DoesWorldFileExist(Guid worldGUID)
        {
            if (worldGUID == null)
            {
                throw new ArgumentNullException(nameof(worldGUID));
            }
            return File.Exists(GetWorldFilePath(worldGUID));
        }

        /// <summary>
        /// Does world cache file exist
        /// </summary>
        /// <param name="worldGUID">World GUID</param>
        /// <returns>"true" if world file exists, otherwise "false"</returns>
        public static bool DoesWorldCacheFileExist(Guid worldGUID)
        {
            if (worldGUID == null)
            {
                throw new ArgumentNullException(nameof(worldGUID));
            }
            return File.Exists(GetWorldCacheDirectoryPath(worldGUID));
        }

        /// <summary>
        /// Create create world task
        /// </summary>
        /// <param name="worldGUID">World GUID</param>
        /// <param name="worldName">World name</param>
        /// <param name="worldDescription">World description</param>
        /// <param name="worldSeed">World seed</param>
        /// <param name="worldManager">World manager</param>
        /// <returns>World IO task if successful, otherwise "null" task</returns>
        public static Task<WorldIO> CreateCreateWorldTask(Guid worldGUID, string worldName, string worldDescription, int worldSeed, IWorldManager worldManager)
        {
            if (worldName == null)
            {
                throw new ArgumentNullException(nameof(worldName));
            }
            if (worldManager == null)
            {
                throw new ArgumentNullException(nameof(worldManager));
            }
            return Task.Run(() => Create(worldGUID, worldName, worldDescription, worldSeed, worldManager));
        }

        /// <summary>
        /// Create open world file task
        /// </summary>
        /// <param name="worldGUID">World GUID</param>
        /// <param name="worldManager">World manager</param>
        /// <returns>World IO if successful, otherwise "null"</returns>
        public static Task<WorldIO> CreateOpenWorldFileTask(Guid worldGUID, IWorldManager worldManager)
        {
            if (worldManager == null)
            {
                throw new ArgumentNullException(nameof(worldManager));
            }
            return Task.Run(() => Open(worldGUID, worldManager));
        }

        /// <summary>
        /// Delete world file
        /// </summary>
        /// <param name="worldGUID">World GUID</param>
        /// <returns>"true" if successful, otherwise "false"</returns>
        public static bool DeleteWorldFile(Guid worldGUID)
        {
            bool ret = false;
            try
            {
                string world_file_path = GetWorldFilePath(worldGUID);
                if (File.Exists(world_file_path))
                {
                    File.Delete(world_file_path);
                    ret = true;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Create clear cache task
        /// </summary>
        /// <returns>Delete cache task</returns>
        public static Task CreateClearCacheTask() => Task.Run(ClearCache);

        /// <summary>
        /// Read chunk block data
        /// </summary>
        /// <param name="chunkID">Chunk ID</param>
        /// <returns>Blocks</returns>
        private BlockData[] ReadChunkBlockData(ChunkID chunkID)
        {
            BlockData[] ret = Array.Empty<BlockData>();
            lock (WorldChunksFileStream)
            {
                if (chunkDataLookup.ContainsKey(chunkID))
                {
                    try
                    {
                        WorldChunksFileStream.Seek(chunkDataLookup[chunkID], SeekOrigin.Begin);
                        using (BinaryReader world_chunks_file_stream_binary_reader = new BinaryReader(WorldChunksFileStream, Encoding.UTF8, true))
                        {
                            ret = new BlockData[ChunkSize.x * ChunkSize.y * ChunkSize.z];
                            for (int index = 0; index < ret.Length; index++)
                            {
                                uint block_palette_index = world_chunks_file_stream_binary_reader.ReadUInt32();
                                ushort health = world_chunks_file_stream_binary_reader.ReadUInt16();
                                ret[index] = ((block_palette_index < blockPalette.Length) ? (new BlockData(blockPalette[block_palette_index], health)) : default);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Read chunk entity data
        /// </summary>
        /// <param name="chunkID">Chunk ID</param>
        /// <returns>Entities</returns>
        private WorldEntityData[] ReadChunkEntityData(ChunkID chunkID)
        {
            WorldEntityData[] ret = Array.Empty<WorldEntityData>();
            lock (WorldEntitiesFileStream)
            {
                if (chunkEntitiesLookup.ContainsKey(chunkID))
                {
                    List<string> chunk_entity_guids = chunkEntitiesLookup[chunkID];
                    ret = new WorldEntityData[chunk_entity_guids.Count];
                    using (BinaryReader world_entities_file_stream_binary_reader = new BinaryReader(WorldEntitiesFileStream, Encoding.UTF8, true))
                    {
                        for (int index = 0; index < ret.Length; index++)
                        {
                            string chunk_entity_guid = chunk_entity_guids[index];
                            if (entityDataLookup.ContainsKey(chunk_entity_guid))
                            {
                                long entity_data_position = entityDataLookup[chunk_entity_guid];
                                WorldEntitiesFileStream.Seek(entity_data_position, SeekOrigin.Begin);
                                string entity_json = world_entities_file_stream_binary_reader.ReadString();
                                WorldEntityData entity = JsonUtility.FromJson<WorldEntityData>(entity_json);
                                if ((entity != null) && entity.IsValid)
                                {
                                    ret[index] = entity;
                                }
                                else
                                {
                                    Debug.LogError($"Invalid entity data entry at file position { entity_data_position }.{ Environment.NewLine }Data:{ entity_json }");
                                }
                            }
                        }
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Read players
        /// </summary>
        /// <returns>Players</returns>
        private IReadOnlyDictionary<string, WorldPlayerData> ReadPlayers()
        {
            Dictionary<string, WorldPlayerData> ret = new Dictionary<string, WorldPlayerData>();
            string players_directory_path = Path.Combine(GetWorldCacheDirectoryPath(WorldGUID), "players");
            try
            {
                if (Directory.Exists(players_directory_path))
                {
                    string[] player_paths = Directory.GetFiles(players_directory_path, "*.json");
                    if (player_paths != null)
                    {
                        foreach (string player_path in player_paths)
                        {
                            try
                            {
                                using (FileStream player_file_stream = File.OpenRead(player_path))
                                {
                                    using (StreamReader player_file_stream_reader = new StreamReader(player_file_stream))
                                    {
                                        string player_json = player_file_stream_reader.ReadToEnd();
                                        WorldPlayerData player = JsonUtility.FromJson<WorldPlayerData>(player_json);
                                        if (player != null)
                                        {
                                            string key = player.GUID.ToString();
                                            if (ret.ContainsKey(key))
                                            {
                                                Debug.LogError($"Skipping duplicate player GUID \"{ key }\".");
                                            }
                                            else
                                            {
                                                ret.Add(key, player);
                                            }
                                        }
                                        else
                                        {
                                            Debug.LogError($"Invalid player JSON:{ Environment.NewLine }{ player_json }");
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.LogError(ex);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            return ret;
        }

        /// <summary>
        /// Write chunks
        /// </summary>
        /// <param name="chunks">Chunks</param>
        /// <param name="deleteChunks">Delete chunks</param>
        /// <returns>"true" if successful, otherwise "false"</returns>
        private bool WriteChunks(IEnumerable<ChunkData> chunks, IEnumerable<ChunkID> deleteChunks)
        {
            bool ret = false;
            lock (WorldChunksFileStream)
            {
                string temporary_world_chunks_file_path = Path.Combine(GetWorldCacheDirectoryPath(WorldGUID), worldChunksTemporaryFilePath);
                try
                {
                    if (File.Exists(temporary_world_chunks_file_path))
                    {
                        File.Delete(temporary_world_chunks_file_path);
                    }
                    using (FileStream temporary_world_chunks_file_stream = File.Open(temporary_world_chunks_file_path, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                    {
                        if (temporary_world_chunks_file_stream != null)
                        {
                            WorldChunksFileStream.Seek(0L, SeekOrigin.Begin);
                            WorldChunksFileStream.CopyTo(temporary_world_chunks_file_stream);
                            temporary_world_chunks_file_stream.Seek(0L, SeekOrigin.Begin);
                            WorldChunksFileStream.SetLength(0L);
                            using (BinaryReader temporary_world_chunks_file_stream_binary_reader = new BinaryReader(temporary_world_chunks_file_stream, Encoding.UTF8, true))
                            {
                                using (BinaryWriter world_chunks_stream_binary_writer = new BinaryWriter(WorldChunksFileStream, Encoding.UTF8, true))
                                {
                                    Dictionary<ChunkID, long> write_chunk_data_lookup = new Dictionary<ChunkID, long>(chunkDataLookup);
                                    Dictionary<ChunkID, ChunkData> append_chunks_lookup = new Dictionary<ChunkID, ChunkData>();
                                    BlockObjectScript[] write_block_palette = new BlockObjectScript[WorldManager.BlockLookup.Count];
                                    Dictionary<uint, uint> old_to_new_block_palette_index_lookup = new Dictionary<uint, uint>();
                                    int chunk_blocks_size = ChunkSize.x * ChunkSize.y * ChunkSize.z;
                                    uint count = 0U;
                                    foreach (ChunkData chunk in chunks)
                                    {
                                        if (chunk.IsValid)
                                        {
                                            if (!(write_chunk_data_lookup.ContainsKey(chunk.ChunkID)))
                                            {
                                                write_chunk_data_lookup.Add(chunk.ChunkID, 0L);
                                            }
                                            append_chunks_lookup.Add(chunk.ChunkID, chunk);
                                        }
                                    }
                                    foreach (ChunkID delete_chunk in deleteChunks)
                                    {
                                        write_chunk_data_lookup.Remove(delete_chunk);
                                        append_chunks_lookup.Remove(delete_chunk);
                                    }
                                    Dictionary<ChunkID, long> new_write_chunk_data_lookup = new Dictionary<ChunkID, long>(write_chunk_data_lookup);
                                    foreach (BlockObjectScript block in WorldManager.BlockLookup.Values)
                                    {
                                        write_block_palette[count] = block;
                                        for (int index = 0; index < blockPalette.Length; index++)
                                        {
                                            BlockObjectScript block_palette_entry = blockPalette[index];
                                            if (block_palette_entry == block)
                                            {
                                                old_to_new_block_palette_index_lookup.Add(count, (uint)index);
                                                break;
                                            }
                                        }
                                        ++count;
                                    }
                                    world_chunks_stream_binary_writer.Write(worldChunksFileSignature);
                                    world_chunks_stream_binary_writer.Write(ChunkSize.x);
                                    world_chunks_stream_binary_writer.Write(ChunkSize.y);
                                    world_chunks_stream_binary_writer.Write(ChunkSize.z);
                                    world_chunks_stream_binary_writer.Write((uint)(write_block_palette.Length));
                                    world_chunks_stream_binary_writer.Write((uint)(write_chunk_data_lookup.Count));
                                    foreach (string key in WorldManager.BlockLookup.Keys)
                                    {
                                        world_chunks_stream_binary_writer.Write(key);
                                    }
                                    foreach (KeyValuePair<ChunkID, long> write_chunk_data in write_chunk_data_lookup)
                                    {
                                        world_chunks_stream_binary_writer.Write(write_chunk_data.Key.X);
                                        world_chunks_stream_binary_writer.Write(write_chunk_data.Key.Y);
                                        world_chunks_stream_binary_writer.Write(write_chunk_data.Key.Z);
                                        world_chunks_stream_binary_writer.Flush();
                                        new_write_chunk_data_lookup[write_chunk_data.Key] = WorldChunksFileStream.Position;
                                        if (append_chunks_lookup.ContainsKey(write_chunk_data.Key))
                                        {
                                            ChunkData chunk = append_chunks_lookup[write_chunk_data.Key];
                                            foreach (BlockData block in chunk.Blocks)
                                            {
                                                uint block_palette_index = uint.MaxValue;
                                                ushort block_health = 0;
                                                if (block.IsABlock)
                                                {
                                                    for (int index = 0; index < write_block_palette.Length; index++)
                                                    {
                                                        BlockObjectScript block_palette_entry = write_block_palette[index];
                                                        if (block_palette_entry == block.Block)
                                                        {
                                                            block_palette_index = (uint)index;
                                                            break;
                                                        }
                                                    }
                                                    block_health = block.Health;
                                                }
                                                world_chunks_stream_binary_writer.Write(block_palette_index);
                                                world_chunks_stream_binary_writer.Write(block_health);
                                            }
                                        }
                                        else
                                        {
                                            temporary_world_chunks_file_stream.Seek(write_chunk_data.Value, SeekOrigin.Begin);
                                            world_chunks_stream_binary_writer.Flush();
                                            new_write_chunk_data_lookup[write_chunk_data.Key] = WorldChunksFileStream.Position;
                                            for (int index = 0; index < chunk_blocks_size; index++)
                                            {
                                                uint old_block_palette_index = temporary_world_chunks_file_stream_binary_reader.ReadUInt32();
                                                ushort block_health = temporary_world_chunks_file_stream_binary_reader.ReadUInt16();
                                                world_chunks_stream_binary_writer.Write(old_to_new_block_palette_index_lookup.ContainsKey(old_block_palette_index) ? old_to_new_block_palette_index_lookup[old_block_palette_index] : uint.MaxValue);
                                                world_chunks_stream_binary_writer.Write(block_health);
                                            }
                                        }
                                    }
                                    world_chunks_stream_binary_writer.Flush();
                                    write_chunk_data_lookup.Clear();
                                    // TEST
                                    //chunkDataLookup.Clear();
                                    chunkDataLookup = new_write_chunk_data_lookup;
                                    append_chunks_lookup.Clear();
                                    blockPalette = write_block_palette;
                                    old_to_new_block_palette_index_lookup.Clear();
                                    ret = true;
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
                try
                {
                    if (File.Exists(temporary_world_chunks_file_path))
                    {
                        File.Delete(temporary_world_chunks_file_path);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
            return ret;
        }

        /// <summary>
        /// Write entities
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <param name="deleteEntities">Delete entities</param>
        /// <returns>"true" if successful, otherwise "false"</returns>
        private bool WriteEntities(IEnumerable<WorldEntityData> entities, IEnumerable<string> deleteEntities)
        {
            bool ret = false;
            lock (WorldEntitiesFileStream)
            {
                string temporary_world_entities_file_path = Path.Combine(GetWorldCacheDirectoryPath(WorldGUID), worldEntitiesTemporaryFilePath);
                try
                {
                    if (File.Exists(temporary_world_entities_file_path))
                    {
                        File.Delete(temporary_world_entities_file_path);
                    }
                    using (FileStream temporary_world_entities_file_stream = File.Open(temporary_world_entities_file_path, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                    {
                        if (temporary_world_entities_file_stream != null)
                        {
                            WorldEntitiesFileStream.Seek(0L, SeekOrigin.Begin);
                            WorldEntitiesFileStream.CopyTo(temporary_world_entities_file_stream);
                            temporary_world_entities_file_stream.Seek(0L, SeekOrigin.Begin);
                            WorldEntitiesFileStream.SetLength(0L);
                            using (BinaryReader temporary_world_entities_file_stream_binary_reader = new BinaryReader(temporary_world_entities_file_stream, Encoding.UTF8, true))
                            {
                                using (BinaryWriter world_entities_stream_binary_writer = new BinaryWriter(WorldEntitiesFileStream, Encoding.UTF8, true))
                                {
                                    Dictionary<string, long> write_entity_data_lookup = new Dictionary<string, long>(entityDataLookup);
                                    Dictionary<string, WorldEntityData> append_entities_lookup = new Dictionary<string, WorldEntityData>();
                                    foreach (WorldEntityData entity in entities)
                                    {
                                        if (entity.IsValid)
                                        {
                                            string key = entity.GUID.ToString();
                                            if (!(write_entity_data_lookup.ContainsKey(key)))
                                            {
                                                write_entity_data_lookup.Add(key, 0L);
                                            }
                                            append_entities_lookup.Add(key, entity);
                                        }
                                    }
                                    foreach (string delete_entity in deleteEntities)
                                    {
                                        write_entity_data_lookup.Remove(delete_entity);
                                        append_entities_lookup.Remove(delete_entity);
                                    }
                                    Dictionary<string, long> new_write_entity_data_lookup = new Dictionary<string, long>(write_entity_data_lookup);
                                    world_entities_stream_binary_writer.Write(worldEntitiesFileSignature);
                                    world_entities_stream_binary_writer.Write((uint)(write_entity_data_lookup.Count));
                                    foreach (KeyValuePair<string, long> write_entity_data in write_entity_data_lookup)
                                    {
                                        world_entities_stream_binary_writer.Write(write_entity_data.Key);
                                        world_entities_stream_binary_writer.Flush();
                                        new_write_entity_data_lookup[write_entity_data.Key] = WorldEntitiesFileStream.Position;
                                        if (append_entities_lookup.ContainsKey(write_entity_data.Key))
                                        {
                                            world_entities_stream_binary_writer.Write(JsonUtility.ToJson(append_entities_lookup[write_entity_data.Key]));
                                        }
                                        else
                                        {
                                            temporary_world_entities_file_stream.Seek(write_entity_data.Value, SeekOrigin.Begin);
                                            string entity_json = temporary_world_entities_file_stream_binary_reader.ReadString();
                                            WorldEntityData entitiy = JsonUtility.FromJson<WorldEntityData>(entity_json);
                                            if ((entitiy != null) && entitiy.IsValid)
                                            {
                                                world_entities_stream_binary_writer.Write(entity_json);
                                            }
                                            else
                                            {
                                                Debug.LogError($"Invalid entity data entry at file position { write_entity_data.Value }.{ Environment.NewLine }Data:{ entity_json }");
                                            }
                                        }
                                    }
                                    world_entities_stream_binary_writer.Flush();
                                    write_entity_data_lookup.Clear();
                                    // TEST
                                    //entityDataLookup.Clear();
                                    entityDataLookup = new_write_entity_data_lookup;
                                    append_entities_lookup.Clear();
                                    ret = true;
                                    // TODO: Rebuild chunk entity lookup dictionary
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
                try
                {
                    if (File.Exists(temporary_world_entities_file_path))
                    {
                        File.Delete(temporary_world_entities_file_path);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
            return ret;
        }

        /// <summary>
        /// Write players
        /// </summary>
        /// <param name="players">Players</param>
        /// <returns></returns>
        private bool WritePlayers(IEnumerable<WorldPlayerData> players)
        {
            bool ret = true;
            try
            {
                string world_cache_directory_path = GetWorldCacheDirectoryPath(WorldGUID);
                foreach (WorldPlayerData player in players)
                {
                    string player_json_file_path = Path.Combine(Path.Combine(world_cache_directory_path, playersDirectoryPath), $"{ player.GUID }.json");
                    if (File.Exists(player_json_file_path))
                    {
                        File.Delete(player_json_file_path);
                    }
                    using (FileStream player_json_file_stream = File.Open(player_json_file_path, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                    {
                        using (StreamWriter player_json_file_stream_writer = new StreamWriter(player_json_file_stream))
                        {
                            player_json_file_stream_writer.Write(JsonUtility.ToJson(player));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Save world
        /// </summary>
        /// <returns>"true" if successful, otherwise "false"</returns>
        private bool Save()
        {
            bool ret = false;
            lock (WorldChunksFileStream)
            {
                lock (WorldEntitiesFileStream)
                {
                    try
                    {
                        WorldChunksFileStream.Close();
                        WorldEntitiesFileStream.Close();
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }
                    try
                    {
                        string worlds_directory_path = WorldsDirectoryPath;
                        string world_file_path = GetWorldFilePath(WorldGUID);
                        if (!(Directory.Exists(worlds_directory_path)))
                        {
                            Directory.CreateDirectory(worlds_directory_path);
                        }
                        if (File.Exists(world_file_path))
                        {
                            File.Delete(world_file_path);
                        }
                        using (ZipFile world_zip_file = ZipFile.Create(world_file_path))
                        {
                            string world_cache_directory_path = GetWorldCacheDirectoryPath(WorldGUID);
                            string[] cache_file_paths = Directory.GetFiles(world_cache_directory_path, "*", SearchOption.AllDirectories);
                            world_zip_file.BeginUpdate();
                            foreach (string cache_file_path in cache_file_paths)
                            {
                                if (!(cache_file_path.EndsWith(".temp")))
                                {
                                    world_zip_file.Add(cache_file_path, ZipEntry.CleanName((cache_file_path.StartsWith(world_cache_directory_path)) ? cache_file_path.Substring(world_cache_directory_path.Length) : cache_file_path));
                                }
                            }
                            world_zip_file.CommitUpdate();
                            ret = true;
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }
                    try
                    {
                        WorldChunksFileStream.Reopen();
                        WorldEntitiesFileStream.Reopen();
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                        throw new AggregateException("Cache file streams can't be reopened");
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Is chunk available
        /// </summary>
        /// <param name="chunkID">Chunk ID</param>
        /// <returns>"true" if chunk is available, otherwise "false"</returns>
        public bool IsChunkAvailable(ChunkID chunkID) => chunkDataLookup.ContainsKey(chunkID);

        /// <summary>
        /// Create read chunk blocks task
        /// </summary>
        /// <param name="chunkID">Chunk ID</param>
        /// <returns>Read chunk task</returns>
        public Task<BlockData[]> CreateReadChunkBlocksTask(ChunkID chunkID) => (CanRead ? Task.Run(() => ReadChunkBlockData(chunkID)) : Task.FromResult(Array.Empty<BlockData>()));

        /// <summary>
        /// Create read chunk entities task
        /// </summary>
        /// <param name="chunkID">Chunk ID</param>
        /// <returns>Read chunk entities task</returns>
        public Task<WorldEntityData[]> CreateReadChunkEntitiesTask(ChunkID chunkID) => (CanRead ? Task.Run(() => ReadChunkEntityData(chunkID)) : Task.FromResult(Array.Empty<WorldEntityData>()));

        /// <summary>
        /// Create read players task
        /// </summary>
        /// <returns>Read players task</returns>
        public Task<IReadOnlyDictionary<string, WorldPlayerData>> CreateReadPlayersTask() => Task.Run(ReadPlayers);

        /// <summary>
        /// Create write chunks task
        /// </summary>
        /// <param name="chunks">Chunks</param>
        /// <param name="deleteChunks">Delete chunks</param>
        /// <returns>Result task contains "true" if successful, otherwise "false"</returns>
        public Task<bool> CreateWriteChunksTask(IEnumerable<ChunkData> chunks, IEnumerable<ChunkID> deleteChunks)
        {
            if (chunks == null)
            {
                throw new ArgumentNullException(nameof(chunks));
            }
            foreach (ChunkData chunk in chunks)
            {
                if (!(chunk.IsValid))
                {
                    throw new ArgumentException("Chunk is invalid.", nameof(chunk));
                }
            }
            if (deleteChunks == null)
            {
                throw new ArgumentNullException(nameof(deleteChunks));
            }
            return ((CanRead && CanWrite) ? Task.Run(() => WriteChunks(chunks, deleteChunks)) : Task.FromResult(false));
        }

        /// <summary>
        /// Create write entities task
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <param name="deleteEntities">Delete entities</param>
        /// <returns>Result task contains "true" if successful, otherwise "false"</returns>
        public Task<bool> CreateWriteEntitiesTask(IEnumerable<WorldEntityData> entities, IEnumerable<string> deleteEntities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            foreach (WorldEntityData entity in entities)
            {
                if (!(entity.IsValid))
                {
                    throw new ArgumentException("Entity is invalid.", nameof(entity));
                }
            }
            if (deleteEntities == null)
            {
                throw new ArgumentNullException(nameof(deleteEntities));
            }
            return ((CanRead && CanWrite) ? Task.Run(() => WriteEntities(entities, deleteEntities)) : Task.FromResult(false));
        }

        /// <summary>
        /// Create write players task
        /// </summary>
        /// <param name="players">Players</param>
        /// <returns>Result task contains "true" if successful, otherwise "false"</returns>
        public Task<bool> CreateWritePlayersTask(IEnumerable<WorldPlayerData> players)
        {
            try
            {
                if (players == null)
                {
                    throw new ArgumentNullException(nameof(players));
                }
                foreach (WorldPlayerData player in players)
                {
                    if (player == null)
                    {
                        throw new ArgumentNullException(nameof(player));
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            return Task.Run(() => WritePlayers(players));
        }

        /// <summary>
        /// Create save task
        /// </summary>
        /// <returns>Result task contains "true" if successful, otherwise "false"</returns>
        public Task<bool> CreateSaveTask() => (CanRead ? Task.Run(Save) : Task.FromResult(false));

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (WorldChunksFileStream != null)
            {
                lock (WorldChunksFileStream)
                {
                    try
                    {
                        WorldChunksFileStream.Dispose();
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e);
                    }
                }
            }
            if (WorldEntitiesFileStream != null)
            {
                lock (WorldEntitiesFileStream)
                {
                    try
                    {
                        WorldEntitiesFileStream.Dispose();
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e);
                    }
                }
            }
            lock (chunkDataLookup)
            {
                chunkDataLookup.Clear();
            }
            lock (entityDataLookup)
            {
                entityDataLookup.Clear();
            }
            lock (chunkEntitiesLookup)
            {
                chunkEntitiesLookup.Clear();
            }
            blockPalette = Array.Empty<BlockObjectScript>();
            try
            {
                string cache_directory_path = GetWorldCacheDirectoryPath(WorldGUID);
                if (File.Exists(cache_directory_path))
                {
                    Directory.Delete(cache_directory_path, true);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}

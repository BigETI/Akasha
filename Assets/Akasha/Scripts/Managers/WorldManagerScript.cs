using Akasha.Controllers;
using Akasha.Data;
using LibNoise;
using LibNoise.Generator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Akasha managers namespace
/// </summary>
namespace Akasha.Managers
{
    /// <summary>
    /// World manager script class
    /// </summary>
    public class WorldManagerScript : AManagerScript<WorldManagerScript>, IWorldManager
    {
        /// <summary>
        /// Noise generator operators
        /// </summary>
        private static NoiseGeneratorOperatorDelegate[] noiseGeneratorOperators = new NoiseGeneratorOperatorDelegate[]
        {
            (left, right) => left + right,
            (left, right) => left - right,
            (left, right) => left * right,
            (left, right) => left / right
        };

        /// <summary>
        /// Chunk size
        /// </summary>
        [SerializeField]
        private Vector3Int chunkSize = new Vector3Int(16, 16, 16);

        /// <summary>
        /// Grid size
        /// </summary>
        [SerializeField]
        private Vector3Int gridSize = new Vector3Int(16, 8, 16);

        /// <summary>
        /// Force chunk refresh grid distance
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float forceChunkRefreshGridDistance = 2.0f;

        /// <summary>
        /// Clip view angle
        /// </summary>
        [SerializeField]
        [Range(0.0f, 360.0f - float.Epsilon)]
        private float clipViewAngle = 60.0f;

        /// <summary>
        /// Game camera world transform controller
        /// </summary>
        [SerializeField]
        private WorldTransformControllerScript gameCameraWorldTransformController;

        /// <summary>
        /// Noise layers
        /// </summary>
        [SerializeField]
        private NoiseLayerData[] noiseLayers = Array.Empty<NoiseLayerData>();

        /// <summary>
        /// Perlin
        /// </summary>
        private Perlin perlin = new Perlin(1.0, 2.0, 0.5, 6, 0, LibNoise.QualityMode.Medium);

        /// <summary>
        /// Block lookup
        /// </summary>
        private readonly Dictionary<string, IBlockObject> blockLookup = new Dictionary<string, IBlockObject>();

        /// <summary>
        /// Chunk controllers
        /// </summary>
        private ChunkControllerScript[] chunkControllers = Array.Empty<ChunkControllerScript>();

        /// <summary>
        /// Chunk controllers
        /// </summary>
        private ChunkControllerScript[] bufferChunkControllers = Array.Empty<ChunkControllerScript>();

        /// <summary>
        /// Refresh chunk controllers
        /// </summary>
        private List<ChunkControllerScript> refreshChunkControllers = new List<ChunkControllerScript>();

        /// <summary>
        /// Noise layer modules
        /// </summary>
        private List<(INoiseLayerData, List<(INoiseGeneratorData, ModuleBase)>)> computedNoiseLayers = new List<(INoiseLayerData, List<(INoiseGeneratorData, ModuleBase)>)>();

        /// <summary>
        /// Last chunk size
        /// </summary>
        private Vector3Int lastChunkSize = Vector3Int.one;

        /// <summary>
        /// Last chunk grid size
        /// </summary>
        private Vector3Int lastChunkGridSize = Vector3Int.one;

        /// <summary>
        /// Last chunk ID
        /// </summary>
        private ChunkID lastChunkID = ChunkID.Zero;

        /// <summary>
        /// Block lookup
        /// </summary>
        public IReadOnlyDictionary<string, IBlockObject> BlockLookup => blockLookup;

        /// <summary>
        /// Chunk size
        /// </summary>
        public Vector3Int ChunkSize
        {
            get
            {
                if (chunkSize.x <= 0)
                {
                    chunkSize.x = 1;
                }
                if (chunkSize.y <= 0)
                {
                    chunkSize.y = 1;
                }
                if (chunkSize.z <= 0)
                {
                    chunkSize.z = 1;
                }
                return chunkSize;
            }
            set => chunkSize = new Vector3Int(Mathf.Max(value.x, 1), Mathf.Max(value.y, 1), Mathf.Max(value.z, 1));
        }

        /// <summary>
        /// Chunk grid size
        /// </summary>
        public Vector3Int GridSize
        {
            get
            {
                if (gridSize.x <= 0)
                {
                    gridSize.x = 1;
                }
                if (gridSize.y <= 0)
                {
                    gridSize.y = 1;
                }
                if (gridSize.z <= 0)
                {
                    gridSize.z = 1;
                }
                return gridSize;
            }
            set => gridSize = new Vector3Int(Mathf.Max(value.x, 1), Mathf.Max(value.y, 1), Mathf.Max(value.z, 1));
        }

        /// <summary>
        /// Force chunk refresh grid distance
        /// </summary>
        public float ForceChunkRefreshGridDistance
        {
            get => Mathf.Max(forceChunkRefreshGridDistance, 0.0f);
            set => forceChunkRefreshGridDistance = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Clip view angle
        /// </summary>
        public float ClipViewAngle
        {
            get => Mathf.Repeat(clipViewAngle, 360.0f - float.Epsilon);
            set => clipViewAngle = Mathf.Repeat(value, 360.0f - float.Epsilon);
        }

        /// <summary>
        /// Game camera world transform controller
        /// </summary>
        public WorldTransformControllerScript GameCameraWorldTransformController
        {
            get => gameCameraWorldTransformController;
            set => gameCameraWorldTransformController = value;
        }

        /// <summary>
        /// Noise layers
        /// </summary>
        public NoiseLayerData[] NoiseLayers
        {
            get
            {
                if (noiseLayers == null)
                {
                    noiseLayers = Array.Empty<NoiseLayerData>();
                }
                return noiseLayers;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                noiseLayers = value;
            }
        }

        /// <summary>
        /// Get chunk ID from block ID
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <returns>Chunk ID</returns>
        public ChunkID GetChunkIDFDromBlockID(BlockID blockID)
        {
            Vector3Int chunk_size = ChunkSize;
            return new ChunkID
            (
                (int)(((blockID.X < 0L) ? (blockID.X - chunk_size.x + 1) : blockID.X) / chunk_size.x),
                (int)(((blockID.Y < 0L) ? (blockID.Y - chunk_size.y + 1) : blockID.Y) / chunk_size.y),
                (int)(((blockID.Z < 0L) ? (blockID.Z - chunk_size.z + 1) : blockID.Z) / chunk_size.z)
            );
        }

        /// <summary>
        /// Get generated block type
        /// </summary>
        /// <param name="blockID">Block ID</param>
        /// <returns>Block or "null"</returns>
        public IBlockObject GetGeneratedBlockType(BlockID blockID)
        {
            IBlockObject ret = null;
            foreach ((INoiseLayerData, List<(INoiseGeneratorData, ModuleBase)>) computed_noise_layer in computedNoiseLayers)
            {
                if (((computed_noise_layer.Item1.Filter == ENoiseLayerFilter.FillEmpty) && (ret != null)) || ((computed_noise_layer.Item1.Filter == ENoiseLayerFilter.ReplaceFull) && (ret == null)))
                {
                    break;
                }
                double final_result = 0.0;
                foreach ((INoiseGeneratorData, ModuleBase) noise_generator_module in computed_noise_layer.Item2)
                {
                    final_result = noiseGeneratorOperators[(int)(noise_generator_module.Item1.GeneratorOperator)]
                    (
                        final_result,
                        (noise_generator_module.Item2.GetValue
                        (
                            ((((noise_generator_module.Item1.AxisFlags & ENoiseGeneratorAxisFlags.X) == ENoiseGeneratorAxisFlags.X) ? blockID.X : 0L) * noise_generator_module.Item1.InputScale.x) + noise_generator_module.Item1.InputOffset.x,
                            ((((noise_generator_module.Item1.AxisFlags & ENoiseGeneratorAxisFlags.Y) == ENoiseGeneratorAxisFlags.Y) ? blockID.Y : 0L) * noise_generator_module.Item1.InputScale.y) + noise_generator_module.Item1.InputOffset.y,
                            ((((noise_generator_module.Item1.AxisFlags & ENoiseGeneratorAxisFlags.Z) == ENoiseGeneratorAxisFlags.Z) ? blockID.Z : 0L) * noise_generator_module.Item1.InputScale.z) + noise_generator_module.Item1.InputOffset.z
                        ) * noise_generator_module.Item1.OutputScale) + (((noise_generator_module.Item1.AxisFlags & ENoiseGeneratorAxisFlags.X) == ENoiseGeneratorAxisFlags.X) ? 0L : blockID.X) + (((noise_generator_module.Item1.AxisFlags & ENoiseGeneratorAxisFlags.Y) == ENoiseGeneratorAxisFlags.Y) ? 0L : blockID.Y) + (((noise_generator_module.Item1.AxisFlags & ENoiseGeneratorAxisFlags.Z) == ENoiseGeneratorAxisFlags.Z) ? 0L : blockID.Z) + noise_generator_module.Item1.OutputOffset
                    );
                }
                if (final_result >= 0.0)
                {
                    ret = computed_noise_layer.Item1.Block;
                }
            }
            return ret;
        }

        /// <summary>
        /// Refresh chunk controller
        /// </summary>
        /// <param name="chunkID">Chunk ID</param>
        public void RefreshChunkController(ChunkID chunkID)
        {
            if (gameCameraWorldTransformController == null)
            {
                foreach (ChunkControllerScript chunk_controller in chunkControllers)
                {
                    if (chunk_controller)
                    {
                        if (chunk_controller.ChunkID == chunkID)
                        {
                            chunk_controller.Refresh();
                            break;
                        }
                    }
                }
            }
            else
            {
                Vector3Int grid_size = GridSize;
                Vector3Int buffer_position = new Vector3Int(chunkID.X - gameCameraWorldTransformController.ChunkID.X + (grid_size.x / 2), chunkID.Y - gameCameraWorldTransformController.ChunkID.Y + (grid_size.y / 2), chunkID.Z - gameCameraWorldTransformController.ChunkID.Z + (grid_size.z / 2));
                if ((buffer_position.x >= 0) && (buffer_position.x < grid_size.x) && (buffer_position.y >= 0) && (buffer_position.y < grid_size.y) && (buffer_position.z >= 0) && (buffer_position.z < grid_size.z))
                {
                    ChunkControllerScript chunk_controller = chunkControllers[buffer_position.x + (buffer_position.y * grid_size.x) + (buffer_position.z * grid_size.x * grid_size.y)];
                    if (chunk_controller)
                    {
                        chunk_controller.Refresh();
                    }
                }
            }
        }

        /// <summary>
        /// Create chunk controller
        /// </summary>
        /// <param name="chunkID">Chunk ID</param>
        /// <param name="bufferPosition">Position</param>
        /// <returns>Chunk controller if successful, otherwise "null"</returns>
        private ChunkControllerScript CreateChunkController(ChunkID chunkID, Vector3Int bufferPosition)
        {
            ChunkControllerScript ret = null;
            GameObject game_object = new GameObject("Chunk " + chunkID, typeof(ChunkControllerScript));
            if (game_object)
            {
                Vector3Int chunk_size = ChunkSize;
                Vector3Int grid_size = GridSize;
                game_object.transform.position = new Vector3((bufferPosition.x * chunk_size.x) - (grid_size.x * chunk_size.x * 0.5f) + chunk_size.x + 0.5f, (bufferPosition.y * chunk_size.y) - (grid_size.y * chunk_size.y * 0.5f) + chunk_size.y + 0.5f, (bufferPosition.z * chunk_size.z) - (grid_size.z * chunk_size.z * 0.5f) + chunk_size.z + 0.5f);
                ret = game_object.GetComponent<ChunkControllerScript>();
                if (ret == null)
                {
                    Destroy(game_object);
                    ret = null;
                }
                else
                {
                    ret.ChunkID = chunkID;
                    game_object.transform.parent = transform;
                }
            }
            return ret;
        }

        /// <summary>
        /// Get chunk controller
        /// </summary>
        /// <param name="chunkID">Chunk ID</param>
        /// <returns>Chunk controller if available, otherwise "null"</returns>
        public ChunkControllerScript GetChunkController(ChunkID chunkID)
        {
            ChunkControllerScript ret = null;
            if (!gameCameraWorldTransformController)
            {
                // Try searching in chunk controllers
                foreach (ChunkControllerScript chunk_controller in chunkControllers)
                {
                    if (chunk_controller)
                    {
                        if (chunk_controller.ChunkID == chunkID)
                        {
                            ret = chunk_controller;
                            break;
                        }
                    }
                }
            }
            else
            {
                Vector3Int grid_size = GridSize;
                Vector3Int buffer_position = new Vector3Int(chunkID.X - gameCameraWorldTransformController.ChunkID.X + (grid_size.x / 2), chunkID.Y - gameCameraWorldTransformController.ChunkID.Y + (grid_size.y / 2), chunkID.Z - gameCameraWorldTransformController.ChunkID.Z + (grid_size.z / 2));
                if ((buffer_position.x >= 0) && (buffer_position.x < grid_size.x) && (buffer_position.y >= 0) && (buffer_position.y < grid_size.y) && (buffer_position.z >= 0) && (buffer_position.z < grid_size.z))
                {
                    ret = chunkControllers[buffer_position.x + (buffer_position.y * grid_size.x) + (buffer_position.z * grid_size.x * grid_size.y)];
                }
            }
            return ret;
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            BlockObjectScript[] blocks = Resources.LoadAll<BlockObjectScript>("Blocks");
            if (blocks != null)
            {
                foreach (BlockObjectScript block in blocks)
                {
                    if (block)
                    {
                        if (blockLookup.ContainsKey(block.name))
                        {
                            Debug.LogError("Skipping duplicate block entry \"" + block.name + "\".");
                        }
                        else
                        {
                            blockLookup.Add(block.name, block);
                        }
                    }
                }
            }
            foreach (INoiseLayerData noise_layer in NoiseLayers)
            {
                if (noise_layer != null)
                {
                    List<(INoiseGeneratorData, ModuleBase)> noise_generator_modules = new List<(INoiseGeneratorData, ModuleBase)>();
                    foreach (INoiseGeneratorData noise_generator in noise_layer.NoiseGenerators)
                    {
                        noise_generator_modules.Add((noise_generator, noise_generator.NewNoiseModule));
                    }
                    computedNoiseLayers.Add((noise_layer, noise_generator_modules));
                }
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (gameCameraWorldTransformController != null)
            {
                bool initialize_chunks = false;
                bool update_chunks = false;
                Vector3Int chunk_size = ChunkSize;
                Vector3Int grid_size = GridSize;
                int chunk_count = grid_size.x * grid_size.y * grid_size.z;
                Vector3 game_camera_position = gameCameraWorldTransformController.transform.position;
                Vector3 game_camera_forward = gameCameraWorldTransformController.transform.forward;
                Vector2 game_camera_forward_plane = new Vector2(game_camera_forward.x, game_camera_forward.z);
                game_camera_forward_plane = ((game_camera_forward_plane.sqrMagnitude > 0) ? game_camera_forward_plane.normalized : Vector2.up);
                float clip_view_angle = ClipViewAngle;
                float force_chunk_refresh_grid_distance = ForceChunkRefreshGridDistance;
                if (lastChunkSize != chunk_size)
                {
                    lastChunkSize = chunk_size;
                    update_chunks = true;
                }
                if (lastChunkGridSize != grid_size)
                {
                    lastChunkGridSize = grid_size;
                    initialize_chunks = true;
                }
                if (chunkControllers.Length != chunk_count)
                {
                    initialize_chunks = true;
                }
                if (initialize_chunks)
                {
                    foreach (ChunkControllerScript chunk_controller in chunkControllers)
                    {
                        if (chunk_controller)
                        {
                            Destroy(chunk_controller.gameObject);
                        }
                    }
                    WorldManager.Reset();
                    chunkControllers = new ChunkControllerScript[chunk_count];
                    bufferChunkControllers = new ChunkControllerScript[chunk_count];
                    for (int index = 0; index < chunk_count; index++)
                    {
                        Vector3Int buffer_position = new Vector3Int(index % grid_size.x, (index / grid_size.x) % grid_size.y, index / (grid_size.x * grid_size.y));
                        ChunkID chunk_id = new ChunkID(buffer_position.x + gameCameraWorldTransformController.ChunkID.X - (grid_size.x / 2), buffer_position.y + gameCameraWorldTransformController.ChunkID.Y - (grid_size.y / 2), buffer_position.z + gameCameraWorldTransformController.ChunkID.Z - (grid_size.z / 2));
                        chunkControllers[index] = CreateChunkController(chunk_id, buffer_position);
                    }
                    refreshChunkControllers.Clear();
                    refreshChunkControllers.AddRange(chunkControllers);
                }
                else if (update_chunks)
                {
                    refreshChunkControllers.Clear();
                    refreshChunkControllers.AddRange(chunkControllers);
                }
                if (lastChunkID != gameCameraWorldTransformController.ChunkID)
                {
                    Vector3Int delta = (Vector3Int)(gameCameraWorldTransformController.ChunkID - lastChunkID);
                    for (int index = 0; index < chunkControllers.Length; index++)
                    {
                        ChunkControllerScript chunk_controller = chunkControllers[index];
                        ref ChunkControllerScript buffer_chunk_controller = ref bufferChunkControllers[index];
                        if (chunk_controller)
                        {
                            Vector3Int buffer_position = new Vector3Int(index % grid_size.x, (index / grid_size.x) % grid_size.y, index / (grid_size.x * grid_size.y));
                            Vector3Int old_buffer_position = buffer_position + delta;
                            Vector3Int new_buffer_position = buffer_position - delta;
                            buffer_chunk_controller = null;
                            if ((old_buffer_position.x >= 0) && (old_buffer_position.x < grid_size.x) && (old_buffer_position.y >= 0) && (old_buffer_position.y < grid_size.y) && (old_buffer_position.z >= 0) && (old_buffer_position.z < grid_size.z))
                            {
                                int old_index = old_buffer_position.x + (old_buffer_position.y * grid_size.x) + (old_buffer_position.z * grid_size.x * grid_size.y);
                                ChunkControllerScript old_chunk_controller = chunkControllers[old_index];
                                if (old_chunk_controller)
                                {
                                    old_chunk_controller.transform.position -= delta * chunk_size;
                                }
                                buffer_chunk_controller = old_chunk_controller;
                            }
                            if (chunk_controller && ((new_buffer_position.x < 0) || (new_buffer_position.x >= grid_size.x) || (new_buffer_position.y < 0) || (new_buffer_position.y >= grid_size.y) || (new_buffer_position.z < 0) || (new_buffer_position.z >= grid_size.z)))
                            {
                                refreshChunkControllers.Remove(chunk_controller);
                                Destroy(chunk_controller.gameObject);
                            }
                            ChunkID chunk_id = (ChunkID)buffer_position + gameCameraWorldTransformController.ChunkID - (ChunkID)(grid_size / 2);
                            if (!buffer_chunk_controller)
                            {
                                buffer_chunk_controller = CreateChunkController(chunk_id, buffer_position);
                                if (buffer_chunk_controller)
                                {
                                    buffer_chunk_controller.name = "Chunk " + buffer_chunk_controller.ChunkID;
                                    refreshChunkControllers.Add(buffer_chunk_controller);
                                }
                            }
                            else
                            {
                                buffer_chunk_controller.ChunkID = chunk_id;
                            }
                        }
                    }
                    ChunkControllerScript[] temporary_chunk_controllers = chunkControllers;
                    chunkControllers = bufferChunkControllers;
                    bufferChunkControllers = temporary_chunk_controllers;
                    Parallel.ForEach(chunkControllers, (chunk_controller) =>
                    {
                        if (chunk_controller)
                        {
                            chunk_controller.TopChunkController = GetChunkController(chunk_controller.ChunkID + ChunkID.Up);
                            chunk_controller.BottomChunkController = GetChunkController(chunk_controller.ChunkID + ChunkID.Down);
                            chunk_controller.LeftChunkController = GetChunkController(chunk_controller.ChunkID + ChunkID.Left);
                            chunk_controller.RightChunkController = GetChunkController(chunk_controller.ChunkID + ChunkID.Right);
                            chunk_controller.FrontChunkController = GetChunkController(chunk_controller.ChunkID + ChunkID.Forward);
                            chunk_controller.BehindChunkController = GetChunkController(chunk_controller.ChunkID + ChunkID.Back);
                        }
                    });
                    lastChunkID = gameCameraWorldTransformController.ChunkID;
                }
                if (refreshChunkControllers.Count > 0)
                {
                    int refresh_chunk_controller_index = 0;
                    float distance_squared = float.PositiveInfinity;
                    for (int index = 0; index < refreshChunkControllers.Count; index++)
                    {
                        ChunkControllerScript refresh_chunk_controller = refreshChunkControllers[index];
                        Vector3 delta = (refresh_chunk_controller.transform.position - game_camera_position);
                        float delta_magnitude_squared = delta.sqrMagnitude;
                        if (((Mathf.Abs(delta.x) <= (chunk_size.x * force_chunk_refresh_grid_distance)) && (Mathf.Abs(delta.y) <= (chunk_size.y * force_chunk_refresh_grid_distance)) && (Mathf.Abs(delta.z) <= (chunk_size.z * force_chunk_refresh_grid_distance))) || ((distance_squared > delta_magnitude_squared) && ((delta_magnitude_squared <= float.Epsilon) || (Vector3.Angle(delta.normalized, game_camera_forward) <= clip_view_angle))))
                        {
                            distance_squared = delta_magnitude_squared;
                            refresh_chunk_controller_index = index;
                        }
                    }
                    refreshChunkControllers[refresh_chunk_controller_index].Refresh();
                    refreshChunkControllers.RemoveAt(refresh_chunk_controller_index);
                }
                foreach (ChunkControllerScript chunk_controller in chunkControllers)
                {
                    if (chunk_controller != null)
                    {
                        Vector3 chunk_controller_position = chunk_controller.transform.position;
                        Vector2 delta = new Vector2(chunk_controller_position.x - game_camera_position.x, chunk_controller_position.z - game_camera_position.z);
                        chunk_controller.gameObject.SetActive(((Mathf.Abs(delta.x) <= (chunk_size.x * force_chunk_refresh_grid_distance)) && (Mathf.Abs(delta.y) <= (chunk_size.z * force_chunk_refresh_grid_distance))) || ((delta.sqrMagnitude > float.Epsilon) && (Vector2.Angle(delta.normalized, game_camera_forward_plane) <= clip_view_angle)));
                    }
                }
                //foreach (ChunkControllerScript chunk_controller in chunkControllers)
                //{
                //    if (chunk_controller != null)
                //    {
                //        Vector3 delta = (chunk_controller.transform.position - game_camera_position);
                //        chunk_controller.gameObject.SetActive(((Mathf.Abs(delta.x) <= (chunk_size.x * force_chunk_refresh_grid_distance)) && (Mathf.Abs(delta.y) <= (chunk_size.y * force_chunk_refresh_grid_distance)) && (Mathf.Abs(delta.z) <= (chunk_size.z * force_chunk_refresh_grid_distance))) || ((delta.sqrMagnitude > float.Epsilon) && (Vector3.Angle(delta.normalized, game_camera_forward) <= clip_view_angle)));
                //    }
                //}
            }
        }
    }
}

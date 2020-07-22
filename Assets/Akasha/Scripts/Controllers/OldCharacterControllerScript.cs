using Akasha.Objects;
using System;
using UnityEngine;

/// <summary>
/// Akasha controllers namespace
/// </summary>
namespace Akasha.Controllers
{
    /// <summary>
    /// Character controller script class (old)
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class OldCharacterControllerScript : DestructibleControllerScript, IOldCharacterController
    {
        /// <summary>
        /// Ground or wall detection distance
        /// </summary>
        private static readonly float groundOrWallDetectionDistance = 0.0625f;

        /// <summary>
        /// Movement speed
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float movementSpeed = 8.0f;

        /// <summary>
        /// Rotation stiffness
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float rotationStiffness = 0.5f;

        /// <summary>
        /// Sprint multiplier
        /// </summary>
        [SerializeField]
        [Range(1.0f, 10.0f)]
        private float sprintMultiplier = 1.25f;

        /// <summary>
        /// Sneak multiplier
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float sneakMultiplier = 0.5f;

        /// <summary>
        /// Movement animation curve
        /// </summary>
        [SerializeField]
        private AnimationCurve movementAnimationCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 1.0f);

        /// <summary>
        /// Movement time
        /// </summary>
        [SerializeField]
        private float movementTime = 0.125f;

        /// <summary>
        /// Minimal horizontal rotation
        /// </summary>
        [SerializeField]
        [Range(-90.0f, 0.0f)]
        private float minimalHorizontalRotation = -80.0f;

        /// <summary>
        /// Maximal horizontal rotation
        /// </summary>
        [SerializeField]
        [Range(0.0f, 90.0f)]
        private float maximalHorizontalRotation = 80.0f;

        /// <summary>
        /// Jump height
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float jumpHeight = 1.5f;

        /// <summary>
        /// Total jumps
        /// </summary>
        [SerializeField]
        private uint totalJumps = 2U;

        /// <summary>
        /// Fall acceleration multiplier
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float fallAccelerationMultiplier = 3.0f;

        /// <summary>
        /// Air movement acceleration
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float airMovementAcceleration = 15.0f;

        /// <summary>
        /// Air movement brake deceleration
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float airMovementBrakeDeceleration = 60.0f;

        /// <summary>
        /// Minimal fall damage
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float minimalFallDamage = 0.0f;

        /// <summary>
        /// Maximal fall damage speed
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float maximalFallDamage = 100.0f;

        /// <summary>
        /// Minimal fall damage speed
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float minimalFallDamageSpeed = 20.0f;

        /// <summary>
        /// Maximal fall damage speed
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float maximalFallDamageSpeed = 120.0f;

        /// <summary>
        /// Height
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float height = 2.0f;

        /// <summary>
        /// Radius
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float radius = 0.5f;

        /// <summary>
        /// Maximal ground angle
        /// </summary>
        [SerializeField]
        [Range(0.0f, 90.0f)]
        private float maximalGroundAngle = 60.0f;

        /// <summary>
        /// Interaction distance
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float interactionDistance = 3.0f;

        /// <summary>
        /// Weapon
        /// </summary>
        [SerializeField]
        private WeaponObjectScript weapon = default;

        /// <summary>
        /// Eyes transform
        /// </summary>
        [SerializeField]
        private Transform eyesTransform = default;

        /// <summary>
        /// Movement
        /// </summary>
        private Vector2 movement;

        /// <summary>
        /// Elapsed movement time
        /// </summary>
        private float elapsedMovementTime;

        /// <summary>
        /// Last direction
        /// </summary>
        private Vector3 lastDirection;

        /// <summary>
        /// Rotation
        /// </summary>
        private Vector2 rotation;

        /// <summary>
        /// Raycast hits
        /// </summary>
        private RaycastHit[] raycastHits = new RaycastHit[128];

        /// <summary>
        /// Elapsed shot time
        /// </summary>
        private float elapsedShootTime;

        /// <summary>
        /// Elapsed reload time
        /// </summary>
        private float elapsedReloadTime;

        /// <summary>
        /// Last weapon
        /// </summary>
        private WeaponObjectScript lastWeapon;

        /// <summary>
        /// Movement speed
        /// </summary>
        public float MovementSpeed
        {
            get => Mathf.Max(movementSpeed, 0.0f);
            set => movementSpeed = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Rotation stiffness
        /// </summary>
        public float RotationStiffness
        {
            get => Mathf.Clamp(rotationStiffness, 0.0f, 1.0f);
            set => rotationStiffness = Mathf.Clamp(value, 0.0f, 1.0f);
        }

        /// <summary>
        /// Sprint multiplier
        /// </summary>
        public float SprintMultiplier
        {
            get => Mathf.Max(sprintMultiplier, 1.0f);
            set => sprintMultiplier = Mathf.Max(value, 1.0f);
        }

        /// <summary>
        /// Sneak multiplier
        /// </summary>
        public float SneakMultiplier
        {
            get => Mathf.Clamp(sneakMultiplier, 0.0f, 1.0f);
            set => sneakMultiplier = Mathf.Clamp(value, 0.0f, 1.0f);
        }

        /// <summary>
        /// Movement animation curve
        /// </summary>
        public AnimationCurve MovementAnimationCurve
        {
            get
            {
                if (movementAnimationCurve != null)
                {
                    movementAnimationCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 1.0f);
                }
                return movementAnimationCurve;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                movementAnimationCurve = value;
            }
        }

        /// <summary>
        /// Movement time
        /// </summary>
        public float MovementTime
        {
            get => Mathf.Max(movementTime, 0.0f);
            set => movementTime = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Minimal horizontal rotation
        /// </summary>
        public float MinimalHorizontalRotation
        {
            get => Mathf.Min(minimalHorizontalRotation, maximalHorizontalRotation);
            set => minimalHorizontalRotation = value;
        }

        /// <summary>
        /// Maximal horizontal rotation
        /// </summary>
        public float MaximalHorizontalRotation
        {
            get => Mathf.Max(minimalHorizontalRotation, maximalHorizontalRotation);
            set => maximalHorizontalRotation = value;
        }

        /// <summary>
        /// Jump height
        /// </summary>
        public float JumpHeight
        {
            get => Mathf.Max(jumpHeight, 0.0f);
            set => jumpHeight = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Total jumps
        /// </summary>
        public uint TotalJumps
        {
            get => totalJumps;
            set => totalJumps = value;
        }

        /// <summary>
        /// Fall acceleration multiplier
        /// </summary>
        public float FallAccelerationMultiplier
        {
            get => Mathf.Max(fallAccelerationMultiplier, 0.0f);
            set => fallAccelerationMultiplier = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Air movement acceleration
        /// </summary>
        public float AirMovementAcceleration
        {
            get => Mathf.Max(airMovementAcceleration, 0.0f);
            set => airMovementAcceleration = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Air movement brake deceleration
        /// </summary>
        public float AirMovementBrakeDeceleration
        {
            get => Mathf.Max(airMovementBrakeDeceleration, 0.0f);
            set => airMovementBrakeDeceleration = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Minimal fall damage
        /// </summary>
        public float MinimalFallDamage
        {
            get => Mathf.Max(Mathf.Min(minimalFallDamage, maximalFallDamage), 0.0f);
            set => minimalFallDamage = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Maximal fall damage speed
        /// </summary>
        public float MaximalFallDamage
        {
            get => Mathf.Max(Mathf.Max(minimalFallDamage, maximalFallDamage), 0.0f);
            set => maximalFallDamage = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Minimal fall damage speed
        /// </summary>
        public float MinimalFallDamageSpeed
        {
            get => Mathf.Max(Mathf.Min(minimalFallDamageSpeed, maximalFallDamageSpeed), 0.0f);
            set => minimalFallDamageSpeed = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Maximal fall damage speed
        /// </summary>
        public float MaximalFallDamageSpeed
        {
            get => Mathf.Max(Mathf.Max(minimalFallDamageSpeed, maximalFallDamageSpeed), 0.0f);
            set => maximalFallDamageSpeed = Mathf.Max(value, 0.0f);
        }

        ///// <summary>
        ///// In air or jumping movement multiplier
        ///// </summary>
        //public float InAirOrJumpingMovementMultiplier
        //{
        //    get => Mathf.Clamp(inAirOrJumpingMovementMultiplier, 0.0f, 1.0f);
        //    set => inAirOrJumpingMovementMultiplier = Mathf.Clamp(value, 0.0f, 1.0f);
        //}

        /// <summary>
        /// Height
        /// </summary>
        public float Height
        {
            get => Mathf.Max(height, 0.0f);
            set => height = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Radius
        /// </summary>
        public float Radius
        {
            get => Mathf.Max(radius, 0.0f);
            set => radius = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Maximal ground angle
        /// </summary>
        public float MaximalGroundAngle
        {
            get => Mathf.Clamp(maximalGroundAngle, 0.0f, 90.0f);
            set => maximalGroundAngle = Mathf.Clamp(value, 0.0f, 90.0f);
        }

        /// <summary>
        /// Interaction distance
        /// </summary>
        public float InteractionDistance
        {
            get => Mathf.Max(interactionDistance, 0.0f);
            set => interactionDistance = Mathf.Max(value, 0.0f);
        }

        /// <summary>
        /// Weapon
        /// </summary>
        public WeaponObjectScript Weapon
        {
            get => weapon;
            set => weapon = value;
        }

        /// <summary>
        /// Eyes transform
        /// </summary>
        public Transform EyesTransform
        {
            get => eyesTransform;
            set => eyesTransform = value;
        }

        /// <summary>
        /// Movement
        /// </summary>
        public Vector2 Movement
        {
            get => movement;
            set => movement = ((value.sqrMagnitude > 1.0f) ? value.normalized : value);
        }

        /// <summary>
        /// Rotation
        /// </summary>
        public Vector2 Rotation
        {
            get => rotation;
            set => rotation = new Vector2(Mathf.Clamp(value.x, MinimalHorizontalRotation, MaximalHorizontalRotation), Mathf.Repeat(value.y, 360.0f - float.Epsilon));
        }

        /// <summary>
        /// Is alive
        /// </summary>
        public bool IsAlive => (Health > float.Epsilon);

        /// <summary>
        /// Running mode
        /// </summary>
        public ERunningMode RunningMode { get; set; }

        /// <summary>
        /// Is on ground
        /// </summary>
        public bool IsOnGround { get; private set; }

        /// <summary>
        /// Is in air or jumping
        /// </summary>
        public bool IsInAirOrJumping => ((ExecutedJumps > 0U) || (!IsOnGround));

        /// <summary>
        /// Ground normal
        /// </summary>
        public Vector3 GroundNormal { get; private set; }

        /// <summary>
        /// Executed jumps
        /// </summary>
        public uint ExecutedJumps { get; private set; }

        /// <summary>
        /// Shots fired
        /// </summary>
        public uint ShotsFired { get; private set; }

        /// <summary>
        /// Is reloading
        /// </summary>
        public bool IsReloading { get; private set; }

        /// <summary>
        /// Character rigidbody
        /// </summary>
        public Rigidbody CharacterRigidbody { get; private set; }

        /// <summary>
        /// Weapon and hands animator
        /// </summary>
        public Animator WeaponAndHandsAnimator { get; private set; }

        /// <summary>
        /// Jump
        /// </summary>
        public void Jump()
        {
            if (IsAlive && (ExecutedJumps < totalJumps) && (CharacterRigidbody != null))
            {
                CharacterRigidbody.AddForce(GroundNormal * (Mathf.Sqrt(-2.0f * JumpHeight * Physics.gravity.y) - Vector3.Dot(GroundNormal, CharacterRigidbody.velocity)), ForceMode.VelocityChange);
                ++ExecutedJumps;
            }
        }

        /// <summary>
        /// Shoot
        /// </summary>
        public void Shoot()
        {
            if (IsAlive && (weapon != null))
            {
                if (ShotsFired >= weapon.AmmoCapacity)
                {
                    Reload();
                }
                else if (elapsedShootTime >= weapon.ShootTime)
                {
                    elapsedShootTime = 0.0f;
                    ++ShotsFired;
                    if ((eyesTransform != null) && (weapon.Distance > float.Epsilon))
                    {
                        int raycast_hits_count = PhysicsUtils.Raycast(eyesTransform.position, eyesTransform.forward, weapon.Distance, ref raycastHits);
                        RaycastHit? nearest_valid_raycast_hit = null;
                        for (int raycast_hits_index = 0; raycast_hits_index < raycast_hits_count; raycast_hits_index++)
                        {
                            RaycastHit raycast_hit = raycastHits[raycast_hits_index];
                            if ((nearest_valid_raycast_hit == null) || (nearest_valid_raycast_hit.Value.distance > raycast_hit.distance))
                            {
                                GameObject game_object = raycast_hit.collider.gameObject;
                                bool success = true;
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
                                    nearest_valid_raycast_hit = raycast_hit;
                                }
                            }
                        }
                        if (nearest_valid_raycast_hit != null)
                        {
                            RaycastHit raycast_hit = nearest_valid_raycast_hit.Value;
                            GameObject game_object = raycast_hit.collider.gameObject;
                            while (game_object != null)
                            {
                                DestructibleControllerScript descructible_controller = game_object.GetComponent<DestructibleControllerScript>();
                                if (descructible_controller != null)
                                {
                                    descructible_controller.Health -= Mathf.Lerp(weapon.Damage, 0.0f, Mathf.Sqrt(raycast_hit.distance / weapon.Distance));
                                    break;
                                }
                                if (game_object.transform.parent == null)
                                {
                                    break;
                                }
                                game_object = game_object.transform.parent.gameObject;
                            }
                            game_object = raycast_hit.collider.gameObject;
                            while (game_object != null)
                            {
                                Rigidbody rigidbody = game_object.GetComponent<Rigidbody>();
                                if (rigidbody != null)
                                {
                                    rigidbody.AddForceAtPosition(eyesTransform.forward * weapon.KnockbackImpulse, raycast_hit.point, ForceMode.Impulse);
                                }
                                if (game_object.transform.parent == null)
                                {
                                    break;
                                }
                                game_object = game_object.transform.parent.gameObject;
                            }
                            if (weapon.BulletHoleAsset != null)
                            {
                                Instantiate(weapon.BulletHoleAsset, raycast_hit.point, Quaternion.FromToRotation(Vector3.forward, raycast_hit.collider.transform.InverseTransformDirection(-raycast_hit.normal)), raycast_hit.collider.transform);
                            }
                        }
                    }
                    if (ShotsFired >= weapon.AmmoCapacity)
                    {
                        Reload();
                    }
                }
            }
        }

        /// <summary>
        /// Reload
        /// </summary>
        public void Reload()
        {
            if (IsAlive && (ShotsFired > 0U))
            {
                IsReloading = true;
            }
        }

        /// <summary>
        /// Interact
        /// </summary>
        public void Interact()
        {
            if (IsAlive && (eyesTransform != null))
            {
                int raycast_hits_count = PhysicsUtils.Raycast(eyesTransform.position, eyesTransform.forward, InteractionDistance, ref raycastHits);
                RaycastHit? nearest_valid_raycast_hit = null;
                for (int raycast_hits_index = 0; raycast_hits_index < raycast_hits_count; raycast_hits_index++)
                {
                    RaycastHit raycast_hit = raycastHits[raycast_hits_index];
                    if ((nearest_valid_raycast_hit == null) || (nearest_valid_raycast_hit.Value.distance > raycast_hit.distance))
                    {
                        nearest_valid_raycast_hit = raycast_hit;
                    }
                }
                if (nearest_valid_raycast_hit != null)
                {
                    GameObject game_object = nearest_valid_raycast_hit.Value.collider.gameObject;
                    while (game_object != null)
                    {
                        InteractableControllerScript interactable_controller = game_object.GetComponent<InteractableControllerScript>();
                        if (interactable_controller != null)
                        {
                            interactable_controller.Interact();
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

        /// <summary>
        /// Start
        /// </summary>
        protected override void Start()
        {
            base.Start();
            CharacterRigidbody = GetComponent<Rigidbody>();
            if (CharacterRigidbody != null)
            {
                CharacterRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            float delta_time = Time.deltaTime;
            elapsedShootTime = ((weapon == null) ? 0.0f : Mathf.Min(elapsedShootTime + delta_time, weapon.ShootTime));
            if (IsReloading)
            {
                elapsedReloadTime += delta_time;
                if (elapsedReloadTime >= weapon.ReloadTime)
                {
                    elapsedReloadTime = 0.0f;
                    ShotsFired = 0U;
                    IsReloading = false;
                }
            }
            if (lastWeapon != weapon)
            {
                lastWeapon = weapon;
                if (WeaponAndHandsAnimator != null)
                {
                    Destroy(WeaponAndHandsAnimator.gameObject);
                    WeaponAndHandsAnimator = null;
                }
                if (weapon != null)
                {
                    if (weapon.WeaponAndHandsAsset != null)
                    {
                        GameObject go = Instantiate(weapon.WeaponAndHandsAsset, transform);
                        if (go != null)
                        {
                            Animator weapon_and_hands_animator = go.GetComponent<Animator>();
                            if (weapon_and_hands_animator == null)
                            {
                                Destroy(go);
                            }
                            else
                            {
                                WeaponAndHandsAnimator = weapon_and_hands_animator;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Fixed update
        /// </summary>
        private void FixedUpdate()
        {
            float delta_time = Time.fixedDeltaTime;
            float radius = Radius;
            int raycast_hits_count = PhysicsUtils.SphereCast(transform.position + (Vector3.up * (radius + (groundOrWallDetectionDistance * 0.5f))), 0.5f, Vector3.down, groundOrWallDetectionDistance, ref raycastHits);
            bool is_on_ground = false;
            float rotation_stiffness = RotationStiffness;
            float damage_per_second = 0.0f;
            for (int raycast_hits_index = 0; raycast_hits_index < raycast_hits_count; raycast_hits_index++)
            {
                RaycastHit raycast_hit = raycastHits[raycast_hits_index];
                if ((raycast_hit.collider.gameObject != gameObject) && ((raycast_hit.collider.transform.parent == null) ? true : (raycast_hit.collider.transform.parent.gameObject != gameObject)) && (Vector3.Angle(Vector3.up, raycast_hit.normal) <= MaximalGroundAngle))
                {
                    is_on_ground = true;
                    GroundNormal += raycast_hit.normal;
                    HurtableGroundControllerScript hurtable_ground_controller = raycast_hit.collider.GetComponent<HurtableGroundControllerScript>();
                    if (hurtable_ground_controller != null)
                    {
                        damage_per_second = Mathf.Max(hurtable_ground_controller.DamagePerSecond, damage_per_second);
                    }
                }
            }
            GroundNormal = (is_on_ground ? ((GroundNormal.sqrMagnitude > float.Epsilon) ? GroundNormal.normalized : Vector3.up) : Vector3.up);
            if (IsOnGround != is_on_ground)
            {
                IsOnGround = is_on_ground;
                if (is_on_ground)
                {
                    ExecutedJumps = 0U;
                }
            }
            Health -= damage_per_second * delta_time;
            if (CharacterRigidbody != null)
            {
                if (IsAlive)
                {
                    Vector2 movement = Movement;
                    Vector3 direction = (CharacterRigidbody.transform.right * movement.x) + (CharacterRigidbody.transform.forward * movement.y);
                    Vector3 velocity = CharacterRigidbody.velocity;
                    float movement_speed = MovementSpeed;
                    if (direction.sqrMagnitude > 1.0f)
                    {
                        direction.Normalize();
                        lastDirection = direction;
                    }
                    else if (direction.sqrMagnitude > float.Epsilon)
                    {
                        lastDirection = direction;
                    }
                    CharacterRigidbody.rotation = Quaternion.Lerp(CharacterRigidbody.rotation, Quaternion.Euler(0.0f, rotation.y, 0.0f), rotation_stiffness);
                    if (is_on_ground)
                    {
                        float movement_time = MovementTime;
                        switch (RunningMode)
                        {
                            case ERunningMode.Sprinting:
                                movement_speed *= SprintMultiplier;
                                break;
                            case ERunningMode.Sneaking:
                                movement_speed *= SneakMultiplier;
                                break;
                        }
                        if (direction.sqrMagnitude > 1.0f)
                        {
                            elapsedMovementTime = Mathf.Clamp(elapsedMovementTime + delta_time, 0.0f, movement_time);
                        }
                        else if (direction.sqrMagnitude <= float.Epsilon)
                        {
                            elapsedMovementTime = Mathf.Clamp(elapsedMovementTime - delta_time, 0.0f, movement_time);
                        }
                        else
                        {
                            elapsedMovementTime = Mathf.Clamp(elapsedMovementTime + delta_time, 0.0f, movement_time);
                        }
                        lastDirection *= MovementAnimationCurve.Evaluate((movement_time <= float.Epsilon) ? 1.0f : (elapsedMovementTime / movement_time));
                        Quaternion surface_rotation = Quaternion.FromToRotation(Vector3.up, GroundNormal);
                        Vector3 movement_velocity = (surface_rotation * lastDirection) * movement_speed * Mathf.Clamp(Vector3.Dot(Vector3.up, GroundNormal), 0.0f, 1.0f);
                        if (velocity.y < (-(float.Epsilon)))
                        {
                            Vector3 gravity = Physics.gravity;
                            velocity = new Vector3(movement_velocity.x + (gravity.x * delta_time), velocity.y + (gravity.y * delta_time), movement_velocity.z + (gravity.z * delta_time));
                        }
                        else
                        {
                            velocity = new Vector3(movement_velocity.x, velocity.y, movement_velocity.z);
                        }
                    }
                    else
                    {
                        if (velocity.y < (-(float.Epsilon)))
                        {
                            velocity += (direction * (AirMovementAcceleration * delta_time)) + (Physics.gravity * delta_time);
                        }
                        else
                        {
                            velocity += (direction * (AirMovementAcceleration * delta_time));
                        }
                        Vector3 current_movement_velocity = new Vector3(velocity.x, 0.0f, velocity.z);
                        float current_movement_velocity_magnitude_squared = current_movement_velocity.sqrMagnitude;
                        if (current_movement_velocity_magnitude_squared > (movement_speed * movement_speed))
                        {
                            velocity += current_movement_velocity.normalized * (-AirMovementBrakeDeceleration * delta_time);
                        }
                    }
                    CharacterRigidbody.velocity = velocity;
                }
                else
                {
                    CharacterRigidbody.constraints = RigidbodyConstraints.None;
                }
            }
            if (IsAlive)
            {
                Quaternion children_local_rotation = Quaternion.Euler(rotation.x, 0.0f, 0.0f);
                if (eyesTransform != null)
                {
                    eyesTransform.localRotation = Quaternion.Lerp(eyesTransform.localRotation, children_local_rotation, rotation_stiffness);
                }
                if (WeaponAndHandsAnimator != null)
                {
                    WeaponAndHandsAnimator.transform.localRotation = Quaternion.Lerp(WeaponAndHandsAnimator.transform.localRotation, children_local_rotation, rotation_stiffness);
                }
            }
        }

        /// <summary>
        /// On collision enter
        /// </summary>
        /// <param name="collision">Collision</param>
        private void OnCollisionEnter(Collision collision)
        {
            GameObject game_object = collision.gameObject;
            bool success = true;
            while (game_object != null)
            {
                if (game_object == gameObject)
                {
                    success = false;
                    break;
                }
                else
                {
                    if (game_object.transform.parent == null)
                    {
                        break;
                    }
                    else
                    {
                        game_object = game_object.transform.parent.gameObject;
                    }
                }
            }
            if (success)
            {
                float minimal_fall_damage_speed = MinimalFallDamageSpeed;
                Vector3 normal;
                if (collision.contactCount > 0)
                {
                    normal = Vector3.zero;
                    for (int i = 0; i < collision.contactCount; i++)
                    {
                        normal += collision.GetContact(i).normal;
                    }
                    normal /= collision.contactCount;
                }
                else
                {
                    normal = Vector3.one;
                }
                float collision_speed = Vector3.Dot(normal, collision.relativeVelocity);
                if (collision_speed >= minimal_fall_damage_speed)
                {
                    float damage_speed_delta = MaximalFallDamageSpeed - minimal_fall_damage_speed;
                    float fall_damage = Mathf.Lerp(MinimalFallDamage, MaximalFallDamage, ((damage_speed_delta > float.Epsilon) ? Mathf.Clamp((collision_speed - minimal_fall_damage_speed) / damage_speed_delta, 0.0f, 1.0f) : 1.0f));
                    Health -= fall_damage;
                }
            }
        }
    }
}

using UnityEngine;
using Dungeon.UI;
using Core.Combat;
using Dungeon.DungeonGeneration;
using Dungeon.Saving;
using Core.ItemManagement;

namespace Core.Player
{
    public class PlayerController : MonoBehaviour, ISaveable
    {
        [Header("Dash (LeftShift)")]
        [SerializeField] private float dashForce = 500f;
        [SerializeField] private float dashForceMultiplier = 1.5f;
        [SerializeField] private float dashCooldown = 5f;

        [Header("Wallrun Settings")]
        [SerializeField] private LayerMask wallLayer;
        [SerializeField] private float wallRunForce;
        [SerializeField] private float maxWallRunTime;
        [SerializeField] private float maxWallSpeed;
        [SerializeField] private float maxWallRunCameraTilt;

        [Header("Slide and Crouch Settings")]
        [SerializeField] private Vector3 crouchScale = new Vector3(1f, 0.5f, 1f);
        [SerializeField] private float crouchJumpMultiplier = 1f;
        [SerializeField] private float slideForce = 600f;
        [SerializeField] private float slideDrag = 0.1f;
        [SerializeField] private float slideCooldown = 1f;
        [SerializeField] private float slideJumpMultiplier = 2.5f;
        [SerializeField] private float slideSpeedMultiplier = 5f;
        [SerializeField] private float maxSlope = 15f;
        [SerializeField] private float velocityForSlideJump = 5f;

        [Header("Ground Check Settings")]
        [SerializeField] private float groundCheckRadius = 0.4f;
        [SerializeField] private float groundCheckDistance = 1f;
        [SerializeField] private LayerMask groundLayer;

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 500f;
        [SerializeField] private float moveMultiplier = 9f;
        [SerializeField] private float maxSpeed = 20f;
        [SerializeField] private float drag = 230f;
        [SerializeField] private float playerHeight = 2f;

        [Header("Jump Settings")]
        [SerializeField] private float jumpForce = 300f;
        [SerializeField] private float jumpMultiplier = 1.5f;
        [SerializeField] private float inAirMovementModifier = 0.5f;
        [SerializeField] private float inAirDrag = 80f;

        [Header("Mouse Look Settings")]
        [SerializeField] private Transform playerCamera = null;
        [SerializeField] private Transform orientation = null;
        [SerializeField] private Vector2 sensitivity = new Vector2(20f, 20f);
        [SerializeField] private float maxCameraAngle = 90f;

        private Rigidbody rb;
        private Controls controls;

        private Vector2 moveInput = Vector2.zero;
        private Vector2 mouseInput = Vector2.zero;

        private Vector3 originalScale = Vector3.zero;

        private float currentSlope = 0f;
        private float xRotation = 0f;
        private bool startRotation = false;

        private float wallRunCameraTilt;

        private float timeSinceLastSlide = 0f;
        private float timeSinceLastDash = 0f;

        private bool crouching, sliding;
        private bool wallRight, wallLeft;
        private bool wallRunning;

        private RaycastHit slopeHit;
        private Vector3 slopeMoveDirection;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            controls = new Controls();

            originalScale = transform.localScale;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Start()
        {
            //Basic movement
            controls.Gameplay.Jump.performed += ctx => OnJump();

            controls.Gameplay.Crouch.performed += ctx => ToggleCrouch(true);
            controls.Gameplay.Crouch.canceled += ctx => ToggleCrouch(false);

            //Abilities
            //controls.Gameplay.Dash.performed += ctx => Dash();
        }

        private void Update()
        {
            if (PauseMenu.isPaused) { return; }
            if (DungeonGenerator.dungeonState != DungeonState.completed) { return; }

            UpdateInputs();
            UpdateMouseLook();

            //CheckForWall();
        }

        private void FixedUpdate()
        {
            if (PauseMenu.isPaused) { return; }
            if (DungeonGenerator.dungeonState != DungeonState.completed) { return; }

            UpdateMovement();
        }

        private void UpdateInputs()
        {
            moveInput = controls.Gameplay.Movement.ReadValue<Vector2>();
            mouseInput = controls.Gameplay.Mouse.ReadValue<Vector2>() * sensitivity * Time.fixedDeltaTime;

            /*if (moveInput.x > 0 && wallRight && !wallRunning)
                StartWallrun();
            if (moveInput.x < 0 && wallLeft && !wallRunning)
                StartWallrun();*/
        }

        private void UpdateMouseLook()
        {
            if (Interract.panelOpen) { return; }

            Vector3 rot = playerCamera.localRotation.eulerAngles;
            float xTo = rot.y + mouseInput.x;

            xRotation -= mouseInput.y;
            if (!startRotation)
            {
                xRotation = playerCamera.localRotation.eulerAngles.x;
                startRotation = true;
            }
            
            xRotation = Mathf.Clamp(xRotation, -maxCameraAngle, maxCameraAngle);
            playerCamera.localRotation = Quaternion.Euler(xRotation, xTo, wallRunCameraTilt);
            orientation.localRotation = Quaternion.Euler(0f, xTo, 0f);

            /*if (Mathf.Abs(wallRunCameraTilt) < maxWallRunCameraTilt && wallRunning && wallRight)
                wallRunCameraTilt += Time.deltaTime * maxWallRunCameraTilt * 2;
            if (Mathf.Abs(wallRunCameraTilt) < maxWallRunCameraTilt && wallRunning && wallLeft)
                wallRunCameraTilt -= Time.deltaTime * maxWallRunCameraTilt * 2;
            if (wallRunCameraTilt > 0 && !wallRight && !wallLeft)
                wallRunCameraTilt -= Time.deltaTime * maxWallRunCameraTilt * 2;
            if (wallRunCameraTilt < 0 && !wallRight && !wallLeft)
                wallRunCameraTilt += Time.deltaTime * maxWallRunCameraTilt * 2;*/
        }

        private void UpdateMovement()
        {
            Vector3 dir = orientation.right * moveInput.x + orientation.forward * moveInput.y;
            rb.AddForce(Vector3.down * Time.fixedDeltaTime * 10f);

            ApplyDrag(-rb.velocity);

            if (crouching && GroundCheck())
            {
                rb.AddForce(Vector3.down * Time.fixedDeltaTime * 5000f);
                return;
            }

            float multiplier = GroundCheck() ? 1f : inAirMovementModifier;

            if (rb.velocity.magnitude > maxSpeed)
            {
                dir = Vector3.zero;
            }

            if (GroundCheck() && OnSlope() && !wallRunning)
            {
                slopeMoveDirection = Vector3.ProjectOnPlane(dir, slopeHit.normal);
                rb.AddForce(slopeMoveDirection * moveSpeed * moveMultiplier * Time.fixedDeltaTime * multiplier * ((GroundCheck() && crouching) ? 0.7f : multiplier));
            }
            else
            {
                rb.AddForce(dir * moveSpeed * moveMultiplier * Time.fixedDeltaTime * multiplier * ((GroundCheck() && crouching) ? 0.7f : multiplier));
            }
        }

        private void ApplyDrag(Vector3 dir)
        {
            if (!GroundCheck())
            {
                rb.AddForce(new Vector3(dir.x, 0f, dir.z) * inAirDrag * Time.fixedDeltaTime);
                return;
            }

            if (crouching)
            {
                rb.AddForce(moveSpeed * Time.fixedDeltaTime * rb.velocity.normalized * slideDrag);
                return;
            }

            if (rb.velocity.magnitude != 0)
            {
                rb.AddForce(dir * drag * Time.fixedDeltaTime);
            }
        }

        private bool GroundCheck()
        {
            bool grounded;
            if (Physics.SphereCast(transform.position, groundCheckRadius, Vector3.down, out RaycastHit hitGround, groundCheckDistance, groundLayer))
            {
                grounded = true;
                currentSlope = Vector3.Angle(Vector3.up, hitGround.normal);
            }
            else if (Physics.SphereCast(transform.position, groundCheckRadius, Vector3.down, out RaycastHit hitWall, groundCheckDistance, wallLayer) && !wallLeft && !wallRight)
            {
                grounded = true;
                currentSlope = Vector3.Angle(Vector3.up, hitWall.normal);
            }
            else
            {
                grounded = false;
            }

            return grounded;

            /*bool grounded = Physics.SphereCast(transform.position, groundCheckRadius, Vector3.down, out RaycastHit hit, groundCheckDistance, groundLayer);
            currentSlope = Vector3.Angle(Vector3.up, hit.normal);
            return grounded;*/
        }

        private void OnJump()
        {
            if (PauseMenu.isPaused) { return; }

            if (GroundCheck() || wallRunning)
            {
                //If couching and not sliding: crouch jump multiplier if sliding: slide jump multiplier, and if all else if false: normal jump multiplier
                rb.AddForce(Vector2.up * jumpForce * (crouching && currentSlope < 10 ? crouchJumpMultiplier : crouching && currentSlope >= 10 && rb.velocity.magnitude >= velocityForSlideJump ? slideJumpMultiplier : crouching && currentSlope >= 10 ? crouchJumpMultiplier : jumpMultiplier));
                rb.AddForce(orientation.forward * maxSpeed * (crouching && currentSlope >= 10 && rb.velocity.magnitude >= velocityForSlideJump ? slideSpeedMultiplier : 0f));
            }
        }

        private void ToggleCrouch(bool enabled)
        {
            if (PauseMenu.isPaused) { return; }

            if (enabled && !wallRunning)
            {
                crouching = true;
                transform.localScale = crouchScale;
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);

                if (Time.time > timeSinceLastSlide)
                {
                    if (rb.velocity.magnitude > 0.5f && GroundCheck() && !sliding)
                    {
                        timeSinceLastSlide = Time.time + slideCooldown;
                        sliding = true;
                        rb.AddForce(orientation.forward * slideForce);
                        timeSinceLastSlide = 0f;
                    }
                }


            }
            else if (!wallRunning)
            {
                crouching = false;
                sliding = false;
                transform.localScale = originalScale;
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (GroundCheck() && crouching && rb.velocity.magnitude > 0.5f && currentSlope < maxSlope && !sliding && timeSinceLastSlide >= slideCooldown)
            {
                rb.AddForce(orientation.forward * slideForce);
            }
        }

        private void CheckForWall()
        {
            wallRight = Physics.Raycast(transform.position, orientation.right, 1f, wallLayer);
            wallLeft = Physics.Raycast(transform.position, -orientation.right, 1f, wallLayer);

            if (!wallLeft && !wallRight) StopWallRun();
        }

        private void StartWallrun()
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.useGravity = false;
            wallRunning = true;

            if (rb.velocity.magnitude <= maxWallSpeed)
            {
                rb.AddForce(orientation.forward * wallRunForce * Time.deltaTime);

                if (wallRight)
                    rb.AddForce(orientation.right * wallRunForce / 5 * Time.deltaTime);
                else
                    rb.AddForce(-orientation.right * wallRunForce / 5 * Time.deltaTime);
            }
        }

        private void StopWallRun()
        {
            wallRunning = false;
            rb.useGravity = true;
        }

        private void Dash()
        {
            if (Time.time > timeSinceLastDash)
            {
                rb.AddForce(orientation.forward * dashForce * dashForceMultiplier);
                timeSinceLastDash = Time.time + dashCooldown;
            }
        }

        private bool OnSlope()
        {
            if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight))
            {
                if(slopeHit.normal != Vector3.up)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }

        public object CaptureState()
        {
            return new SerializeableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializeableVector3 position = (SerializeableVector3)state;
            transform.position = position.ToVector();
        }
    }
}

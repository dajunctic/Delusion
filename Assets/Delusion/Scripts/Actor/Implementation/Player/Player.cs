using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dajunctic
{
    public class Player : Actor, IPlayer
    {
        [SerializeField] InputActionAsset playerInput;
        [SerializeField] InputActionReference moveInput;
        [SerializeField] InputActionReference jumpInput;
        [SerializeField] InputActionReference lookInput;
        [SerializeField] InputActionReference sprintInput;
        [SerializeField] InputActionReference dashInput;

        [SerializeField] Camera cam;
        [SerializeField] LayerMask groundLayer = ~0;
        public Camera Camera => cam;

        public float AirControlRatio => 0.6f;

        public float LightLandingDuration => 0.15f;

        public float HardLandingDuration => 0.5f;

        public float HardLandingThreshold => 5f;

        private float CoyoteTime => 0.15f;
        private float JumpBufferTime => 0.12f;

        private float MaxSlopeAngle = 45f; 
        private float SlopeSnapForce = 10f;
        private AnimationCurve SlopeJumpCurve = AnimationCurve.Linear(0f, 1f, 45f, 0.65f);

        protected BaseStateMachine<IPlayer> stateMachine;

        private float verticalVelocity;
        private float lastDashTime;
        private float coyoteTimer;
        private float jumpBufferTimer;
        private float fallStartY;


        public override void Initialize()
        {
            base.Initialize();

            stateMachine = new PlayerStateMachine<IPlayer>();
            stateMachine.Initialize(
                this,
                new List<IState<IPlayer>>
                {
                    new PlayerIdleState(),
                    new PlayerWalkState(),
                    new PlayerRunState(),
                    new PlayerJumpState(),
                    new PlayerFallState(),
                    new PlayerLandingState(),
                    new PlayerDashState(),
                    new PlayerLightStoppingState(),
                    new PlayerHardStoppingState()
                }
            );

            stateMachine.ChangeState<PlayerIdleState>();

            playerInput.Enable();
        }

        public override void ListenEvents()
        {
            base.ListenEvents();

        }

        public override void StopListenEvents()
        {
            base.StopListenEvents();
        }

        public override void Tick()
        {
            base.Tick();
            ApplyGravity();
            stateMachine.Tick();
        }

        private void ApplyGravity()
        {
            if (IsGround() && verticalVelocity <= 0)
            {
                verticalVelocity = 0f;
            }
            else
            {
                verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        public Vector2 GetMoveInput()
        {
            return moveInput.action.ReadValue<Vector2>().normalized;
        }

        public bool GetSprintInput()
        {
            return sprintInput.action.IsPressed();
        }

        public bool GetJumpInput()
        {
            return jumpInput.action.IsPressed();
        }

        public bool GetDashInput()
        {
            return dashInput.action.IsPressed();
        }

        public bool IsGround()
        {
            Vector3 rayOrigin = transform.position + Vector3.up * 0.2f;
            int mask = groundLayer & ~(1 << gameObject.layer);
            return Physics.Raycast(rayOrigin, Vector3.down, 0.35f, mask, QueryTriggerInteraction.Ignore);
        }

        public override void PlayAnimation(int animHash)
        {
            Animator.SetBool(animHash, true);
        }

        public override void StopAnimation(int animHash)
        {
            Animator.SetBool(animHash, false);
        }

        public float GetVerticalVelocity()
        {
            return verticalVelocity;
        }

        public void AddForceVerticalVelocity(float force)
        {
            verticalVelocity = force;
            var vel = Rigidbody.linearVelocity;
            vel.y = force;
            Rigidbody.linearVelocity = vel;
        }

        public Vector3 GetMoveDirection()
        {
            var moveInput = GetMoveInput();
            var camForward = Camera.transform.forward.normalized;
            var camRight = Camera.transform.right.normalized;

            camForward.y = 0f;
            camRight.y = 0f;

            var moveDirection = (camForward * moveInput.y) + (camRight * moveInput.x);

            return moveDirection.normalized;
        }


        public void HandleMove(float speed, Vector3 moveDirection)
        {
            var targetVelocity = moveDirection * speed;
            targetVelocity.y = IsGround() && verticalVelocity <= 0 ? 0f : GetVerticalVelocity();
            Rigidbody.linearVelocity = targetVelocity;
            RotateToDirection(moveDirection);
        }

        public void HandleDeceleration(float deceleration)
        {
            var currentVelocity = Rigidbody.linearVelocity;
            currentVelocity.y = 0f;

            var decelerationVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, deceleration * Time.deltaTime);
            decelerationVelocity.y = IsGround() && verticalVelocity <= 0 ? 0f : GetVerticalVelocity();

            Rigidbody.linearVelocity = decelerationVelocity;
        }

        public bool CanDash()
        {
            return GetDashInput() && IsGround() && Time.time - lastDashTime >= DashCooldown;
        }

        public void StartDash()
        {
            lastDashTime = Time.time;
        }

        public void UpdateCoyoteTime()
        {
            if (IsGround())
            {
                coyoteTimer = CoyoteTime;
            }
            else
            {
                coyoteTimer -= Time.deltaTime;
            }
        }

        public void ResetCoyoteTime() => coyoteTimer = 0f;
        public bool IsCoyoteTimeValid() => coyoteTimer > 0f;

        public void UpdateJumpBuffer()
        {
            if (GetJumpInput())
            {
                jumpBufferTimer = JumpBufferTime;
            }
            else
            {
                jumpBufferTimer -= Time.deltaTime;
            }
        }

        public bool IsJumpBufferValid() => jumpBufferTimer > 0f;
        public void ResetJumpBuffer() => jumpBufferTimer = 0f;

        public void StartTrackingFall()
        {
           fallStartY = Position.y;
        }

        public float GetFallDistance()
        {
            return Mathf.Max(0f, fallStartY - Position.y);
        }

        public bool IsHardLanding()
        {
            return GetFallDistance() >= HardLandingThreshold;
        }
    }
}
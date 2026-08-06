using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
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

        [SerializeField] InputActionReference interactInput;
        [SerializeField] InputActionReference escapeIpput;

        [SerializeField] Camera cam;
        [SerializeField] LayerMask groundLayer = ~0;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private Transform grapContainer;

        private PlayerRigAnim RigAnim => gameObject.GetAndCacheComponent(ref rigAnim);

        public float JumpForce => playerData.JumpForce;
        public float StoppingDeceleration => playerData.StoppingDeceleration;
        public float DashSpeed => playerData.DashSpeed;
        public float DashDuration => playerData.DashDuration;
        public float DashCooldown => playerData.DashCooldown;

        public float AirControlRatio => playerData.AirControlRatio;
        public float LightLandingDuration => playerData.LightLandingDuration;
        public float HardLandingDuration => playerData.HardLandingDuration;
        public float HardLandingThreshold => playerData.HardLandingThreshold;

        private float CoyoteTime => playerData.CoyoteTime;
        private float JumpBufferTime => playerData.JumpBufferTime;

        private float maxSlopeAngle => playerData.MaxSlopeAngle;
        private float slopeSnapForce => playerData.SlopeSnapForce;
        private AnimationCurve slopeJumpCurve => playerData.SlopeJumpCurve;

        public bool IsHoldItem => HeldItem != null;

        public IGrabInteractable HeldItem => heldItem;


        public Camera Camera => cam;

        private PlayerRigAnim rigAnim;

        private RaycastHit groundHit;
        private float currentSlopeAngle;

        protected BaseStateMachine<IPlayer> stateMachine;

        private float verticalVelocity;
        private float lastDashTime;
        private float coyoteTimer;
        private float jumpBufferTimer;
        private float fallStartY;

        private IGrabInteractable heldItem;
        private IEscapeInteratable escItem;
        private List<IInteractable> activeInteractables = new ();


        public override void Initialize()
        {
            SetupData(playerData.ActorData);
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
                    new PlayerRollingState(),
                    new PlayerDashState(),
                    new PlayerLightStoppingState(),
                    new PlayerHardStoppingState()
                }
            );

            stateMachine.ChangeState<PlayerIdleState>();

            playerInput.Enable();
            RigAnim.SetAnim(RigAnimation.None);

            if (Rigidbody != null)
            {
                Rigidbody.freezeRotation = true;
            }
        }

        public override void ListenEvents()
        {
            base.ListenEvents();
            interactInput.action.started += HandleInteract;
            escapeIpput.action.started += HandleEscape;
        }

        public override void StopListenEvents()
        {
            base.StopListenEvents();
            interactInput.action.started -= HandleInteract;
            escapeIpput.action.started -= HandleEscape;

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
                verticalVelocity += MathUtils.Gravity * Time.deltaTime;
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
            float radius = CapsuleCollider != null ? CapsuleCollider.radius * 0.9f : 0.2f;
            Vector3 rayOrigin = transform.position + Vector3.up * (radius + 0.05f);
            int mask = groundLayer & ~(1 << gameObject.layer);
            if (Physics.SphereCast(rayOrigin, radius, Vector3.down, out groundHit, 0.2f, mask, QueryTriggerInteraction.Ignore))
            {
                currentSlopeAngle = Vector3.Angle(Vector3.up, groundHit.normal);
                return true;
            }

            currentSlopeAngle = 0f;
            return false;
        }

        public bool IsExceedingMaxSlope()
        {
            return IsGround() && currentSlopeAngle > maxSlopeAngle;
        }

        public Vector3 GetGroundNormal() => groundHit.normal;
        public float GetSlopeAngle() => currentSlopeAngle;

        public float GetSlopeJumpMultiplier()
        {
            if (slopeJumpCurve == null || slopeJumpCurve.length == 0)
                return 1f;
            float mult = slopeJumpCurve.Evaluate(currentSlopeAngle);
            return mult <= 0f ? 1f : mult;
        }

        private float currentAnimSpeed;

        public void UpdateLocomotionAnimation(float targetSpeedNormalized)
        {
            currentAnimSpeed = Mathf.MoveTowards(currentAnimSpeed, targetSpeedNormalized, Time.deltaTime * 6f);
            Animator.SetFloat(AnimHash.Speed, currentAnimSpeed);
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
            if (moveInput.sqrMagnitude < 0.01f) return Vector3.zero;

            var camForward = Camera.transform.forward;
            var camRight = Camera.transform.right;

            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            var moveDirection = (camForward * moveInput.y) + (camRight * moveInput.x);

            return moveDirection.normalized;
        }


        public void HandleMove(float speed, Vector3 moveDirection, bool allowRotation = true)
        {
            if (IsGround() && verticalVelocity <= 0)
            {
                if (IsExceedingMaxSlope())
                {
                    var slideDirection = new Vector3(groundHit.normal.x, -groundHit.normal.y, groundHit.normal.z);
                    Rigidbody.linearVelocity = slideDirection * speed;
                    return;
                }

                var slopeMoveDir = Vector3.ProjectOnPlane(moveDirection, groundHit.normal).normalized;
                var targetVelocity = slopeMoveDir * speed;

                if (verticalVelocity <= 0)
                {
                    targetVelocity.y = Mathf.Min(targetVelocity.y, -1f);
                }

                Rigidbody.linearVelocity = targetVelocity;
            }
            else
            {
                var targetVelocity = moveDirection * speed;
                targetVelocity.y = GetVerticalVelocity();
                Rigidbody.linearVelocity = targetVelocity;
            }

            if (allowRotation)
            {
                RotateToDirection(moveDirection);
            }
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

        public override void SetCanInteractor(bool canInteract, IInteractable interactable)
        {
            base.SetCanInteractor(canInteract, interactable);

            if (canInteract)
            {
                if (!activeInteractables.Contains(interactable))
                {
                    activeInteractables.Add(interactable);
                }
            }
            else
            {
                activeInteractables.Remove(interactable);
            }

            UpdateInteractableUI();
        }

        public void HandleInteract(InputAction.CallbackContext ctx) 
        {
            var interactable = GetPrimaryInteractable();
            if (interactable == null)
            {
                UpdateInteractableUI();
                return;
            }
          
            if (interactable is IGrabInteractable grabInteractable)
            {
                if (heldItem == grabInteractable)
                {
                    RigAnim.SetAnim(RigAnimation.None);
                    grabInteractable.Drop();
                    heldItem = null;
                }
                else if (grabInteractable.State == GrabState.None)
                {
                    if (heldItem != null)
                    {
                        heldItem.Drop();
                        heldItem = null;
                    }

                    RigAnim.SetAnim(RigAnimation.Grab);
                    grabInteractable.Pick(grapContainer);
                    heldItem = grabInteractable;
                }    
            }
            else 
            {
                interactable.OnInteract(this);
            }

            UpdateInteractableUI();
        }

        public IInteractable GetPrimaryInteractable()
        {
            activeInteractables.RemoveAll(x => x == null);

            foreach (var interactable in activeInteractables)
            {
                if (interactable is not IGrabInteractable) return interactable;
            }

            foreach (var interactable in activeInteractables)
            {
                if (interactable is IGrabInteractable grab && grab.State == GrabState.None && !IsHoldItem)
                {
                    return interactable;
                }
            }


            if (IsHoldItem) return HeldItem;

            return null;
        }

        void UpdateInteractableUI()
        {
            var interactable = GetPrimaryInteractable();

            if (interactable != null && interactable.CanInteract())
            {
                this.Raise(new ShowInteractUI(true, interactable));
            }
            else
            {
                this.Raise(new ShowInteractUI(false, null));
            }

        }

        void HandleEscape(InputAction.CallbackContext ctx)
        {
            if (escItem != null)
            {
                escItem.Escape();
            }
        }
    }
}
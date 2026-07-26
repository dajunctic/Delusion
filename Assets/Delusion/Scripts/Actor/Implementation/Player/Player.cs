using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dajunctic
{
    public class Player: Actor, IPlayer
    {
        [SerializeField] InputActionAsset playerInput;
        [SerializeField] InputActionReference moveInput;
        [SerializeField] InputActionReference jumpInput;
        [SerializeField] InputActionReference lookInput;
        [SerializeField] InputActionReference sprintInput;
        [SerializeField] InputActionReference dashInput;

        [SerializeField] Camera cam;
        public Camera Camera => cam;

        protected BaseStateMachine<IPlayer> stateMachine;

        private float verticalVelocity;
        private float lastDashTime;

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
            stateMachine.Tick();
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
            return CharacterController.isGrounded;
            // Vector3 sphereOrigin = Position + Vector3.up * 0.2f;
            // return Physics.SphereCast(sphereOrigin, 0.3f, Vector3.down, out var hit,  0.25f);
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
            if (IsGround() && verticalVelocity <= 0)
            {
                verticalVelocity = -2f;
            }
            else
            {
                verticalVelocity += Gravity * Time.deltaTime;
            }

            return verticalVelocity;
        }

        public void AddForceVerticalVelocity(float force)
        {
            verticalVelocity = force;
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
            var moveSpeed = speed * Time.deltaTime;
            var verticalVelocity = GetVerticalVelocity() * Time.deltaTime;

            CharacterController.Move(moveSpeed * moveDirection + new Vector3(0f, verticalVelocity, 0f));
            RotateToDirection(moveDirection);
        }

        public void HandleDeceleration(float deceleration)
        {
            var currentVelocity = CharacterController.velocity;
            currentVelocity.y = 0;

            var decelerationVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, deceleration);
            decelerationVelocity.y = GetVerticalVelocity();

            CharacterController.Move(decelerationVelocity * Time.deltaTime);
        }

        public bool CanDash()
        {
            return GetDashInput() && IsGround() && Time.time - lastDashTime >= DashCooldown;
        }

        public void StartDash()
        {
            lastDashTime = Time.time;
        }
    }
}
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

        [SerializeField] Camera cam;
        public Camera Camera => cam;

        protected BaseStateMachine<IPlayer> stateMachine;

        public int SpeedHash => Animator.StringToHash("Speed");

        private float verticalVelocity;

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
                    new PlayerFallState()
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

        public bool IsGround()
        {
            Vector3 sphereOrigin = Position + Vector3.up * 0.2f;
            return Physics.SphereCast(sphereOrigin, 0.3f, Vector3.down, out var hit,  0.25f);
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
            verticalVelocity += force;
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


        public void HandleMove(float speed)
        {
            var moveDirection = GetMoveDirection();
            var moveSpeed = speed * Time.deltaTime;
            var verticalVelocity = GetVerticalVelocity();

            CharacterController.Move(moveSpeed * moveDirection + new Vector3(0f, verticalVelocity, 0f));
            Rotate(moveDirection, 0.3f);
        }
    }
}
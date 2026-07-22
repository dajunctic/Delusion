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

        protected BaseStateMachine<IPlayer> stateMachine;

        public int SpeedHash => Animator.StringToHash("Speed");

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
                    new PlayerRunState()
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
    }
}
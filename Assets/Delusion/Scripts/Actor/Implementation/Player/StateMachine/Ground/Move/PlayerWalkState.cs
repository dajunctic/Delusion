using UnityEngine;

namespace Dajunctic
{
    public class PlayerWalkState: PlayerMovingState
    {

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Tick()
        {
            base.Tick();

            var moveInput = context.GetMoveInput();
            var sprintInput = context.GetSprintInput();

            if (moveInput.sqrMagnitude < 0.01f)
            {
                stateMachine.ChangeState<PlayerLightStoppingState>();
                return;
            }

            if (sprintInput)
            {
                stateMachine.ChangeState<PlayerRunState>();
                return;
            }

            context.HandleMove(GetSpeed(), context.GetMoveDirection());
        }
    }
}
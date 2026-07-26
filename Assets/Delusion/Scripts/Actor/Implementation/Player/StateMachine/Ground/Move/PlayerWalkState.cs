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
                if (movingTimer >= 0.25f)
                {
                    stateMachine.ChangeState<PlayerLightStoppingState>();
                }
                else
                {
                    stateMachine.ChangeState<PlayerIdleState>();
                }
                return;
            }

            if (sprintInput)
            {
                stateMachine.ChangeState<PlayerRunState>();
                return;
            }

            context.UpdateLocomotionAnimation(0.5f);
            context.HandleMove(GetSpeed(), context.GetMoveDirection());
        }
    }
}
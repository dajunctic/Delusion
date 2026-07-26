using UnityEngine;

namespace Dajunctic
{
    public class PlayerRunState: PlayerMovingState
    {
        public override void Enter()
        {
            base.Enter();
            context.PlayAnimation(AnimHash.Run);
        }

        public override void Exit()
        {
            base.Exit();
            context.StopAnimation(AnimHash.Run);
        }

        public override void Tick()
        {
            base.Tick();

            var moveInput = context.GetMoveInput();
            var sprintInput = context.GetSprintInput();

            if (moveInput.sqrMagnitude < 0.01f)
            {
                stateMachine.ChangeState<PlayerHardStoppingState>();
                return;
            }

            if (!sprintInput)
            {
                stateMachine.ChangeState<PlayerWalkState>();
                return;
            } 

            context.HandleMove(GetSpeed(), context.GetMoveDirection());
        }
    }
}
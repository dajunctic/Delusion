using UnityEngine;

namespace Dajunctic
{
    public class PlayerIdleState: PlayerGroundState
    {
        public override void Enter()
        {
            base.Enter();
           
            context.PlayAnimation(AnimHash.Idle);
        }

        public override void Exit()
        {
            base.Exit();
            context.StopAnimation(AnimHash.Idle);
        }

        public override void Tick()
        {
            base.Tick();

            var moveInput = context.GetMoveInput();
            var sprintInput = context.GetSprintInput();

            if (moveInput.sqrMagnitude >= 0.01f && context.IsGround())
            {
                if (sprintInput)
                {
                    stateMachine.ChangeState<PlayerRunState>();
                }
                else
                {
                    stateMachine.ChangeState<PlayerWalkState>();
                }

                return;
            }

            context.UpdateLocomotionAnimation(0f);
            context.HandleMove(GetSpeed(), context.GetMoveDirection());
        }   

    }
}
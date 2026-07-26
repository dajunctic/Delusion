using UnityEngine;

namespace Dajunctic
{
    public class PlayerGroundState: BaseState<IPlayer>
    {

        public override void Enter()
        {
            base.Enter();
            context.PlayAnimation(AnimHash.Grounded);
        }

        public override void Exit()
        {
            base.Exit();
            context.StopAnimation(AnimHash.Grounded);
        }


        public override void Tick()
        {
            base.Tick();

            if (context.GetJumpInput())
            {
                stateMachine.ChangeState<PlayerJumpState>();
                return;
            }

            if (context.CanDash())
            {
                stateMachine.ChangeState<PlayerDashState>();
            }

            if (!context.IsGround())
            {
                stateMachine.ChangeState<PlayerFallState>();
                return;
            }
        }

        
        protected float GetSpeed()
        {
             var moveInput = context.GetMoveInput();
            var sprintInput = context.GetSprintInput();

            return moveInput.sqrMagnitude >= 0.01f ? (sprintInput ? context.RunSpeed : context.WalkSpeed): 0f;
        }

    }
}
using UnityEngine;

namespace Dajunctic
{
    public class PlayerGroundState: BaseState<IPlayer>
    {

        public override void Enter()
        {
            base.Enter();
            context.PlayAnimation(AnimHash.Grounded);
            context.StopAnimation(AnimHash.Airborne);
        }

        public override void Exit()
        {
            base.Exit();
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
                return;
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
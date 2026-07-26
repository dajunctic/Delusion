using UnityEngine;

namespace Dajunctic
{
    public class PlayerDashState: PlayerGroundState
    {
        private float dashCountdown;
        private Vector3 dashDirection;

        public override void Enter()
        {
            base.Enter();

            context.StartDash();
            dashCountdown = context.DashDuration;

            var moveInput = context.GetMoveInput();

            dashDirection =  moveInput.sqrMagnitude >= 0.01f ? context.GetMoveDirection() : context.CachedTransform.forward;

            context.PlayAnimation(AnimHash.Dash);
            context.Animator.Play("Dash", -1, 0f);
            context.Animator.Update(0f);
        }

        public override void Exit()
        {
            base.Exit();
            context.StopAnimation(AnimHash.Dash);
        }

        public override void Tick()
        {
            base.Tick();
            dashCountdown -= Time.deltaTime;

            var moveInput = context.GetMoveInput();
            var sprintInput = context.GetSprintInput();

            context.HandleMove(context.DashSpeed, dashDirection);
            if (dashCountdown <= 0)
            {
                if (moveInput.sqrMagnitude >= 0.01f)
                {
                    if (sprintInput)
                    {
                        stateMachine.ChangeState<PlayerRunState>();
                    }
                    else
                    {
                        stateMachine.ChangeState<PlayerWalkState>();
                    }
                }
                else
                {
                    stateMachine.ChangeState<PlayerHardStoppingState>();
                }
            }
        }
    }
}
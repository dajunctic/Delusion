using UnityEngine;

namespace Dajunctic
{
    public class PlayerLandingState: PlayerGroundState
    {
        private float landingTimer;
        private bool isHardLanding;

        public override void Enter()
        {
            base.Enter();

            isHardLanding = context.IsHardLanding();
            landingTimer = isHardLanding ? context.HardLandingDuration: context.LightLandingDuration;

            context.PlayAnimation(AnimHash.Landing);

            if (isHardLanding)
            {
                context.PlayAnimation(AnimHash.HardLanding);
            }
            else
            {
                context.PlayAnimation(AnimHash.LightLanding);
            }

        }

        public override void Exit()
        {
            base.Exit();
            context.StopAnimation(AnimHash.Landing);

            if (isHardLanding)
            {
                context.StopAnimation(AnimHash.HardLanding);
            }
            else
            {
                context.StopAnimation(AnimHash.LightLanding);
            }
        }

        public override void Tick()
        {
            base.Tick();

            landingTimer -= Time.deltaTime;

            var moveInput = context.GetMoveInput();
            var sprintInput = context.GetSprintInput();

            if (!isHardLanding && moveInput.sqrMagnitude >= 0.01f)
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

            if (landingTimer <= 0f)
            {
                stateMachine.ChangeState<PlayerIdleState>();
            }
        }
    }
}
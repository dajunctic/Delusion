namespace Dajunctic
{
    public class PlayerFallState: PlayerAirborneState
    {
        public override void Enter()
        {
            base.Enter();

            context.StartTrackingFall();
        }

        public override void Tick()
        {
            base.Tick();

            var sprintInput = context.GetSprintInput();
            var moveInput = context.GetMoveInput();

            if (context.IsGround())
            {
                if (context.IsHardLanding() && moveInput.sqrMagnitude >= 0.01f)
                {
                    stateMachine.ChangeState<PlayerRollingState>();
                }
                else
                {
                    stateMachine.ChangeState<PlayerLandingState>();
                }
                return;
            }

            context.HandleMove(GetSpeed(), context.GetMoveDirection());
        }
    }
}
namespace Dajunctic
{
    public class PlayerFallState: PlayerAirborneState
    {
        public override void Tick()
        {
            base.Tick();

            var sprintInput = context.GetSprintInput();
            var moveInput = context.GetMoveInput();
            context.GetVerticalVelocity();

            if (context.IsGround())
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
                    stateMachine.ChangeState<PlayerIdleState>();
                }

                return;
            }

            context.HandleMove(GetSpeed(), context.GetMoveDirection());
        }
    }
}
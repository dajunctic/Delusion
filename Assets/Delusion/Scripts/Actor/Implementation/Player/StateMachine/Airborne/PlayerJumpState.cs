namespace Dajunctic
{
    public class PlayerJumpState: PlayerAirborneState
    {
        public override void Enter()
        {
            base.Enter();

            context.AddForceVerticalVelocity(context.JumpForce);
        }

        public override void Tick()
        {
            base.Tick();

            var verticalVelocity = context.GetVerticalVelocity();

            if (verticalVelocity <= 0)
            {
                stateMachine.ChangeState<PlayerFallState>();
            }

            context.HandleMove(0);
        }
    }
}
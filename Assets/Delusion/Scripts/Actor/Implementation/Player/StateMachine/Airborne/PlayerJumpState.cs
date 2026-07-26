namespace Dajunctic
{
    public class PlayerJumpState: PlayerAirborneState
    {
        public override void Enter()
        {
            base.Enter();

            context.AddForceVerticalVelocity(context.JumpForce);

            context.PlayAnimation(AnimHash.Jump);

            context.ResetCoyoteTime();
            context.ResetJumpBuffer();
        }

        public override void Exit()
        {
            base.Exit();

            context.StopAnimation(AnimHash.Jump);
        }

        public override void Tick()
        {
            base.Tick();

            var verticalVelocity = context.GetVerticalVelocity();

            if (verticalVelocity <= 0)
            {
                stateMachine.ChangeState<PlayerFallState>();
                return;
            }

            context.HandleMove(GetSpeed(), context.GetMoveDirection());
        }
    }
}
namespace Dajunctic
{
    public class PlayerJumpState: PlayerAirborneState
    {
        public override void Enter()
        {
            base.Enter();

            context.AddForceVerticalVelocity(context.JumpForce);

            context.Animator.SetBool(AnimHash.Jump, true);
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
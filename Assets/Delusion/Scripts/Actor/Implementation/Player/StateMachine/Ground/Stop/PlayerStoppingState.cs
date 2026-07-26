namespace Dajunctic
{
    public class PlayerStoppingState: PlayerGroundState
    {

        public override void Enter()
        {
            base.Enter();

            context.PlayAnimation(AnimHash.Stop);
        }

        public override void Exit()
        {
            base.Exit();
            context.StopAnimation(AnimHash.Stop);
        }

        public override void Tick()
        {
            base.Tick();

            var moveInput = context.GetMoveInput();
            var sprintInput = context.GetSprintInput();

            if (moveInput.sqrMagnitude >= 0.01)
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
            
            context.HandleDeceleration(context.StoppingDeceleration);

            var horizontalVelocity = context.CharacterController.velocity;
            horizontalVelocity.y = 0f;

            if (horizontalVelocity.sqrMagnitude <= 0.01)
            {
                stateMachine.ChangeState<PlayerIdleState>();
            }

        }
    }
}
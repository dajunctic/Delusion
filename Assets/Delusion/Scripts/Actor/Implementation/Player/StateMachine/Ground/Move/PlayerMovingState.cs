namespace Dajunctic
{
    public class PlayerMovingState: PlayerGroundState
    {
        public override void Enter()
        {
            base.Enter();
            context.PlayAnimation(AnimHash.Move);
        }

        public override void Exit()
        {
            base.Exit();
            context.StopAnimation(AnimHash.Move);
        }
    }
}
namespace Dajunctic
{
    public class PlayerLandingState: PlayerGroundState
    {
        public override void Enter()
        {
            base.Enter();
            context.PlayAnimation(AnimHash.Landing);
        }

        public override void Exit()
        {
            base.Exit();
            context.StopAnimation(AnimHash.Landing);
        }
    }
}
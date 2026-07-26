namespace Dajunctic
{
    public class PlayerLightStoppingState: PlayerStoppingState
    {
        public override void Enter()
        {
            base.Enter();
            context.PlayAnimation(AnimHash.LightStop);
        }

        public override void Exit()
        {
            base.Exit();
            context.StopAnimation(AnimHash.LightStop);
        }
    }
}
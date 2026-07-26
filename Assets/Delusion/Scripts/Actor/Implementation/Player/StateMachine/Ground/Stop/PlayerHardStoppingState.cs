namespace Dajunctic
{
    public class PlayerHardStoppingState: PlayerStoppingState
    {
        public override void Enter()
        {
            base.Enter();
            context.PlayAnimation(AnimHash.HardStop);
        }

        public override void Exit()
        {
            base.Exit();
            context.StopAnimation(AnimHash.HardStop);
        }
    }
}
namespace Dajunctic
{
    public class PlayerMovingState: PlayerGroundState
    {
        protected float movingTimer;

        public override void Enter()
        {
            base.Enter();
            movingTimer = 0f;
            context.PlayAnimation(AnimHash.Move);
        }

        public override void Exit()
        {
            base.Exit();
            context.StopAnimation(AnimHash.Move);
        }

        public override void Tick()
        {
            base.Tick();
            movingTimer += UnityEngine.Time.deltaTime;
        }
    }
}
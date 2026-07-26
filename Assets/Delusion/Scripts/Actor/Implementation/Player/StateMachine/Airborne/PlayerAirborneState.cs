namespace Dajunctic
{
    public class PlayerAirborneState: BaseState<IPlayer>
    {

        public override void Enter()
        {
            base.Enter();
            context.PlayAnimation(AnimHash.Airborne);
        }

        public override void Exit()
        {
            base.Exit();
            context.StopAnimation(AnimHash.Airborne);
        }
        
        protected float GetSpeed()
        {
             var moveInput = context.GetMoveInput();
            var sprintInput = context.GetSprintInput();

            var baseSpeed =  moveInput.sqrMagnitude >= 0.01f ? (sprintInput ? context.RunSpeed : context.WalkSpeed): 0f;

            return baseSpeed * context.AirControlRatio;
        }
    }
}
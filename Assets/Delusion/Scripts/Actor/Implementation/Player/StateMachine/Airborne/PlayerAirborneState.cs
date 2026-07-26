namespace Dajunctic
{
    public class PlayerAirborneState: BaseState<IPlayer>
    {
        protected float GetSpeed()
        {
             var moveInput = context.GetMoveInput();
            var sprintInput = context.GetSprintInput();

            return moveInput.sqrMagnitude >= 0.01f ? (sprintInput ? context.RunSpeed : context.WalkSpeed): 0f;
        }
    }
}
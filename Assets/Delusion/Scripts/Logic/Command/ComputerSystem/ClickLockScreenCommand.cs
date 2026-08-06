namespace Dajunctic
{
    public class ClickLockScreenCommand : ICommand, ICanGetSystem
    {
        public CommandResult Execute()
        {
            var computerSystem = this.GetSystem<IComputerSystem>();

            if (computerSystem.State != ComputerState.Lock)
            {
                return CommandResult.Failed;
            }

            computerSystem.SetClickLockScreen(true);
            return CommandResult.Success;
        }
    }
}
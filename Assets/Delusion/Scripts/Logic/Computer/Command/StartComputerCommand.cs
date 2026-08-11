namespace Dajunctic
{

    public class StartComputerCommand : ICommand, ICanGetSystem
    {
        CommandResult ICommand.Execute()
        {
             var system = this.GetSystem<IComputerSystem>();
            
            if (system.IsBooted)
            {
                return CommandResult.Failed;
            }

            _ = system.StartComputer();
            return CommandResult.Success;
        }
    }
}
namespace Dajunctic
{
    public class LoginDesktopCommand : ICommand, ICanGetSystem
    {
        string _password;

        public LoginDesktopCommand(string password)
        {
            _password = password;
        }

        public CommandResult Execute()
        {
            var system = this.GetSystem<IComputerSystem>();

            var login = system.Login(_password);

            if (login)
            {
                system.SetLoginSuccess(true);
                return CommandResult.Success;
            }

            return CommandResult.Failed;
        }
    }
}
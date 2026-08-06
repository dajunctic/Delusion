namespace Dajunctic
{
    public interface ICommandProcessor
    {
        CommandResult SendCommand(ICommand command);
    }

    public static class CommandExtensions
    {
        public static CommandResult SendCommand(this ICanSendCommand obj, ICommand command)
        {
            return IApplication.Instance.SendCommand(command);
        }
    }
}
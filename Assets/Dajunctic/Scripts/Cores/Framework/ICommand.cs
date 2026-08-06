namespace Dajunctic
{
    public interface ICommand
    {
        CommandResult Execute();
    }

    public enum CommandResult
    {
        Success,
        Failed
    }
}
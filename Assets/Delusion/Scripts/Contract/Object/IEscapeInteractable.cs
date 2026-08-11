namespace Dajunctic
{
    public interface IEscapeInteratable: IInteractable, ICanSendEvent
    {
        void Escape();
    }
}
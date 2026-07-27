namespace Dajunctic
{
    public interface IToggleInteractable: IInteractable
    {
        bool CanToggle();
        void Toggle();
    }
}
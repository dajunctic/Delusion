namespace Dajunctic
{
    public class ToggleInteractableObject : BaseInteractableObject, IToggleInteractable
    {
        public virtual bool CanToggle() => false;
        public virtual void Toggle() {}
    }
}
namespace Dajunctic
{
    public interface IInteractor: IEntity
    {
        void SetCanInteractor(bool canInteract, IInteractable interactable);
    }
}
namespace Dajunctic
{
    public interface IInteractable: IEntity, ITransform, IAnimator
    {
        string GetInteractDecs();
        bool CanInteract();
        void OnInteract(IInteractor interactor);
        void OnStopInteract(IInteractor interactor);
    }
}
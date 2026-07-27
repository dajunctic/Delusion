namespace Dajunctic
{
    public interface IInteractable: IEntity, ITransform, IAnimator
    {
        string GetInteractDecs();

        void OnInteract(IInteractor interactor);
        void OnStopInteract(IInteractor interactor);
    }
}
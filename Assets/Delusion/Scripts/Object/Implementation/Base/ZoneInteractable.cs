using UnityEngine;

namespace Dajunctic
{
    public class ZoneInteractable: BaseMono
    {
        [SerializeField] LayerMask interactableLayer;
        [SerializeField] BaseInteractableObject interactable;

        void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & interactableLayer) == 0) return;
 
            if (other.TryGetComponent<IInteractor>(out var interactor))
            {
                interactor.SetCanInteractor(true, interactable);
                this.Raise(new ShowInteractUI(true, interactable));
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (((1 << other.gameObject.layer) & interactableLayer) == 0) return;

            if (other.TryGetComponent<IInteractor>(out var interactor))
            {
                interactor.SetCanInteractor(false, interactable);
                this.Raise(new ShowInteractUI(false, interactable));
            }
        }
    }
}
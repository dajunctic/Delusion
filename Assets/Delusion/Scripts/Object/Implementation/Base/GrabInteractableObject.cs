using UnityEngine;

namespace Dajunctic
{
    public class GrabInteractableObject : BaseInteractableObject, IGrabInteractable
    {
        public Rigidbody Rigidbody => gameObject.GetAndCacheComponent(ref rb);
        public Collider Collider => gameObject.GetAndCacheComponent(ref col);
        private Rigidbody rb;
        private Collider col;

        public GrabState State => state;

        private GrabState state;

        public override bool CanInteract()
        {
            return true;
        }

        public override string GetInteractDecs()
        {
            return state == GrabState.None ? "Press E to pick" : "Press E to drop";
        }

        public override void OnInteract(IInteractor interactor)
        {
            base.OnInteract(interactor);
        }

        public override void OnStopInteract(IInteractor interactor)
        {
            base.OnStopInteract(interactor);
        }

        public virtual void Drop()
        {
            CachedTransform.SetParent(null);
            state = GrabState.None;
            Rigidbody.isKinematic = false;
            Collider.isTrigger = false;
        }

        public virtual void Pick(Transform container)
        {
            CachedTransform.SetParent(container);
            CachedTransform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            Rigidbody.isKinematic = true;
            Collider.isTrigger = true;
            state = GrabState.Picked;
        }
    }
}
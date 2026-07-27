using UnityEngine;

namespace Dajunctic
{
    public abstract class BaseInteractableObject : BaseMono, IInteractable
    {
        public virtual Transform CachedTransform => gameObject.GetAndCacheComponent(ref mTransform);
        public virtual Vector3 Position => CachedTransform.position;
        public virtual Vector3 Forward => CachedTransform.forward;

        public Animator Animator => gameObject.GetAndCacheComponent(ref animator);

        Transform mTransform;
        Animator animator;

        public virtual void RotateToDirection(Vector3 direction)
        {
           
        }

        public virtual void PlayAnimation(int animHash)
        {
            
        }

        public virtual void StopAnimation(int animHash)
        {
            
        }

        public virtual string GetInteractDecs() => string.Empty;

        public virtual void OnInteract(IInteractor interactor) {}

        public virtual void OnStopInteract(IInteractor interactor) {}
    }
}
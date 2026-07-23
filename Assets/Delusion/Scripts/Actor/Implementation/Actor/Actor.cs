using UnityEngine;

namespace Dajunctic
{
    public abstract class Actor: BaseMono, IActor
    {
        public Animator Animator => gameObject.GetAndCacheComponent(ref animator);
        public CharacterController CharacterController => gameObject.GetAndCacheComponent(ref characterController);
        public Transform CachedTransform => gameObject.GetAndCacheComponent(ref mTransform);

        public virtual float WalkSpeed => 2.0f;
        public virtual float RunSpeed => 5.335f;
        public virtual float RotateSpeed => 5f;
        public virtual float Gravity => -9.81f;
        public virtual float JumpForce => 1.5f;


        public virtual Vector3 Position => CachedTransform.position;
        public virtual Vector3 Forward => CachedTransform.forward;

        protected Animator animator;
        protected CharacterController characterController; 
        private Transform mTransform;


        public override void Initialize()
        {
            base.Initialize();
        }

        public void Rotate(Vector3 direction, float dt)
        {
            CachedTransform.forward = Vector3.Lerp(CachedTransform.forward, direction, dt);
        }
    }
}
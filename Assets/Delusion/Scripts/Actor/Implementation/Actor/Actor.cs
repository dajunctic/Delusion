using UnityEngine;

namespace Dajunctic
{
    public abstract class Actor : BaseMono, IActor
    {
        public Animator Animator => gameObject.GetAndCacheComponent(ref animator);
        public Rigidbody Rigidbody => gameObject.GetAndCacheComponent(ref rigidbodyComp);
        public CapsuleCollider CapsuleCollider => gameObject.GetAndCacheComponent(ref capsuleColliderComp);
        public Transform CachedTransform => gameObject.GetAndCacheComponent(ref mTransform);
        public Vector3 Velocity => Rigidbody.linearVelocity;

        public virtual float WalkSpeed => 2.0f;
        public virtual float RunSpeed => 5.335f;
        public virtual float RotateSpeed => 5f;
        public virtual float DashSpeed => 12f;
        public virtual float DashDuration => 0.35f;
        public virtual float DashCooldown => 2f;
        public virtual float StoppingDeceleration => 20f;
        public virtual float Gravity => -9.81f;
        public virtual float JumpForce => 6.5f;


        public virtual Vector3 Position => CachedTransform.position;
        public virtual Vector3 Forward => CachedTransform.forward;

        protected Animator animator;
        protected Rigidbody rigidbodyComp;
        protected CapsuleCollider capsuleColliderComp;
        private Transform mTransform;

        private float rotationVelocity;
        public float RotationSmoothTime = 0.12f;


        public override void Initialize()
        {
            base.Initialize();
        }

        public void RotateToDirection(Vector3 direction)
        {
            if (direction.sqrMagnitude < 0.01f) return;

            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            var rotation = Mathf.SmoothDampAngle(CachedTransform.eulerAngles.y, targetAngle, ref rotationVelocity, RotationSmoothTime);

            CachedTransform.rotation = Quaternion.Euler(0f, rotation, 0f);
        }

        public virtual void PlayAnimation(int animHash)
        {

        }

        public virtual void StopAnimation(int animHash)
        {

        }
    }
}
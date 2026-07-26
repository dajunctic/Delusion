using UnityEngine;

namespace Dajunctic
{
    public interface IActor: ITransform, IMovable, IAnimator, ISkillOwner, IDamageTaker, IDamageDealer
    {
        public Rigidbody Rigidbody { get; }
        public CapsuleCollider CapsuleCollider { get; }
        public Vector3 Velocity { get; }
        public float WalkSpeed {get; }
        public float RunSpeed {get; }
        public float RotateSpeed {get; }
        public float DashSpeed {get; }
        public float DashDuration { get; }
        public float DashCooldown {get; }
        public float StoppingDeceleration {get; }
        public float Gravity {get; }
        public float JumpForce {get; }
    }
}
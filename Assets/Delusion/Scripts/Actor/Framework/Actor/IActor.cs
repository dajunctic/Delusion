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
    }
}
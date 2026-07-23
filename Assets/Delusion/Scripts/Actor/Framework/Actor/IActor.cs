using UnityEngine;

namespace Dajunctic
{
    public interface IActor: ITransform, IMovable, IAnimationOwner, ISkillOwner, IDamageTaker, IDamageDealer
    {
        public CharacterController CharacterController {get; }
        public float WalkSpeed {get; }
        public float RunSpeed {get; }
        public float RotateSpeed {get; }
        public float Gravity {get; }
        public float JumpForce {get; }
    }
}
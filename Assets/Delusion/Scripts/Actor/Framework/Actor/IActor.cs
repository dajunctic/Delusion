using UnityEngine;

namespace Dajunctic
{
    public interface IActor: ITransform, IMovable, IAnimationOwner, ISkillOwner, IDamageTaker, IDamageDealer
    {
        public CharacterController CharacterController {get; }
        public float WalkSpeed {get; }
        public float RunSpeed {get; }
    }
}
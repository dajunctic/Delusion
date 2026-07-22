using UnityEngine;

namespace Dajunctic
{
    public interface IAnimationOwner: IEntity
    {
        public Animator Animator {get; }
    }
}
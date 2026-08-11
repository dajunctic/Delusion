using UnityEngine;

namespace Dajunctic
{
    public interface IAnimator: IEntity
    {
        public Animator Animator {get; }
        public void PlayAnimation(int animHash);
        public void StopAnimation(int animHash);
    }
}
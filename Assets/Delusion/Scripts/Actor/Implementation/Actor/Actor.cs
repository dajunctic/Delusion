using Unity.VisualScripting;
using UnityEngine;

namespace Dajunctic
{
    public abstract class Actor: BaseMono, IActor
    {
        
        public Animator Animator => gameObject.GetAndCacheComponent(ref animator);
        private Animator animator;

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
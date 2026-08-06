using UnityEngine;

namespace Dajunctic
{
    public class BootScreenUI: BaseMono
    {
        public Animator Animator => gameObject.GetAndCacheComponent(ref animator);
        private Animator animator;

        public override void ListenEvents()
        {
            base.ListenEvents();

            this.RegisterListener<ChangeComputerStateEvent>(OnChangeComputerState);
        }

         void OnChangeComputerState(ChangeComputerStateEvent @event)
        {
            if (@event.State == ComputerState.Boot)
            {
                Animator.Play(Animator.StringToHash("Start"));
            }
        }
    }
}
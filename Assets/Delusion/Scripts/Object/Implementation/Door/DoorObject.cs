using UnityEngine;

namespace Dajunctic
{
    public class DoorObject: ToggleInteractableObject
    {
        [SerializeField] string conditionTerm;
        [SerializeField] string openTerm;
        [SerializeField] string closeTerm;
        [SerializeField] float openDuration;
        [SerializeField] float closeDuration;


        private bool isOpen;
        private float lastTime;

        public bool IsConditionMeet => true;

        public override string GetInteractDecs()
        {
            return isOpen ? "Press E to close" : "Press E to open";
        }

        public override void PlayAnimation(int animHash)
        {
            Animator.Play(animHash, 0, 0f);
        }

        public override void StopAnimation(int animHash)
        {
            
        }

        public override bool CanToggle()
        {
            if (isOpen)
            {
                return Time.time - lastTime >= openDuration; 
            }
            else
            {
                return Time.time - lastTime >= closeDuration;
            }
        }

        public override void Toggle()
        {
            base.Toggle();
            Debug.LogError("ahoho");

            lastTime = Time.time;
            isOpen = !isOpen;

            PlayAnimation(isOpen ? AnimHash.Open: AnimHash.Close);
        }

        public override void OnInteract(IInteractor interactor)
        {
            base.OnInteract(interactor);
            if (!IsConditionMeet)
            {
                PlayAnimation(AnimHash.Jam);
            }

            if (CanToggle()) Toggle();
        }

        public override void OnStopInteract(IInteractor interactor)
        {
            base.OnStopInteract(interactor);
        }


    }
}
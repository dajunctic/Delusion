using UnityEngine;

namespace Dajunctic
{
    public class ComputerObject: BaseInteractableObject, IEscapeInteratable
    {
        [SerializeField] string openTerm;
        [SerializeField] string exitTerm;

        private bool isUsing = false;
        public bool IsBeingUsed => isUsing;

        public override bool CanInteract() => !isUsing;

        public override string GetInteractDecs()
        {
            return "Press E to use";
        }

        public override void OnInteract(IInteractor interactor)
        {
            base.OnInteract(interactor);

            if (isUsing)
            {
                return;
            }

            OpenComputer();
        }

        void OpenComputer()
        {
            isUsing = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            this.Raise(new ToggleComputerUIEvent()
            {
                Value = true,
            });

            this.Raise(new ShowHintUI()
            {
                Value = true,
                Decs = "[ESC] Exit"
            });
        }   

        public void Escape()
        {
            isUsing = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            this.Raise(new ToggleComputerUIEvent()
            {
                Value = false,
            });

            this.Raise(new ShowHintUI()
            {
                Value = false,
                Decs = string.Empty
            });
        }
    }

    public struct ToggleComputerUIEvent: IEvent
    {
        public bool Value;
    }
}
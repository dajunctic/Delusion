using TMPro;
using UnityEngine;

namespace Dajunctic
{
    public class InteractUI: BaseMono
    {
        [SerializeField] GameObject toggleUI;
        [SerializeField] TMP_Text decs;

        bool canShow;
        IInteractable interactable;

        public override void DoEnable()
        {
            base.DoEnable();
            toggleUI.SetActive(false);
        }

        public override void ListenEvents()
        {
            base.ListenEvents();
            this.RegisterListener<ShowInteractUI>(OnToggleInteractUI);
        }

        public override void StopListenEvents()
        {
            base.StopListenEvents();
            this.RemoveListener<ShowInteractUI>(OnToggleInteractUI);
        }

        public override void Tick()
        {
            base.Tick();

            if (interactable is null)
            {
                toggleUI.SetActive(false);
                return;
            }
            toggleUI.SetActive(canShow && interactable.CanInteract());
            decs.text = interactable.GetInteractDecs();
        }

        void OnToggleInteractUI(ShowInteractUI @event)
        {
            canShow = @event.Value;
            interactable = @event.Interactable;
        }
    }

    public struct ShowInteractUI: IEvent
    {
        public bool Value;
        public IInteractable Interactable;
        public ShowInteractUI(bool value, IInteractable interactable)
        {
            Value = value;
            Interactable = interactable;
        }
    }

}
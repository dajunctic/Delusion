using UnityEngine;

namespace Dajunctic
{
    public class ComputerUI: BaseMono, ICanSendEvent
    {
        [SerializeField] private CanvasGroup panel;
        [Header("Screens")]
        [SerializeField] private GameObject bootScreen;
        [SerializeField] private GameObject lockScreen;
        [SerializeField] private GameObject desktopScreen;
        [SerializeField] private GameObject blackScreen; 

        private bool isOpen;

        public override void Initialize()
        {
            base.Initialize();
            panel.alpha = 0;
            bootScreen.SetActive(true);
            lockScreen.SetActive(true);
            desktopScreen.SetActive(true);
            blackScreen.SetActive(true);
        }

        public override void ListenEvents()
        {
            base.ListenEvents();
            this.RegisterListener<ToggleComputerUIEvent>(OnToggleComputerUI);
            this.RegisterListener<ChangeComputerStateEvent>(OnChangeComputerState);
        }

        public override void StopListenEvents()
        {
            base.StopListenEvents();
            this.RemoveListener<ToggleComputerUIEvent>(OnToggleComputerUI);
            this.RemoveListener<ChangeComputerStateEvent>(OnChangeComputerState);

        }

        void OnToggleComputerUI(ToggleComputerUIEvent @event)
        {
            isOpen = @event.Value;

            panel.alpha = isOpen ? 1: 0;

            if (isOpen)
            {
                this.SendCommand(new StartComputerCommand());
            }           
        }

        void OnChangeComputerState(ChangeComputerStateEvent @event)
        {
            switch (@event.State)
            {
                case ComputerState.None:
                    blackScreen.transform.SetAsLastSibling();
                    break;
                case ComputerState.Boot:
                    bootScreen.transform.SetAsLastSibling();
                    break;
                case ComputerState.Lock:
                case ComputerState.Login:
                    lockScreen.transform.SetAsLastSibling();
                    break;
                case ComputerState.Desktop:
                    desktopScreen.transform.SetAsLastSibling();
                    break;
            }
        }
    }
}
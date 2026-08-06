using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dajunctic
{
    public class lockScreenUI: BaseMono
    {
        public Animator Animator => gameObject.GetAndCacheComponent(ref animator);
        private Animator animator;

        [SerializeField] private TMP_InputField inputField;

        private ComputerState state;

        public override void ListenEvents()
        {
            base.ListenEvents();

            this.RegisterListener<ChangeComputerStateEvent>(OnChangeComputerState);
        }

        void OnChangeComputerState(ChangeComputerStateEvent @event)
        {
            state = @event.State;
            if (@event.State == ComputerState.Lock)
            {
                Animator.Play(Animator.StringToHash("In"));
            }
            
            if (@event.State == ComputerState.Login)
            {
                Animator.Play(Animator.StringToHash("Password In"));
            }
        }

        public void OnClickLock()
        {
            this.SendCommand(new ClickLockScreenCommand());
        }

        public void OnclickLogin()
        {
            var result = this.SendCommand(new LoginDesktopCommand(inputField.text));

            if (result == CommandResult.Failed)
            {
                ShowLoginError();   
            }
        }

        void ShowLoginError()
        {
            
        }

        public override void Tick()
        {
            base.Tick();

            if (Input.anyKeyDown && state == ComputerState.Lock)
            {
                OnClickLock();
            }

            if (Input.GetKeyDown(KeyCode.Return) && state == ComputerState.Login)
            {
                OnClickLock();
            }
        }
        
    }
}
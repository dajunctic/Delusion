using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Dajunctic
{
    public class ComputerSystem : IComputerSystem
    {
        public bool Initialized => initialized;
        public bool IsBooted => isBooted;
        public ComputerState State => state;
        public event Action<ComputerState> OnStateChanged;

        private bool initialized;
        private ComputerState state;
        private bool isBooted;
        private bool isClickLockScreen;
        private bool isLoginSuccess;

        private ComputerConfig config;

        public void Initialize()
        {
            state = ComputerState.None;
            initialized = true;
        }

        public void CleanUp()
        {
            
        }

        public async UniTask StartComputer()
        {
            await Boot();
            await Lock();
            await Login();
            await Desktop();
        }

        async UniTask Boot()
        {
            isBooted = true;
            ChangeState(ComputerState.Boot);
            await UniTask.Delay(4000);
        }

        async UniTask Lock()
        {
            ChangeState(ComputerState.Lock);
            await UniTask.WaitUntil(()=> isClickLockScreen);
        }

        async UniTask Login()
        {
            ChangeState(ComputerState.Login);
            await UniTask.WaitUntil(()=> isLoginSuccess);
        }

        async UniTask Desktop()
        {
            ChangeState(ComputerState.Desktop);
            state = ComputerState.Desktop;
        }
    

        void ChangeState(ComputerState state)
        {
            this.state = state;
            this.Raise(new ChangeComputerStateEvent(state));
        }

        public void SetClickLockScreen(bool value)
        {
            isClickLockScreen = value;
        }

        public void SetLoginSuccess(bool value)
        {
            isLoginSuccess = value;
        }

        public bool Login(string password)
        {
            config = this.GetFirstConfig<ComputerConfig>();
            return config.Password == password;
        }
    }

    public struct ChangeComputerStateEvent: IEvent
    {
        public ComputerState State;

        public ChangeComputerStateEvent(ComputerState state)
        {
            State = state;
        }
    }
}
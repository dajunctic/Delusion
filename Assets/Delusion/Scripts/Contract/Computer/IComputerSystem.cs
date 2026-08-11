using System;
using Cysharp.Threading.Tasks;

namespace Dajunctic
{
    public interface IComputerSystem: ISystem, ICanGetConfig, ICanSendEvent
    {
        bool IsBooted {get; }
        ComputerState State {get; }
        UniTask StartComputer();
        bool Login(string password);
        void SetClickLockScreen(bool value);
        void SetLoginSuccess(bool value);
    }
}
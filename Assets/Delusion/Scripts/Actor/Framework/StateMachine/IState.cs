namespace Dajunctic
{
    public interface IState<T>
    {
        void Init(T context, IStateMachine<T> stateMachine);
        void Enter();
        void Tick();
        void FixedTick();
        void Exit();
    }
}
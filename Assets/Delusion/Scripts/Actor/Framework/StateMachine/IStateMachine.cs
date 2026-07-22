using System.Collections.Generic;

namespace Dajunctic
{
    public interface IStateMachine<T>
    {
        IState<T> CurrentState { get; }
        public void Initialize(T context, IEnumerable<IState<T>> states);
        public void ChangeState<TState>();

        void Tick();
        void FixedTick();
    }
}
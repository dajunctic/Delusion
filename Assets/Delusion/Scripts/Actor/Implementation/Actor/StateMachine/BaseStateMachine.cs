using System;
using System.Collections.Generic;

namespace Dajunctic
{
    public class BaseStateMachine<T> : IStateMachine<T>
    {
        public IState<T> CurrentState {get; private set; }
        private readonly Dictionary<Type, IState<T>> _states = new();

        public void Initialize(T context, IEnumerable<IState<T>> states)
        {
            foreach (var state in states)
            {
                state.Init(context, this);
                _states[state.GetType()] = state;
            }
        }

        public void ChangeState<TState>()
        {
            CurrentState?.Exit();
            CurrentState = _states[typeof(TState)];
            CurrentState.Enter();
        }

        public void Tick() => CurrentState?.Tick();

        public void FixedTick() => CurrentState?.FixedTick();
    }
}
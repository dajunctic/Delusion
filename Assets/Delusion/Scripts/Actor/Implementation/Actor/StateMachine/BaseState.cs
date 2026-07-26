using UnityEngine;

namespace Dajunctic
{
    public abstract class BaseState<T> : IState<T>
    {
        protected T context;
        protected IStateMachine<T> stateMachine;

        public void Init(T context, IStateMachine<T> stateMachine)
        {
           this.context = context;
           this.stateMachine = stateMachine;
        }

        public virtual void Enter()
        {
            Debug.Log(GetType());
        }

        public virtual void Exit()
        {
            
        }

        public virtual void FixedTick()
        {
            
        }

        public virtual void HandleInput()
        {
            
        }

        public virtual void Tick()
        {
            
        }
    }
}
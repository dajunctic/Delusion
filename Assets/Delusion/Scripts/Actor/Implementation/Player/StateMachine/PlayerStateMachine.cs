using UnityEngine;

namespace Dajunctic
{
    public class PlayerStateMachine<T>: BaseStateMachine<T>
    {
        public override void Tick()
        {
            base.Tick();

            Debug.LogError(CurrentState.GetType());
        }
    }
}
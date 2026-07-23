using UnityEngine;

namespace Dajunctic
{
    public class PlayerGroundState: BaseState<IPlayer>
    {

        public override void Tick()
        {
            base.Tick();

            if (context.GetJumpInput())
            {
                stateMachine.ChangeState<PlayerJumpState>();
                return;
            }

            if (!context.IsGround())
            {
                stateMachine.ChangeState<PlayerFallState>();
                return;
            }
        }

    }
}
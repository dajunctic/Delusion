using UnityEngine;

namespace Dajunctic
{
    public class PlayerIdleState: PlayerGroundState
    {
        public override void Enter()
        {
            base.Enter();
            context.Animator.SetFloat(context.SpeedHash, 0f);

        }

        public override void Tick()
        {
            base.Tick();

            var moveInput = context.GetMoveInput();
            var sprintInput = context.GetSprintInput();

            if (moveInput != Vector2.zero)
            {
                if (sprintInput)
                {
                    stateMachine.ChangeState<PlayerRunState>();
                }
                else
                {
                    stateMachine.ChangeState<PlayerWalkState>();
                }
            }

             
        }

    }
}
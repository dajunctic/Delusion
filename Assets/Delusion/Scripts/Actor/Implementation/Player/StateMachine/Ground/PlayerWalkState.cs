using Unity.VisualScripting;
using UnityEngine;

namespace Dajunctic
{
    public class PlayerWalkState: PlayerGroundState
    {

        public override void Enter()
        {
            base.Enter();
            context.Animator.SetFloat(context.SpeedHash, 0.5f);
        }

        public override void Tick()
        {
            base.Tick();

            var moveInput = context.GetMoveInput();
            var sprintInput = context.GetSprintInput();

            if (moveInput.sqrMagnitude < 0.01f)
            {
                stateMachine.ChangeState<PlayerIdleState>();
                return;
            }

            if (sprintInput)
            {
                stateMachine.ChangeState<PlayerRunState>();
                return;
            }

            context.HandleMove(context.WalkSpeed);
        }
    }
}
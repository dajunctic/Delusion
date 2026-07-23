using UnityEngine;

namespace Dajunctic
{
    public class PlayerRunState: PlayerGroundState
    {
        public override void Enter()
        {
            base.Enter();
            context.Animator.SetFloat(context.SpeedHash, 1f);
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

            if (!sprintInput)
            {
                stateMachine.ChangeState<PlayerWalkState>();
                return;
            } 

            context.HandleMove(context.RunSpeed);
        }
    }
}
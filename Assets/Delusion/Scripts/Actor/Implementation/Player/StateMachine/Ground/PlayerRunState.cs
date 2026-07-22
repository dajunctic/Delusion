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

            if (moveInput == Vector2.zero)
            {
                stateMachine.ChangeState<PlayerIdleState>();
                return;
            }

            if (!sprintInput)
            {
                stateMachine.ChangeState<PlayerWalkState>();
                return;
            } 

            var moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
            var moveSpeed = context.RunSpeed * Time.deltaTime;

            context.CharacterController.Move(moveSpeed * moveDirection);

            context.Rotate(moveDirection, 0.3f);
        }
    }
}
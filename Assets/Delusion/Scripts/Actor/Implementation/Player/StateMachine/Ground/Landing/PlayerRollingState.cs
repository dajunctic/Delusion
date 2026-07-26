using UnityEngine;

namespace Dajunctic
{
    public class PlayerRollingState : PlayerGroundState
    {
        private float rollDuration = 0.6f;
        private float rollTimer;

        public override void Enter()
        {
            base.Enter();
            rollTimer = rollDuration;
            context.PlayAnimation(AnimHash.Roll);
        }

        public override void Exit()
        {
            base.Exit();
            context.StopAnimation(AnimHash.Roll);
        }

        public override void Tick()
        {
            base.Tick();
            rollTimer -= Time.deltaTime;

            context.HandleMove(context.RunSpeed, context.GetMoveDirection());

            if (rollTimer <= 0f)
            {
                var moveInput = context.GetMoveInput();
                var sprintInput = context.GetSprintInput();

                if (moveInput.sqrMagnitude >= 0.01f)
                {
                    if (sprintInput)
                        stateMachine.ChangeState<PlayerRunState>();
                    else
                        stateMachine.ChangeState<PlayerWalkState>();
                }
                else
                {
                    stateMachine.ChangeState<PlayerIdleState>();
                }
            }
        }
    }
}

using UnityEngine;

namespace Dajunctic
{
    public interface IPlayer: IActor
    {
        public Camera Camera {get; }

        public bool IsGround();

        public Vector2 GetMoveInput();
        public bool GetSprintInput();
        public bool GetJumpInput();
        
        public float GetVerticalVelocity();
        public void AddForceVerticalVelocity(float force);
        public void HandleDeceleration(float deceleration);

        public Vector3 GetMoveDirection();
        public void HandleMove(float speed, Vector3 direction);
        public bool CanDash();
        public void StartDash();

    }
}
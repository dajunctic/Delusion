using UnityEngine;

namespace Dajunctic
{
    public interface IPlayer: IActor
    {
        public int SpeedHash { get; }
        public Camera Camera {get; }

        public bool IsGround();

        public Vector2 GetMoveInput();
        public bool GetSprintInput();
        public bool GetJumpInput();
        
        public float GetVerticalVelocity();
        public void AddForceVerticalVelocity(float force);

        
        public void HandleMove(float speed);

    }
}
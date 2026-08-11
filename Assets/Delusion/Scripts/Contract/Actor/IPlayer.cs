using UnityEngine;

namespace Dajunctic
{
    public interface IPlayer: IActor
    {
        public Camera Camera {get; }

        public bool IsHoldItem {get; }
        public IGrabInteractable HeldItem {get; }
        public IInteractable GetPrimaryInteractable();

        public bool IsGround();
        public bool IsExceedingMaxSlope();
        public float GetSlopeJumpMultiplier();
        public Vector3 GetGroundNormal();
        public float GetSlopeAngle();

        public Vector2 GetMoveInput();
        public bool GetSprintInput();
        public bool GetJumpInput();
        
        public float GetVerticalVelocity();
        public void AddForceVerticalVelocity(float force);
        public void HandleDeceleration(float deceleration);

        public Vector3 GetMoveDirection();
        public void HandleMove(float speed, Vector3 direction, bool allowRotation = true);
        public void UpdateLocomotionAnimation(float targetSpeedNormalized);
        public bool CanDash();
        public void StartDash();

        public float DashSpeed {get; }
        public float DashDuration {get; }
        public float DashCooldown {get; }
        public float StoppingDeceleration {get; }
        public float JumpForce {get; }

        public float AirControlRatio {get; }
        public float LightLandingDuration {get; }
        public float HardLandingDuration {get; }
        public float HardLandingThreshold {get; }

        public void UpdateCoyoteTime();
        public void ResetCoyoteTime();
        public bool IsCoyoteTimeValid();

        public void UpdateJumpBuffer();
        public bool IsJumpBufferValid();
        public void ResetJumpBuffer();


        public void StartTrackingFall();
        public float GetFallDistance();
        public bool IsHardLanding();

    }
}
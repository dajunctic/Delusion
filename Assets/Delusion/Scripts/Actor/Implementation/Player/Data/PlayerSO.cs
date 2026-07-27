using UnityEngine;

namespace Dajunctic
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Dajunctic/Data/PlayerData")]
    public class PlayerData : BaseSO
    {
        [SerializeField] private ActorData actorData;

        [Header("Stop")]
        [SerializeField] private float stoppingDeceleration = 20f;

        [Header("Dash")]
        [SerializeField] private float dashSpeed = 12f;
        [SerializeField] private float dashDuration = 0.35f;
        [SerializeField] private float dashCooldown = 2.0f;

        [Header("Airborne")]
        [SerializeField] private float jumpForce = 6.5f;
        [SerializeField] private float coyoteTime = 0.15f;
        [SerializeField] private float jumpBufferTime = 0.12f;
        [SerializeField] private float airControlRatio = 0.6f;

        [Header("Slope")]
        [SerializeField] private float maxSlopeAngle = 45f;
        [SerializeField] private float slopeSnapForce = 10f;
        [SerializeField] private AnimationCurve slopeJumpCurve = AnimationCurve.Linear(0f, 1f, 45f, 0.65f);

        [Header("Landing")]
        [SerializeField] private float lightLandingDuration = 0.15f;
        [SerializeField] private float hardLandingDuration = 0.5f;
        [SerializeField] private float hardLandingThreshold = 5f;

        public ActorData ActorData => actorData;

        public float JumpForce => jumpForce;
        public float StoppingDeceleration => stoppingDeceleration;

        public float DashSpeed => dashSpeed;
        public float DashDuration => dashDuration;
        public float DashCooldown => dashCooldown;

        public float CoyoteTime => coyoteTime;
        public float JumpBufferTime => jumpBufferTime;
        public float AirControlRatio => airControlRatio;

        public float MaxSlopeAngle => maxSlopeAngle;
        public float SlopeSnapForce => slopeSnapForce;
        public AnimationCurve SlopeJumpCurve => slopeJumpCurve;

        public float LightLandingDuration => lightLandingDuration;
        public float HardLandingDuration => hardLandingDuration;
        public float HardLandingThreshold => hardLandingThreshold;
    }
}

using UnityEngine;

namespace Dajunctic
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Dajunctic/Data/PlayerData")]
    public class PlayerData : BaseSO
    {
        [SerializeField] private ActorData actorData;
        [SerializeField] private float coyoteTime = 0.15f;
        [SerializeField] private float jumpBufferTime = 0.12f;
        [SerializeField] private float airControlRatio = 0.6f;
        [SerializeField] private float maxSlopeAngle = 45f;
        [SerializeField] private float slopeSnapForce = 10f;
        [SerializeField] private AnimationCurve slopeJumpCurve = AnimationCurve.Linear(0f, 1f, 45f, 0.65f);
        [SerializeField] private float lightLandingDuration = 0.15f;
        [SerializeField] private float hardLandingDuration = 0.5f;
        [SerializeField] private float hardLandingThreshold = 5f;

        public ActorData ActorData => actorData;

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

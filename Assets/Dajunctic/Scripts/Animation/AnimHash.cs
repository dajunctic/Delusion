using UnityEngine;

namespace Dajunctic
{
    public static class AnimHash
    {
        public static int Grounded => Animator.StringToHash("Grounded");
        public static int Idle => Animator.StringToHash("IsIdling");
        public static int Move => Animator.StringToHash("IsMoving");
        public static int Run => Animator.StringToHash("IsRunning");
        public static int Dash => Animator.StringToHash("IsDashing");
        public static int Stop => Animator.StringToHash("IsStopping");
        public static int LightStop => Animator.StringToHash("IsLightStopping");
        public static int HardStop => Animator.StringToHash("IsHardStopping");
        public static int Landing => Animator.StringToHash("IsLanding");
        public static int LightLanding => Animator.StringToHash("IsLightLanding");
        public static int HardLanding => Animator.StringToHash("IsHardLanding");
        public static int Roll => Animator.StringToHash("IsRolling");

        public static int Airborne = Animator.StringToHash("Airborne");
        public static int Jump => Animator.StringToHash("IsJumping");
    }
}
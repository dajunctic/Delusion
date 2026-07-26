using UnityEngine;

namespace Dajunctic
{
    public static class AnimHash
    {
        public static int Idle => Animator.StringToHash("IsIdling");
        public static int Grounded => Animator.StringToHash("IsGrounding");
        public static int Move => Animator.StringToHash("IsMoving");
        public static int Run => Animator.StringToHash("IsRunning");
        public static int Jump => Animator.StringToHash("IsJumping");
        public static int Dash => Animator.StringToHash("IsDashing");
        public static int Stop => Animator.StringToHash("IsStopping");
        public static int LightStop => Animator.StringToHash("IsLightStopping");
        public static int HardStop => Animator.StringToHash("IsHardStopping");
    }
}
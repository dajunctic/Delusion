using System;
using UnityEngine;

namespace Dajunctic
{
    [Serializable]
    public class ActorData
    {
        [SerializeField] private float walkSpeed = 2.0f;
        [SerializeField] private float runSpeed = 5.335f;
        [SerializeField] private float rotateSpeed = 5f;
        [SerializeField] private float jumpForce = 6.5f;
        [SerializeField] private float stoppingDeceleration = 20f;
        [SerializeField] private float dashSpeed = 12f;
        [SerializeField] private float dashDuration = 0.35f;
        [SerializeField] private float dashCooldown = 2.0f;

        public float WalkSpeed => walkSpeed;
        public float RunSpeed => runSpeed;
        public float RotateSpeed => rotateSpeed;

        public float JumpForce => jumpForce;
        public float StoppingDeceleration => stoppingDeceleration;

        public float DashSpeed => dashSpeed;
        public float DashDuration => dashDuration;
        public float DashCooldown => dashCooldown;
    }
}

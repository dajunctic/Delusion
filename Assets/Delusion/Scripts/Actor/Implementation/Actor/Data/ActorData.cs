using System;
using UnityEngine;

namespace Dajunctic
{
    [Serializable]
    public class ActorData
    {
        [SerializeField] ActorMovement movement;

        public float WalkSpeed => movement.walkSpeed;
        public float RunSpeed => movement.runSpeed;
        public float RotateSpeed => movement.rotateSpeed;
        public float Radius => movement.radius;
    }

    [Serializable]
    public class ActorMovement
    {
        public float walkSpeed = 2.0f;
        public float runSpeed = 5.335f;
        public float rotateSpeed = 5f;
        public float radius = 0.3f;
    }
}

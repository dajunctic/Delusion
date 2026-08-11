using System;
using System.Numerics;

namespace Dajunctic
{
    [Serializable]
    public struct Vector3Data
    {
        public float x, y, z;

        public Vector3Data (float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static implicit operator Vector3(Vector3Data v) => new(v.x, v.y, v.z);
        public static implicit operator Vector3Data(Vector3 v) => new(v.X, v.Y, v.Z);
    }
}
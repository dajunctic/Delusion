using UnityEngine;

namespace Dajunctic
{
    public interface ITransform: IEntity
    {
        public Transform CachedTransform {get; }
        public Vector3 Position {get; }
        public Vector3 Forward {get; }

        public void Rotate(Vector3 direction, float dt);
    }
}
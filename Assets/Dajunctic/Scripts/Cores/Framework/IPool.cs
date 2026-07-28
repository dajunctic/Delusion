using UnityEngine;

namespace Dajunctic
{
    public interface IPool
    {
        GameObject Spawn(GameObject prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null);
        T Spawn<T>(T prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where T: Component;
        void Despawn(GameObject obj, float delay = 0f);
    }
}
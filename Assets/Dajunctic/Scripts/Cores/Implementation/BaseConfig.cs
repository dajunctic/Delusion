using NaughtyAttributes;
using UnityEngine;

namespace Dajunctic
{
    public class BaseConfig : ScriptableObject, IConfig
    {
        [SerializeField, ReadOnly] private string id;

        public string Id => id;

        public bool Initialized => initialized;
        private bool initialized;

        public virtual void ListenEvents() {}
        public virtual void StopListenEvents() {}

        public virtual void Initialize()
        {
            initialized = true;
        }

        public virtual void CleanUp()
        {
            initialized = false;
        }

#if UNITY_EDITOR
        [Button]
        public void ResetId()
        {
            id = name;
        }

        void OnValidate()
        {
            id = name;
        }
#endif
    }
}
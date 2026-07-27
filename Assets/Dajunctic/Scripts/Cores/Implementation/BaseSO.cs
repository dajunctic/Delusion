using NaughtyAttributes;
using UnityEngine;

namespace Dajunctic
{
    public class BaseSO : ScriptableObject, IScriptableObject
    {
        [SerializeField, ReadOnly] private string id;

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
#endif
    }
}
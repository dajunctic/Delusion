using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Dajunctic
{
    public abstract partial class BaseApplication: MonoBehaviour, IApplication
    {   
        private bool initialized;
        public bool Initialized => initialized;


        #region LifeCycle

        void Awake()
        {
            if (IApplication.Instance != null)
            {
                Destroy(gameObject);
            }
            IApplication.Instance = this;
            DontDestroyOnLoad(gameObject);

            initialized = false;
        }

        async void Start()
        {
            _ = InitializeAsync();
        }

        void Update()
        {
            OnEarlyTick?.Invoke();
            OnTick?.Invoke();
        }

        void LateUpdate()
        {
            OnLateTick?.Invoke();
        }

        void FixedUpdate()
        {
            OnFixedTick?.Invoke();
        }

        void OnEnable()
        {
            
        }

        void OnDisable()
        {
            
        }

        void OnDestroy()
        {
            CleanUp();
        }

        public void Initialize()
        {

        }

        public virtual async UniTask InitializeAsync()
        {
            await InitializeSystems();
            await InitializeConfig();

            initialized = true;
        }

        public void CleanUp()
        {
            
        }

        #endregion

        #region EventDispatcher

        private static readonly Dictionary<Type, Delegate> _subscribers = new Dictionary<Type, Delegate>();

        public void RegisterListener<T>(Action<T> callback) where T : IEvent
        {
            Type type = typeof(T);

            if (_subscribers.TryGetValue(type, out var existingDelegate))
            {
                _subscribers[type] = Delegate.Combine(existingDelegate, callback);
            }
            else
            {
                _subscribers[type] = callback;
            }
        }

        public void RemoveListener<T>(Action<T> callback) where T : IEvent
        {
            Type type = typeof(T);

            if (_subscribers.TryGetValue(type, out var existingDelegate))
            {
                var currentDelegate = Delegate.Remove(existingDelegate, callback);

                if (currentDelegate == null)
                {
                    _subscribers.Remove(type);
                }
                else
                {
                    _subscribers[type] = currentDelegate;
                }
            }
        }

        public void Raise<T>(T eventData) where T : IEvent
        {
            Type type = typeof(T);

            if (_subscribers.TryGetValue(type, out var existingDelegate))
            {
                (existingDelegate as Action<T>)?.Invoke(eventData);
            }
        }

        public void ClearAllEvent()
        {
            _subscribers.Clear();
        }

        #endregion

        #region Ticker
        public event Action OnEarlyTick;
        public event Action OnTick;
        public event Action OnLateTick;
        public event Action OnFixedTick;

        #endregion

        #region Pool
        public GameObject Spawn(GameObject prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null)
        {
            throw new NotImplementedException();
        }

        public T Spawn<T>(T prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where T : Component
        {
            throw new NotImplementedException();
        }

        public void Despawn(GameObject obj, float delay = 0)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public static class BaseApplicationExtensions
    {
        public static IApplication GetApplication(this MonoBehaviour mono)
        {
            return IApplication.Instance;
        }
    }

   
}
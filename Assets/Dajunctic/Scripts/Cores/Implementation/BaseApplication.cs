using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dajunctic
{
    public abstract class BaseApplication: MonoBehaviour, IApplication
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

        void Start()
        {
            Initialize();
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

        public virtual void Initialize()
        {
            InitializeSystems();

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

        #region System

        private readonly Dictionary<Type, object> _systems = new Dictionary<Type, object>();

        protected virtual void InitializeSystems()
        {
            
        }

        public void RegisterSystem<T>(T system)
        {
            Type type = typeof(T);

            if (_systems.ContainsKey(type))
            {
                _systems[type] = system;
            }
            else
            {
                _systems.Add(type, system);
            }
        }

        public void UnregisterSystem<T>()
        {
            Type type = typeof(T);
            if (_systems.ContainsKey(type))
            {
                _systems.Remove(type);
            }
        }

        public T GetSystem<T>()
        {
            Type type = typeof(T);

            if (_systems.TryGetValue(type, out object system))
            {
                return (T)system;
            }

            Debug.LogError($"[BaseApplication] Can not find system has type: {type.Name}");
            return default;
        }
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Dajunctic
{
    public partial class BaseApplication
    {
       private readonly Dictionary<Type, object> _systems = new Dictionary<Type, object>();

        protected virtual async UniTask InitializeSystems()
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
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Dajunctic
{
    public partial class BaseApplication
    {
        private Dictionary<Type, object> _configs = new();

        private AsyncOperationHandle<IList<ScriptableObject>> _loadHandles;

        async UniTask InitializeConfig(string label = "config")
        {

            _loadHandles = Addressables.LoadAssetsAsync<ScriptableObject>(label, config =>
            {
                // Debug.LogError(config.name);
            });
            var loadedAssets = await _loadHandles.ToUniTask();

            if (_loadHandles.Status == AsyncOperationStatus.Succeeded)
            {
                foreach (var asset in loadedAssets)
                {
                    if (asset is IConfig)
                    {
                        RegisterConfig(asset);
                    }
                }
            }
        }

        public void RegisterConfig(ScriptableObject config)
        {
            var type = config.GetType();
            RegisterConfigInternal(type, config);
        }

        void RegisterConfigInternal(Type type, ScriptableObject config)
        {
            if (!_configs.TryGetValue(type, out var listObjs))
            {
                var listType = typeof(List<>).MakeGenericType(type);
                listObjs = Activator.CreateInstance(listType);
                _configs[type] = listObjs;
            }

            ((IList)listObjs).Add(config);
        }

        public void ReleaseConfig()
        {
            if (_loadHandles.IsValid())
            {
                Addressables.Release(_loadHandles);
            }
            _configs.Clear();
        }

        public T GetFirstConfig<T>() where T: IConfig
        {
            if (_configs.TryGetValue(typeof(T), out var listObjs))
            {
                var list = (List<T>)listObjs;
                return list.Count > 0 ? list[0] : default;
            }

            return default;
        }

        public T[] GetAllConfigs<T>() where T: IConfig
        {
            if (_configs.TryGetValue(typeof(T), out var listObjs))
            {
                var list = (List<T>)listObjs;
                return list.ToArray();
            }
            return Array.Empty<T>();
        }
    }

    
}
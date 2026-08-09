using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Dajunctic
{
    public partial class BaseApplication
    {
        private Dictionary<string, object> _saves;

        async UniTask InitializeSave(string label = "save")
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
                        RegisterSave(asset);
                    }
                }
            }
        }

        public void RegisterSave(ScriptableObject config)
        {
            var type = config.GetType();
            RegisterSaveInternal(type, config);
        }

        void RegisterSaveInternal(Type type, ScriptableObject config)
        {
            if (!_configs.TryGetValue(type, out var listObjs))
            {
                var listType = typeof(List<>).MakeGenericType(type);
                listObjs = Activator.CreateInstance(listType);
                _configs[type] = listObjs;
            }

            ((IList)listObjs).Add(config);
        }


    }
}
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Dajunctic
{
    public class MainApplication: BaseApplication
    {
        [SerializeField] AssetReference homeScene;
        
        async void Start()
        {
            _ = LoadingAsync();
        }

        async UniTask LoadingAsync()
        {
            await InitializeAsync();
            await UniTask.WaitUntil(() => Initialized);
            AddressableUtils.LoadScene(homeScene);
        }

        protected override async UniTask InitializeSystems()
        {
            await base.InitializeSystems();
            RegisterSystem<IComputerSystem>(new ComputerSystem());
        }
    }
}
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
            var saveSystem = new SaveSystem();
            var computerSytem = new ComputerSystem();

            RegisterSystem<ISaveSystem>(saveSystem);
            RegisterSystem<IComputerSystem>(computerSytem);


            // await saveSystem.Initialize();
        }
    }
}
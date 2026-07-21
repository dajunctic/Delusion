using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Dajunctic
{
    public class MainApplication: BaseApplication
    {
        [SerializeField] AssetReference homeScene;

        public override void Initialize()
        {
            base.Initialize();
            StartCoroutine(IWaitLoading());
        }

        IEnumerator IWaitLoading()
        {
            yield return new WaitUntil(() => Initialized);
            AddressableUtils.LoadScene(homeScene);
        }

        protected override void InitializeSystems()
        {
            base.InitializeSystems();

            
        }
    }
}
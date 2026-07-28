using System.Collections;
using UnityEngine;

namespace Dajunctic
{
    public interface IApplication: ILifeCycle, IEventDispatcher, ITicker, ISystemLocator, IPool
    {
        static IApplication Instance;

        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine coroutine);
    }
}

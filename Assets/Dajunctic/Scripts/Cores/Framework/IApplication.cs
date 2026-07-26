using System.Collections;
using UnityEngine;

namespace Dajunctic
{
    public interface IApplication: ILifeCycle, IEventDispatcher, ITicker, ISystemLocator
    {
        static IApplication Instance;

        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine coroutine);
    }
}

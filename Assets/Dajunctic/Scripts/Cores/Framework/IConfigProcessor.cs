using UnityEngine;

namespace Dajunctic
{
    public interface IConfigProcessor
    {
        void RegisterConfig(ScriptableObject config);


        void ReleaseConfig();

        public T GetFirstConfig<T>() where T: IConfig;

        public T[] GetAllConfigs<T>() where T: IConfig;
    }
}
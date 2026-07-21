namespace Dajunctic
{
    public interface ISystem
    {
        
    }

    public interface ISystemLocator
    {
        public void RegisterSystem<T>(T system);
        public void UnregisterSystem<T>();
        public T GetSystem<T>();
    }
}
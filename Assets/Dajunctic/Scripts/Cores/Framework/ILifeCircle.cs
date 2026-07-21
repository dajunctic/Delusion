namespace Dajunctic
{
    public interface ILifeCircle
    {
        bool Initialized {get; }
        void Initialize();
        void CleanUp();
    }
}
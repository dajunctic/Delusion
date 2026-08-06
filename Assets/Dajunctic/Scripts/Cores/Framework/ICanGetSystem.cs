using Unity.VisualScripting;

namespace Dajunctic
{
    public interface ICanGetSystem
    {
        
    }

    public static class SystemExtensions
    {

        public static T GetSystem<T>(this ICanGetSystem obj) where T: ISystem
        {
            return IApplication.Instance.GetSystem<T>();
        }
    }
}
namespace Dajunctic
{
    public interface ICanGetConfig
    {
        
    }

    public static class ConfigExtensions
    {
        public static T GetFirstConfig<T>(this ICanGetConfig obj) where T: IConfig
        {
            return IApplication.Instance.GetFirstConfig<T>();
        }
    }
}
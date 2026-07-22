namespace Dajunctic
{
    public interface IEntity
    {
        T As<T>() where T: class => this as T;
    }
}
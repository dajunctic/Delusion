namespace Dajunctic
{
    public class BaseSave<T> : BaseConfig, ISavable
    {
        public T Value => value;
        T value;

        public void Load()
        {
            
        }

        public void Save(bool write)
        {
            
        }
    }
}
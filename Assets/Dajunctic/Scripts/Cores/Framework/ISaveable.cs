namespace Dajunctic
{
    public interface ISavable
    {
        void Load();
        void Save(bool write);
    }
}
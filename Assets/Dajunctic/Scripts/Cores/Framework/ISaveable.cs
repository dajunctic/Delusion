namespace Dajunctic
{
    public interface ISavable
    {
        string SaveId {get; }
        void PopulateSaveData(ISave save);
        void RestoreSaveData(ISave save);
    }
}
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Dajunctic
{
    public interface ISaveStorage
    {
        UniTask<bool> SaveToFileAsync(string fileName, GameSave data);
        UniTask<GameSave> LoadFromFileAsync(string fileName);
        UniTask<SaveMetadata> LoadMetadataAsync(string fileName);
        bool Exists(string fileName);
        void Delete(string fileName);
        
        UniTask<List<SaveMetadata>> GetAllSaveMetadataAsync();

    }
}
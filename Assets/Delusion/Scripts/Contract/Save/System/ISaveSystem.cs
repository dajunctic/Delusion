using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Dajunctic
{
    public interface ISaveSystem: ISystem
    {
        UniTask<bool> SaveAsync(int slotIndex);
        UniTask<bool> LoadAsync(int slotIndex);

        void RegisterSavable(ISavable savable);
        void UnregisterSavable(ISavable savable);

        UniTask<List<SaveMetadata>> GetAllSaveMetadataAsync();
    }
}
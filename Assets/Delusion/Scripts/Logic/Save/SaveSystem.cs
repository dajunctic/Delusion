using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Dajunctic
{
    public class SaveSystem : ISaveSystem
    {
        public bool Initialized => _initialized;
        private bool _initialized;

        private ISaveStorage _storage;
        private HashSet<ISavable> _savables;

        public async void Initialize()
        {
            _storage = new SaveStorage();
            _savables = new HashSet<ISavable>();
            _initialized = true;
        }

        public async void CleanUp()
        {
            _initialized = false;
            _storage = null;
            _savables = null;
        }
      
        public async UniTask<bool> LoadAsync(int slotIndex)
        {
            var fileName = $"save_{slotIndex}.json";
            var saveData = await _storage.LoadFromFileAsync(fileName);

            return saveData is not null;
        }

        public async UniTask<bool> SaveAsync(int slotIndex)
        {
            var fileName = $"save_{slotIndex}.json";
            
            var saveData = new GameSave();
            saveData.Meta.SlotIndex = slotIndex;
            saveData.Meta.SaveTitle = $"Save {slotIndex}";
            // saveData.Meta.SaveTimestamp = TimeUtils.SecondTimes;
            // saveData.Meta.Version 

            foreach (var savable in _savables)
            {
                savable.PopulateSaveData(saveData);
            }

            var success = await _storage.SaveToFileAsync(fileName, saveData);

            return success;
        }
 
        public void RegisterSavable(ISavable savable)
        {
            _savables.Add(savable);
        }

        public void UnregisterSavable(ISavable savable)
        {
            _savables.Remove(savable);
        }

        public async UniTask<List<SaveMetadata>> GetAllSaveMetadataAsync()
        {
            return await _storage.GetAllSaveMetadataAsync();
        }
    }
}
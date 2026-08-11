using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Dajunctic
{
    public class SaveStorage: ISaveStorage
    {
        string _saveDirectory;

        public SaveStorage()
        {
            _saveDirectory = Path.Combine(Application.persistentDataPath, "Saves");

            if (!Directory.Exists(_saveDirectory))
            {
                Directory.CreateDirectory(_saveDirectory);
            }
        }

        public async UniTask<GameSave> LoadFromFileAsync(string fileName)
        {
            return await UniTask.RunOnThreadPool(() =>
            {
                var filePath = Path.Combine(_saveDirectory, fileName);
                var bakPath = filePath + ".bak";
                var tmpPath = filePath + ".tmp";

                if (File.Exists(tmpPath))
                {
                    try
                    {
                        var tmpJson = File.ReadAllText(tmpPath, Encoding.UTF8);
                        var tmpData = JsonConvert.DeserializeObject<GameSave>(tmpJson) ?? throw new System.Exception();

                        if (File.Exists(filePath))
                        {
                            if (File.Exists(bakPath)) File.Delete(bakPath);
                            File.Move(filePath, bakPath);
                        }

                        File.Move(tmpPath, filePath);
                        return tmpData;
                    }
                    catch {}
                    finally
                    {
                        if (File.Exists(tmpPath)) File.Delete(tmpPath);   
                    }
                }

                if (File.Exists(filePath))
                {
                    try
                    {
                        var mainJson = File.ReadAllText(tmpPath, Encoding.UTF8);
                        var mainData = JsonConvert.DeserializeObject<GameSave>(mainJson) ?? throw new System.Exception();
                        return mainData;
                    }
                    catch {}
                }

               
                if (File.Exists(bakPath))
                {
                    try
                    {
                        var backUpJson = File.ReadAllText(tmpPath, Encoding.UTF8);
                        var backUpData = JsonConvert.DeserializeObject<GameSave>(backUpJson) ?? throw new System.Exception();
                        return backUpData;
                    }
                    catch {}
                }

                return null;
            }
            
            );
        }

        public async UniTask<bool> SaveToFileAsync(string fileName, GameSave data)
        {
            return await UniTask.RunOnThreadPool(() =>
            {
                var filePath = Path.Combine(_saveDirectory, fileName);
                var bakPath = filePath + ".bak";
                var tmpPath = filePath + ".tmp";

                try 
                {
                    var json = JsonConvert.SerializeObject(data);

                    File.WriteAllText(tmpPath, json, Encoding.UTF8);

                    if (File.Exists(filePath))
                    {
                        File.Copy(filePath, bakPath);
                        File.Delete(filePath);
                    }

                    File.Move(tmpPath, filePath);

                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        public void Delete(string fileName)
        {
            var filePath = Path.Combine(_saveDirectory, fileName);
            if (File.Exists(filePath)) File.Delete(filePath);
        }

        public bool Exists(string fileName)
        {
            var filePath = Path.Combine(_saveDirectory, fileName);
            return File.Exists(filePath);
        }

        async UniTask<SaveMetadata> ISaveStorage.LoadMetadataAsync(string fileName)
        {
            var save = await LoadFromFileAsync(fileName);
            return save?.Meta;
        }

        async UniTask<List<SaveMetadata>> ISaveStorage.GetAllSaveMetadataAsync()
        {
            return await UniTask.RunOnThreadPool(()=>
            {
                var list = new List<SaveMetadata>();
                var templatePath = Path.Combine(_saveDirectory, "save_*.json");
                var files = Directory.GetFiles(templatePath);

                foreach (var file in files)
                {
                    try
                    {
                        var json = File.ReadAllText(file, Encoding.UTF8);
                        var data = JsonConvert.DeserializeObject<GameSave>(json);
                        if (data?.Meta is not null) list.Add(data.Meta);
                    }
                    catch {}
                }

                return list;
            });
        }
    }
}
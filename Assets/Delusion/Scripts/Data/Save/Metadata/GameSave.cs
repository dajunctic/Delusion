using System;

namespace Dajunctic
{
    [Serializable]
    public class GameSave: ISave
    {
        public SaveMetadata Meta = new();
        public ComputerSave ComputerSave = new();
        
    }
}
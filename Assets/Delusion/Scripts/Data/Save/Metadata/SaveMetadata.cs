using System;

namespace Dajunctic
{
    [Serializable]
    public class SaveMetadata: ISaveMetadata
    {
        public int SlotIndex;
        public string SaveTitle = "New Save";
        public double SaveTimestamp; // Epoch Time
        public int Version = 1;
    }


}
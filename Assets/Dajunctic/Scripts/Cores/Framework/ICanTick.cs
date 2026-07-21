using System;

namespace Dajunctic
{
    public interface ICanTick
    {
        public void EarlyTick() {}
        public void Tick() {}
        public void LateTick() {}
        public void FixedTick() {}
    }

    [Flags]
    public enum TickMode
    {
        None = 0,
        EarlyTick = 1,
        Tick = 2,
        FixedTick = 4,
        LateTick = 8,
    }
}
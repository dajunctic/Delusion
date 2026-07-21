using System;

namespace Dajunctic
{
    public interface ITicker
    {
        public event Action OnEarlyTick;
        public event Action OnTick;
        public event Action OnLateTick;
        public event Action OnFixedTick;

        public void EarlyTick() {}
        void Tick() {}
        void LateTick() {} 
        void FixedTick() {}
    }
}
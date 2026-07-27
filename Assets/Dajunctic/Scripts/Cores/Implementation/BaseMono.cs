using UnityEngine;

namespace Dajunctic
{
    public class BaseMono : MonoBehaviour, IMono
    {
        [SerializeField] TickMode tickMode;
        [SerializeField] bool initialized;

        public bool Initialized => initialized;


        void Start()
        {
            if (initialized) Initialize();
        }

        void OnEnable()
        {
            SubTick();
            ListenEvents();
            DoEnable();
        }

        void OnDisable()
        {
            DoDisable();
            UnSubTick();
            StopListenEvents();
            CleanUp();
        }

        void SubTick()
        {
            if (tickMode.HasFlag(TickMode.EarlyTick)) this.GetApplication().OnEarlyTick += EarlyTick;
            if (tickMode.HasFlag(TickMode.Tick)) this.GetApplication().OnTick += Tick;
            if (tickMode.HasFlag(TickMode.LateTick)) this.GetApplication().OnLateTick += LateTick;
            if (tickMode.HasFlag(TickMode.FixedTick)) this.GetApplication().OnFixedTick += FixedTick;
        }

        void UnSubTick()
        {
            if (tickMode.HasFlag(TickMode.EarlyTick)) this.GetApplication().OnEarlyTick -= EarlyTick;
            if (tickMode.HasFlag(TickMode.Tick)) this.GetApplication().OnTick -= Tick;
            if (tickMode.HasFlag(TickMode.LateTick)) this.GetApplication().OnLateTick -= LateTick;
            if (tickMode.HasFlag(TickMode.FixedTick)) this.GetApplication().OnFixedTick -= FixedTick;
        }

        public virtual void Initialize() {}
        public virtual void ListenEvents() {}
        public virtual void StopListenEvents() {}
        public virtual void DoEnable() {}
        public virtual void DoDisable() {}
        public virtual void EarlyTick() {}
        public virtual void Tick() {}
        public virtual void LateTick() {}
        public virtual void FixedTick() {}
        public virtual void CleanUp(){}
    }
}

using UnityEngine;

namespace Dajunctic.Cores
{
    public class BaseMono : MonoBehaviour
    {
        [SerializeField] TickMode tickMode;

        void Awake()
        {
            
        }


        void Start()
        {
            
        }

        void OnEnable()
        {
            
        }

        void OnDisable()
        {
            
        }

        void SubTick()
        {
            
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
        
    }
}

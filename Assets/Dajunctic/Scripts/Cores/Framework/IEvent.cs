using System;
using UnityEngine;

namespace Dajunctic
{
     public interface IEventDispatcher
    {
        public void RegisterListener<T>(Action<T> callback) where T : IEvent {}
        public void RemoveListener<T>(Action<T> callback) where T : IEvent {}
        public void Raise<T>(T eventData) where T : IEvent {}
        public void ClearAllEvent();
    }

    public interface IEvent{}


    public static class EventDispatcherExtensions
    {
    
        public static void Raise<T>(this MonoBehaviour mono, T eventData) where T : IEvent
        {
            IApplication.Instance.Raise(eventData);
        }

        public static Action<T> RegisterListener<T>(this MonoBehaviour mono, Action<T> callback) where T : IEvent
        {
            IApplication.Instance.RegisterListener(callback);
            return callback;
        }

        public static Action<T> RemoveListener<T>(this MonoBehaviour mono, Action<T> callback) where T : IEvent
        {
            IApplication.Instance.RemoveListener(callback);
            return callback;
        }
    }
}
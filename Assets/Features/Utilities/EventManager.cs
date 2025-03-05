using System;
using System.Collections.Generic;
using Features.Utilities;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public delegate void EventCallback<in T>(T data);

    public class EventManager : SingletonMonoBehaviour<EventManager>
    {
        private static Dictionary<Type, List<Delegate>> _eventDictionary = new();

        public static void AddEventListener<T>(EventCallback<T> listener)
        {
            if (_eventDictionary.TryGetValue(typeof(T), out var listeners))
            {
                if (!listeners.Contains(listener)) listeners.Add(listener);
            }
            else
            {
                listeners = new List<Delegate>();
                listeners.Add(listener);
                _eventDictionary.Add(typeof(T), listeners);
            }
        }

        public static List<Delegate> GetListenersOfEvent<T>()
        {
            if (_eventDictionary.TryGetValue(typeof(T), out var listeners)) return listeners;

            return null;
        }

        public static void RemoveEventListener<T>(EventCallback<T> listener)
        {
            if (_eventDictionary.TryGetValue(typeof(T), out var listeners)) listeners.Remove(listener);
        }

        public static void RemoveEvent<T>()
        {
            _eventDictionary.Remove(typeof(T));
        }

        public static void RemoveAllEvents()
        {
            _eventDictionary = new Dictionary<Type, List<Delegate>>();
        }

        public static void TriggerEvent<T>(T data)
        {
            if (!_eventDictionary.TryGetValue(typeof(T), out var listeners)) return;
            foreach (var listener in listeners.ToArray())
                listener.DynamicInvoke(data);
        }
    }
}
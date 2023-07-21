using System.Collections.Generic;
using UnityEngine;

namespace Event
{
    [CreateAssetMenu(menuName = "Game Event Int")]
    public class GameEventInt : ScriptableObject
    {
        private readonly List<GameEventListenerInt> _listeners = new();

        public void Trigger(int value)
        {
            for (var i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventTriggered(value);
            }
        }

        public void AddListener(GameEventListenerInt listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(GameEventListenerInt listener)
        {
            _listeners.Remove(listener);
        }
    }
}

using UnityEngine;
using UnityEngine.Events;

namespace Event
{
    public class GameEventListenerInt : MonoBehaviour
    {
        public GameEventInt gameEvent;
        public UnityEvent<int> onEventTriggered;

        private void OnEnable()
        {
            gameEvent.AddListener(this);
        }

        private void OnDisable()
        {
            gameEvent.RemoveListener(this);
        }

        public void OnEventTriggered(int value)
        {
            onEventTriggered.Invoke(value);
        }
    }
}
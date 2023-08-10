using UnityEngine;
using UnityEngine.Events;

namespace Ram
{
    public class RamHealth : MonoBehaviour, ISerializationCallbackReceiver
    {
        private static int _maxHitPoint = 5;

        [Tooltip("The max and starting hit point of the ram.")] [SerializeField]
        private int maxHitPoint;

        [Tooltip("The current hit point of the ram.")] [SerializeField]
        private int currentHitPoint;

        [Tooltip("Amount of health added to max hit point when ram gets destroyed.")] [SerializeField]
        private int difficultyRamp = 1;

        [Tooltip("This event is triggered when ram's health reaches 0.")] [SerializeField]
        private UnityEvent onDeathEvent;


        private void OnEnable()
        {
            currentHitPoint = _maxHitPoint;
        }


        public void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Bolt")) return;

            currentHitPoint--;
            if (!(currentHitPoint <= 0)) return;

            _maxHitPoint += difficultyRamp;
            onDeathEvent.Invoke();
        }


        public void OnAfterDeserialize()
        {
            _maxHitPoint = maxHitPoint;
        }

        public void OnBeforeSerialize()
        {
        }
    }
}
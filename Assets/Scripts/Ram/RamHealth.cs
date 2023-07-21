using UnityEngine;
using UnityEngine.Events;

namespace Ram
{
    public class RamHealth : MonoBehaviour
    {
        [Tooltip("The max and starting hit point of the ram.")] [SerializeField]
        private float maxHitPoint = 5f;

        [Tooltip("The current hit point of the ram.")] [SerializeField]
        private float currentHitPoint;

        [Tooltip("This event is triggered when ram's health reaches 0.")]
        [SerializeField] private UnityEvent onDeathEvent;


        private void OnEnable()
        {
            currentHitPoint = maxHitPoint;
        }


        public void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Bolt")) return;
            
            currentHitPoint--;
            if (currentHitPoint <= 0)
            {
                onDeathEvent.Invoke();
            }
        }
    }
}
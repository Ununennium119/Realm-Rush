using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    public class SelfDestructor : MonoBehaviour
    {
        [Tooltip("The delay before deactivating game object in seconds.")] [SerializeField]
        private float delay = 3f;

        [Tooltip("This event is triggered when destroying object.")] [SerializeField]
        private UnityEvent onDestroyEvent;


        private void OnEnable()
        {
            Invoke(nameof(DestroyObject), delay);
        }


        private void DestroyObject()
        {
            onDestroyEvent.Invoke();
        }
    }
}
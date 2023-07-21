using UnityEngine;

namespace Bolt
{
    public class BoltController : MonoBehaviour
    {
        [Tooltip("The speed in which bolt moves.")] [SerializeField]
        private float speed = 100f;

        [Tooltip("The bolt will disable after passing lifeTime seconds.")] [SerializeField]
        private float lifeTime = 3f;


        private void OnEnable()
        {
            Invoke(nameof(DisableBolt), lifeTime);
        }

        private void Update()
        {
            var boltTransform = transform;
            boltTransform.position += boltTransform.forward * (speed * Time.deltaTime);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ram"))
            {
                gameObject.SetActive(false);
            }
        }


        private void DisableBolt()
        {
            gameObject.SetActive(false);
        }
    }
}
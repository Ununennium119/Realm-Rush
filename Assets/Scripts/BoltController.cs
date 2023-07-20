using UnityEngine;

public class BoltController : MonoBehaviour
{
    [SerializeField] private float speed = 100f;

    [SerializeField] private GameEvent enemyHitEvent;


    private void Update()
    {
        var boltTransform = transform;
        boltTransform.position += boltTransform.forward * (speed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        enemyHitEvent.Trigger();
        Destroy(gameObject);
    }
}
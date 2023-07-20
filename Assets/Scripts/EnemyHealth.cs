using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHitPoint = 5f;
    [SerializeField] private float currentHitPoint;


    private void Start()
    {
        currentHitPoint = maxHitPoint;
    }


    public void OnEnemyHit()
    {
        currentHitPoint--;
        if (currentHitPoint <= 0)
        {
            Destroy(gameObject);
        }
    }
}
using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
    [SerializeField] private float delay = 3f;


    private void Start()
    {
        Destroy(gameObject, delay);
    }
}
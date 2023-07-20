using System.Collections;
using UnityEngine;

public class BallistaController : MonoBehaviour
{
    [SerializeField] private GameObject boltPrefab;

    [SerializeField] private Transform weapon;

    [SerializeField] private float fireDelay = 1f;

    private Transform _target;
    private bool _targetPresent;


    private void Start()
    {
        _target = FindObjectOfType<EnemyMover>().transform;
        StartCoroutine(Fire());
    }

    private void Update()
    {
        _targetPresent = _target != null;
        if (_targetPresent)
        {
            weapon.LookAt(_target.position + Vector3.up * weapon.position.y);
        }
    }


    private IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireDelay);
            if (_targetPresent)
            {
                Instantiate(boltPrefab, weapon.position, weapon.rotation);
            }
        }
    }
}
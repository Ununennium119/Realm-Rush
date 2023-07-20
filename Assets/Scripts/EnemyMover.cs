using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<WayPoint> path = new();

    private void Start()
    {
        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        foreach (var wayPoint in path)
        {
            Debug.Log(wayPoint);
            transform.position = wayPoint.transform.position;
            yield return new WaitForSeconds(1);
        }
    }
}

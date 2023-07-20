using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<WayPoint> path = new();

    [SerializeField] [Range(0f, 5f)] private float speed = 1f;

    
    private void Start()
    {
        StartCoroutine(FollowPath());
    }

    
    private IEnumerator FollowPath()
    {
        foreach (var wayPoint in path)
        {
            var startPosition = transform.position;
            var endPosition = wayPoint.transform.position;
            var travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
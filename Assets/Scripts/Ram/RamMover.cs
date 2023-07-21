using System.Collections;
using System.Collections.Generic;
using Tile;
using UnityEngine;
using UnityEngine.Events;

namespace Ram
{
    public class RamMover : MonoBehaviour
    {
        [Tooltip("The ram's path.")] [SerializeField]
        private List<WayPoint> path = new();

        [Tooltip("The speed in which ram moves.")] [SerializeField] [Range(0f, 5f)]
        private float speed = 1f;

        [Tooltip("This event will be invoked when enemy reaches treasure.")] [SerializeField] 
        private UnityEvent enemyReachedTreasureEvent;


        private void OnEnable()
        {
            FindPath();
            ReturnToStart();
            StartCoroutine(FollowPath());
        }


        private void FindPath()
        {
            path.Clear();
            
            var pathObject = GameObject.FindWithTag("Path");
            foreach (Transform child in pathObject.transform)
            {
                path.Add(child.GetComponent<WayPoint>());
            }
        }

        private void ReturnToStart()
        {
            transform.position = path[0].transform.position;
        }

        private IEnumerator FollowPath()
        {
            var waitForEndOfFrame = new WaitForEndOfFrame();
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
                    yield return waitForEndOfFrame;
                }
            }
            
            enemyReachedTreasureEvent.Invoke();
        }
    }
}
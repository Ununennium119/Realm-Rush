using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Grid;
using UnityEngine;
using UnityEngine.Events;

namespace Ram
{
    public class RamMover : MonoBehaviour
    {
        [Tooltip("The speed in which ram moves.")] [SerializeField] [Range(0f, 5f)]
        private float speed = 1f;

        [Tooltip("This event will be invoked when enemy reaches treasure.")] [SerializeField]
        private UnityEvent enemyReachedTreasureEvent;

        private Coroutine _followPathCoroutine;


        private GameController _gameController;
        private GridController _gridController;

        private List<Vector3> _path;


        private void Awake()
        {
            _gameController = FindObjectOfType<GameController>();
            _gridController = FindObjectOfType<GridController>();
        }

        private void OnEnable()
        {
            FindPath();
            ReturnToStart();
            _followPathCoroutine = StartCoroutine(FollowPath());
        }

        public void OnBallistaSpawned()
        {
            StopCoroutine(_followPathCoroutine);
            _path = _gridController.FindPath(transform.position, _gameController.playerBasePosition);
            _followPathCoroutine = StartCoroutine(FollowPath());
        }


        private void FindPath()
        {
            _path = _gridController.FindPath(_gameController.enemyBasePosition, _gameController.playerBasePosition);
        }

        private void ReturnToStart()
        {
            transform.position = _path[0];
        }

        private IEnumerator FollowPath()
        {
            var waitForEndOfFrame = new WaitForEndOfFrame();
            foreach (var position in _path.Skip(1))
            {
                var startPosition = transform.position;
                var travelPercent = 0f;

                transform.LookAt(position);

                while (travelPercent < 1f)
                {
                    travelPercent += Time.deltaTime * speed;
                    transform.position = Vector3.Lerp(startPosition, position, travelPercent);
                    yield return waitForEndOfFrame;
                }
            }

            enemyReachedTreasureEvent.Invoke();
        }
    }
}
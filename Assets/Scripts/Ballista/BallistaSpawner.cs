using Event;
using Grid;
using Treasury;
using UnityEngine;

namespace Ballista
{
    public class BallistaSpawner : MonoBehaviour
    {
        [Tooltip("Insert ballista's prefab here.")] [SerializeField]
        private GameObject ballistaPrefab;

        [Tooltip("The cost of spawning a ballista.")] [SerializeField]
        private int ballistaPrice = 50;

        [Tooltip("Insert 'BallistaSpawnedEvent' of type 'GameEvent' here.")] [SerializeField]
        private GameEvent ballistaSpawnedEvent;


        private GameController _gameController;
        private GridController _gridController;
        private TreasuryController _treasuryController;


        private void Awake()
        {
            _gameController = FindObjectOfType<GameController>();
            _gridController = FindObjectOfType<GridController>();
            _treasuryController = FindObjectOfType<TreasuryController>();
        }


        public bool SpawnBallista(Vector3 position, Quaternion rotation)
        {
            _gridController.SetNodeBlocked(position, true);
            if (_gridController.FindPath(_gameController.enemyBasePosition, _gameController.playerBasePosition) == null)
            {
                _gridController.SetNodeBlocked(position, false);
                return false;
            }

            if (!_treasuryController.DecreaseGold(ballistaPrice)) return false;

            var ballista = Instantiate(ballistaPrefab, transform);
            ballista.transform.position = position;
            ballista.transform.rotation = rotation;
            var ballistaController = ballista.GetComponent<BallistaController>();
            StartCoroutine(ballistaController.Build());

            ballistaSpawnedEvent.Trigger();
            return true;
        }
    }
}
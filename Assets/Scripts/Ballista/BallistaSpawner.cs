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

        
        private TreasuryController _treasuryController;


        private void Awake()
        {
            _treasuryController = FindObjectOfType<TreasuryController>();
        }


        public bool SpawnBallista(Vector3 position, Quaternion rotation)
        {
            if (!_treasuryController.DecreaseGold(ballistaPrice)) return false;

            var ballista = Instantiate(ballistaPrefab, transform);
            ballista.transform.position = position;
            ballista.transform.rotation = rotation;
            return true;
        }
    }
}
using Event;
using Treasury;
using UnityEngine;

namespace Ram
{
    public class RamController : MonoBehaviour
    {
        [Tooltip("The amount of money the player will be rewarded after killing this enemy.")] [SerializeField]
        private int killReward = 25;

        [Tooltip("The amount of money the enemy will stole after enemy reaching treasure.")] [SerializeField]
        private int stealAmount = 25;

        [Tooltip("Insert 'EnemyReachedTreasuryEvent' of type 'GameEventInt' here.")] [SerializeField]
        private GameEventInt enemyReachedTreasuryEvent;

        private TreasuryController _treasuryController;


        private void Awake()
        {
            _treasuryController = FindObjectOfType<TreasuryController>();
        }


        public void OnDeath()
        {
            gameObject.SetActive(false);
            _treasuryController.IncreaseGold(killReward);
        }

        public void OnReachedTreasure()
        {
            enemyReachedTreasuryEvent.Trigger(stealAmount);
        }
    }
}
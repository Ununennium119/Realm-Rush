using TMPro;
using UnityEngine;

namespace Treasury
{
    public class TreasuryController : MonoBehaviour
    {
        [Tooltip("The starting money in the treasury.")] [SerializeField]
        private int startGold;

        [Tooltip("The current money in the treasury.")] [SerializeField]
        private int currentGold;

        [SerializeField] private TextMeshProUGUI goldText;


        private void Start()
        {
            currentGold = startGold;
            UpdateGoldText();
        }


        public bool DecreaseGold(int amount)
        {
            if (amount > currentGold) return false;

            currentGold -= amount;
            UpdateGoldText();
            return true;
        }

        public bool IncreaseGold(int amount)
        {
            currentGold += amount;
            UpdateGoldText();
            return true;
        }


        private void UpdateGoldText()
        {
            goldText.text = $"Gold: {currentGold}";
        }
    }
}
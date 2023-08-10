using Treasury;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Tooltip("The ram's start position.")] [SerializeField]
    public Vector3 enemyBasePosition;

    [Tooltip("The ram's final destination.")] [SerializeField]
    public Vector3 playerBasePosition;

    private TreasuryController _treasuryController;


    private void Awake()
    {
        _treasuryController = FindObjectOfType<TreasuryController>();
    }


    public void OnEnemyReachedTreasury(int penalty)
    {
        if (_treasuryController.DecreaseGold(penalty)) return;

        GameOver();
    }

    private static void GameOver()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static float speed = 10f;
    public static bool isGameOver = false;

    [Header("UI")]
    [SerializeField] private GameObject gameOverUI;

    private void Start()
    {
        isGameOver = false;
        speed = 10f;

        if (gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        // Stop movement
        speed = 0f;

        // Show UI
        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        // Restart after 10 sec
        Invoke(nameof(RestartGame), 10f);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
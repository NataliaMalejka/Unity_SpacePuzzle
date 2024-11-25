using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;

    private bool activeWinPanel = false;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameOverPanel != null && (winPanel == null || !activeWinPanel))
                gameOverPanel.SetActive(true);
        }
    }

    public void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            activeWinPanel = true;
        }
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadSelectedLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}

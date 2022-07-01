using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool GameStarted { get; private set; }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        GameStarted = true;
    }
    public void RestartGame()
    {
        Invoke("Load", 1f);
    }
    private void Load()
    {
        SceneManager.LoadScene(0);
    }
}

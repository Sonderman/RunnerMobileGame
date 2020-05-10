using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameStarted { get; private set; }
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
   
    public void StartGame()
    {
        gameStarted = true;
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

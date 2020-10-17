using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0;
    }
    /// <summary>
    /// Продолжает/запускает игру.
    /// </summary>
    public void StartGame()
    {
        Time.timeScale = 1;
    }
    /// <summary>
    /// Перезапускает игру.
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    /// <summary>
    /// Заканчивает игру.
    /// </summary>
    public void StopGame()
    {
        Time.timeScale = 0;
    }
}

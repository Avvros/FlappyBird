using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnGameSpeedChange;

    [SerializeField] private GameObject _scoreCounter;
	[SerializeField] private GameObject _boosterManager;
    [SerializeField] private GameObject _darkMask;

    [SerializeField] private int _difficult = 1;
    [SerializeField] private int _coveredScore;
    [SerializeField] internal float GameSpeedMultiplayer = 1;

    [SerializeField] public bool GameIsStopped = false;

    private void Start()
    {
        StartCoroutine(ScreenDarker());
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (ChangeDifficult())
        {
            SetGameSpeed();
        }
    }

    /// <summary>
    /// Продолжает/запускает игру.
    /// </summary>
    public void StartGame()
    {
        GameIsStopped = false;
        Time.timeScale = 1 * GameSpeedMultiplayer;
		_boosterManager.GetComponent<BoostersManager>().ChangeBoostersCondition(true);
    }

    /// <summary>
    /// Перезапускает сцену.
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
        GameIsStopped = true;
        Time.timeScale = 0;
    }

    private bool ChangeDifficult()
    {
        if (_scoreCounter == null)
        {
            return false;
        }

        bool isDifficultChange = false;
        float _calculatedDifficult = _difficult;
        _coveredScore = _scoreCounter.GetComponent<Score>().CoveredScoreCount;

        switch (_coveredScore)
        {
            case 30:
                _calculatedDifficult = 2;
                isDifficultChange = true;
                break;
            case 50:
                _calculatedDifficult = 3;
                isDifficultChange = true;
                break;
            case 80:
                _calculatedDifficult = 4;
                isDifficultChange = true;
                break;
            case 100:
                _calculatedDifficult = 5;
                isDifficultChange = true;
                break;
        }
        if (_difficult == _calculatedDifficult) return false;

        _difficult = Convert.ToInt32(_calculatedDifficult);
        return isDifficultChange;
    }

    private void SetGameSpeed()
    {
        switch (_difficult)
        {
            case 2:
                GameSpeedMultiplayer = 1.1f;
                break;
            case 3:
                GameSpeedMultiplayer = 1.2f;
                break;
            case 4:
                GameSpeedMultiplayer = 1.3f;
                break;
            case 5:
                GameSpeedMultiplayer = 1.4f;
                break;
            default:
                GameSpeedMultiplayer = 1f;
                break;
        }
        if(Time.timeScale != 0 && Time.timeScale != GameSpeedMultiplayer && Time.timeScale != GameSpeedMultiplayer/2)
        {
            Time.timeScale = GameSpeedMultiplayer;

            OnGameSpeedChange.Invoke();
            Debug.Log("OnGameSpeedChange.Invoke()");
        }
    }

    IEnumerator ScreenDarker()
    {
        float alpha = 0.9f;
        while (_darkMask.GetComponent<SpriteRenderer>().color.a > 0)
        {
            alpha -= 0.02f;
            _darkMask.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, alpha);
            yield return new WaitForEndOfFrame();
        }

        StopCoroutine(ScreenDarker());
    }
}

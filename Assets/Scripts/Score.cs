using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] internal int _score;
    [SerializeField] internal int _coveredScore;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _coveredScoreText;
    private void Start()
    {
        _score = 0;
        _coveredScore = 0;
    }

    private void Update()
    {
        _scoreText.text = _score.ToString();
        _coveredScoreText.text = _coveredScore.ToString();

    }

    public void AddScore()
    {
        _score++;
        _coveredScore++;
    }
}

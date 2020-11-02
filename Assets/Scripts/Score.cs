using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] internal int ScoreCount;
    [SerializeField] internal int CoveredScoreCount;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _coveredScoreText;
    private void Start()
    {
        ScoreCount = 0;
        CoveredScoreCount = 0;
    }

    private void Update()
    {
        _scoreText.text = ScoreCount.ToString();
        _coveredScoreText.text = CoveredScoreCount.ToString();

    }

    public void AddScore()
    {
        ScoreCount++;
        CoveredScoreCount++;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int _score;
    [SerializeField] private Text _scoreText;
    void Start()
    {
        _score = 0;
    }

    void Update()
    {
        _scoreText.text = _score.ToString();
        Debug.Log(_score);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ScoreZone")
        {
            _score++;
        }
    }
}

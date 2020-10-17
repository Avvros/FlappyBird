using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    internal int _score;
    [SerializeField] private Text _scoreText;
    private void Start()
    {
        _score = 0;
    }

    private void Update()
    {
        _scoreText.text = _score.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ScoreZone")
        {
            _score++;
        }
    }
}

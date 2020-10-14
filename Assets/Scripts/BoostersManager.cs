using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BoostersManager : MonoBehaviour
{
    [SerializeField] private GameObject _bird;
    [SerializeField] private GameObject _birdIdle;
    [SerializeField] private GameObject _birdBlue;
    [SerializeField] private GameObject _timeSlower;
    void Start()
    {

    }

    void Update()
    {
    }

    public void DoSlowDownTime()
    {
        StartCoroutine(SlowDownTime());
    }

    IEnumerator SlowDownTime()
    {
        _timeSlower.SetActive(true);
        _birdBlue.SetActive(true);
        _birdIdle.SetActive(false);

        Time.timeScale = 0.5f;
        for (int i = 10; i > 0; i--)
        {
            _timeSlower.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }

        _timeSlower.SetActive(false);
        _birdBlue.SetActive(false);
        _birdIdle.SetActive(true);

        if (_bird != null)
        {
            Time.timeScale = 1f;
        }
    }

    
}

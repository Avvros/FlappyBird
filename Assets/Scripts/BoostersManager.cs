using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BoostersManager : MonoBehaviour
{
    [SerializeField] private GameObject _boostersObj;

    [SerializeField] internal GameObject _bird;
    [SerializeField] internal GameObject _birdIdle;
    [SerializeField] internal GameObject _birdBlue;

    [SerializeField] internal GameObject _timeSlowerTimer;

    [SerializeField] internal GameObject _birdSheld;
    [SerializeField] internal GameObject _sheldTimer;

    [SerializeField] internal GameObject _ninjaMask;
    [SerializeField] internal GameObject _ninjaTimer;

    private Boosters _boosters;

    void Start()
    {
        _boosters = _boostersObj.GetComponent<Boosters>();
    }

    void Update()
    {
    }

    public void UseBooster(string booster)
    {
        switch (booster)
        {
            case "TimeDilation":
                _boosters.DoSlowDownTime();
                break;
            case "Sheld":
                _boosters.ActiateSheld();
                break;
            case "NinjaMode":
                _boosters.ActivateNinjaMode();
                break;
            default:
                break;
        }
    }
}

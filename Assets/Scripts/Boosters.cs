using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static BoostersManager;

public class Boosters : MonoBehaviour
{
    [SerializeField] private BoostersManager _bM;
    private float _thisTimeSpeed = 1f;

    public void DoSlowDownTime()
    {
        StartCoroutine(SlowDownTime());
    }
    IEnumerator SlowDownTime()
    {
        _bM._timeSlowerTimer.SetActive(true);
        _bM._birdBlue.SetActive(true);
        _bM._birdIdle.SetActive(false);

        _thisTimeSpeed = 0.5f;
        Time.timeScale = _thisTimeSpeed;
        for (int i = 10; i > 0; i--)
        {
            _bM._timeSlowerTimer.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }


        _bM._timeSlowerTimer.SetActive(false);
        _bM._birdBlue.SetActive(false);
        _bM._birdIdle.SetActive(true);

        if (_bM._bird != null)
        {
            _thisTimeSpeed = 1f;
            Time.timeScale = _thisTimeSpeed;
        }
    }

    public void ActiateSheld()
    {
        StartCoroutine(SheldActiator());
    }
    IEnumerator SheldActiator()
    {
        Time.timeScale = _thisTimeSpeed;
        _bM._sheldTimer.SetActive(true);
        _bM._birdSheld.SetActive(true);
        _bM._bird.GetComponent<Collider2D>().tag = "SheldedPlayer";
        for (int i = 15; i > 0; i--)
        {
            _bM._sheldTimer.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }

        _bM._sheldTimer.SetActive(false);
        _bM._birdSheld.SetActive(false);

        if (_bM._bird != null)
        {
            _bM._bird.GetComponent<Collider2D>().tag = "Player";
        }
    }

    public void ActivateNinjaMode()
    {
        StartCoroutine(NinjaModeActivator());
    }
    IEnumerator NinjaModeActivator()
    {
        Time.timeScale = _thisTimeSpeed;
        _bM._ninjaTimer.SetActive(true);
        _bM._ninjaMask.SetActive(true);
        _bM._bird.GetComponent<CapsuleCollider2D>().enabled = false;
        for (int i = 5; i > 0; i--)
        {
            _bM._ninjaTimer.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }

        _bM._ninjaTimer.SetActive(false);
        _bM._ninjaMask.SetActive(false);

        if (_bM._bird != null)
        {
            _bM._bird.GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }
}

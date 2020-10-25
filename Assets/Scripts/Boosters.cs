using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boosters : MonoBehaviour
{
    [SerializeField] private BoostersManager _boosterManager;

    private void FixedUpdate()
    {
        
    }

    /// <summary>
    /// Активатор замедления времени.
    /// </summary>
    public void DoSlowDownTime()
    {
        if (_boosterManager._timeSlowerTimer.activeInHierarchy == true)
        {
            Time.timeScale = _boosterManager._thisTimeSpeed;
            return;
        };
        StartCoroutine(SlowDownTime());
    }
    /// <summary>
    /// Корутина, замедления времени.
    /// </summary>
    IEnumerator SlowDownTime()
    {
        _boosterManager._timeSlowerTimer.SetActive(true);
        _boosterManager._birdBlue.SetActive(true);
        _boosterManager._birdIdle.SetActive(false);

        _boosterManager._thisTimeSpeed /= 2;
        Time.timeScale = _boosterManager._thisTimeSpeed;
        for (int i = 10; i > 0; i--)
        {
            _boosterManager._timeSlowerTimer.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }

        _boosterManager._timeSlowerTimer.SetActive(false);
        _boosterManager._birdBlue.SetActive(false);
        _boosterManager._birdIdle.SetActive(true);

        if (_boosterManager._bird != null)
        {
            _boosterManager._thisTimeSpeed *= 2;
            Time.timeScale = _boosterManager._thisTimeSpeed;
        }
    }

    /// <summary>
    /// Активатор щита.
    /// </summary>
    public void ActiateSheld()
    {
        if (_boosterManager._sheldTimer.activeInHierarchy == true)
        {
            Time.timeScale = _boosterManager._thisTimeSpeed;
            return;
        }
        StartCoroutine(SheldActiator());
    }
    /// <summary>
    /// Корутина, применяющая щит.
    /// </summary>
    IEnumerator SheldActiator()
    {
        Time.timeScale = _boosterManager._thisTimeSpeed;
        _boosterManager._sheldTimer.SetActive(true);
        _boosterManager._birdSheld.SetActive(true);
        _boosterManager._bird.GetComponent<Collider2D>().tag = "SheldedPlayer";
        for (int i = 15; i > 0; i--)
        {
            _boosterManager._sheldTimer.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }

        _boosterManager._sheldTimer.SetActive(false);
        _boosterManager._birdSheld.SetActive(false);

        if (_boosterManager._bird != null)
        {
            _boosterManager._bird.GetComponent<Collider2D>().tag = "Player";
        }
    }
    /// <summary>
    /// Активатор режима ниндзи.
    /// </summary>
    public void ActivateNinjaMode()
    {
        if (_boosterManager._ninjaTimer.activeInHierarchy == true)
        {
            Time.timeScale = _boosterManager._thisTimeSpeed;
            return;
        }
        StartCoroutine(NinjaModeActivator());
    }
    /// <summary>
    /// Корутина, для маски ниндзи.
    /// </summary>
    IEnumerator NinjaModeActivator()
    {
        Time.timeScale = _boosterManager._thisTimeSpeed;
        _boosterManager._ninjaTimer.SetActive(true);
        _boosterManager._ninjaMask.SetActive(true);
        _boosterManager._bird.GetComponent<CapsuleCollider2D>().enabled = false;
        for (int i = 5; i > 0; i--)
        {
            _boosterManager._ninjaTimer.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }

        _boosterManager._ninjaTimer.SetActive(false);
        _boosterManager._ninjaMask.SetActive(false);

        if (_boosterManager._bird != null)
        {
            _boosterManager._bird.GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }
}

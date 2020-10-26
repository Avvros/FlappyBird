using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boosters : MonoBehaviour
{
    [SerializeField] private BoostersManager _boosterManager;

    private void ActivateElems(GameObject[] elems)
    {
        foreach (var elem in elems)
        {
            elem.SetActive(true);
        }
    }
    private void DeActivateElems(GameObject[] elems)
    {
        foreach (var elem in elems)
        {
            elem.SetActive(false);
        }
    }
    private void ReverseElemsContition(GameObject[] elems)
    {
        foreach (var elem in elems)
        {
            if (elem.activeInHierarchy)
            {
                elem.SetActive(false);
            }
            else
            {
                elem.SetActive(true);
            }
        }
    }

    private void SetTimeAsThisTimeSpeed()
    {
        Time.timeScale = _boosterManager._thisTimeSpeed;
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
        ActivateElems(new GameObject[]{
            _boosterManager._timeSlowerTimer, 
            _boosterManager._birdBlue
        });
        DeActivateElems(new GameObject[] {
         _boosterManager._birdIdle
        });
        _boosterManager._thisTimeSpeed /= 2;
        SetTimeAsThisTimeSpeed();

        for (int i = _boosterManager._timeSlowerDuration; i > 0; i--)
        {
            _boosterManager._timeSlowerTimer.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }

        ReverseElemsContition(new GameObject[]
        {
            _boosterManager._timeSlowerTimer,
            _boosterManager._birdBlue,
            _boosterManager._birdIdle
        });
        if (_boosterManager._bird != null)
        {
            _boosterManager._thisTimeSpeed *= 2;
            SetTimeAsThisTimeSpeed();
        }
    }

    /// <summary>
    /// Активатор щита.
    /// </summary>
    public void ActiateSheld()
    {
        if (_boosterManager._sheldTimer.activeInHierarchy == true)
        {
            SetTimeAsThisTimeSpeed();
            return;
        }
        StartCoroutine(SheldActiator());
    }
    /// <summary>
    /// Корутина, применяющая щит.
    /// </summary>
    IEnumerator SheldActiator()
    {
        SetTimeAsThisTimeSpeed();
        ActivateElems(new GameObject[]
        {
            _boosterManager._sheldTimer,
            _boosterManager._birdSheld
        });
        _boosterManager._bird.GetComponent<Collider2D>().tag = "SheldedPlayer";

        for (int i = _boosterManager._sheldDuration; i > 0; i--)
        {
            _boosterManager._sheldTimer.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }

        ReverseElemsContition(new GameObject[]
        {
            _boosterManager._sheldTimer,
            _boosterManager._birdSheld
        });
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
            SetTimeAsThisTimeSpeed();
            return;
        }
        StartCoroutine(NinjaModeActivator());
    }
    /// <summary>
    /// Корутина, для маски ниндзи.
    /// </summary>
    IEnumerator NinjaModeActivator()
    {
        SetTimeAsThisTimeSpeed();
        ActivateElems(new GameObject[]
        {
            _boosterManager._ninjaTimer,
            _boosterManager._ninjaMask
        });
        _boosterManager._bird.GetComponent<CapsuleCollider2D>().enabled = false;

        for (int i = _boosterManager._ninjaMaskDuration; i > 0; i--)
        {
            _boosterManager._ninjaTimer.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }

        ReverseElemsContition(new GameObject[]
        {
            _boosterManager._ninjaTimer,
            _boosterManager._ninjaMask
        });
        if (_boosterManager._bird != null)
        {
            _boosterManager._bird.GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }
    /// <summary>
    /// Активатор уменьшения птицы.
    /// </summary>
    public void ActivateReduceBird()
    {
        if (_boosterManager._reduceBirdTimer.activeInHierarchy == true)
        {
            SetTimeAsThisTimeSpeed();
            return;
        }
        StartCoroutine(ReduceBirdActivator());
    }
    /// <summary>
    /// Корутина, для уменьшения птицы.
    /// </summary>
    IEnumerator ReduceBirdActivator()
    {
        SetTimeAsThisTimeSpeed(); 
        ActivateElems(new GameObject[]
        {
            _boosterManager._reduceBirdTimer
        });
        _boosterManager._bird.transform.localScale /= 2;
        _boosterManager._bird.GetComponent<Rigidbody2D>().gravityScale /= 1.5f;


        for (int i = _boosterManager._reduceBirdDuration; i > 0; i--)
        {
            _boosterManager._reduceBirdTimer.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }

        DeActivateElems(new GameObject[]
        {
            _boosterManager._reduceBirdTimer
        });
        _boosterManager._bird.transform.localScale *= 2;
        _boosterManager._bird.GetComponent<Rigidbody2D>().gravityScale *= 1.5f;

        if (_boosterManager._bird != null)
        {
            _boosterManager._bird.GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }
}

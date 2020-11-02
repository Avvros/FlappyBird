using System.Collections;
using UnityEngine;
using static Utils;

public class Boosters : MonoBehaviour
{
    [SerializeField] private BoostersManager _boosterManager;

    #region Slowdown Time
    /// <summary>
    /// Активатор замедления времени.
    /// </summary>
    public void DoSlowDownTime()
    {
        StartBooster(_boosterManager.TimeSlowerTimer, "SlowDownTime");
    }
    /// <summary>
    /// Корутина замедления времени.
    /// </summary>
    IEnumerator SlowDownTime()
    {
        ActivateElemsInHierarchy(new GameObject[]{
            _boosterManager.TimeSlowerTimer, 
            _boosterManager.BirdBlue
        });
        DeactivateElemsInHierarchy(new GameObject[] {
            _boosterManager.BirdIdle
        });
        _boosterManager.BooferTimeSpeed /= 2;
        SetTimeAsBooferTimeSpeed();

        for (int remainSecond = _boosterManager.TimeSlowerDuration; remainSecond > 0; remainSecond--)
        {
            if (_boosterManager.GameManagerHanler.GameIsStopped)
            {
                ++remainSecond;
            }
            SetTextOnUIObject(_boosterManager.TimeSlowerTimer, remainSecond);
            yield return new WaitForSecondsRealtime(1);
        }

        ReverseElemsCondition(new GameObject[]
        {
            _boosterManager.TimeSlowerTimer,
            _boosterManager.BirdBlue,
            _boosterManager.BirdIdle
        });
        if (_boosterManager.Bird != null)
        {
            _boosterManager.BooferTimeSpeed *= 2;
            SetTimeAsBooferTimeSpeed();
        }
    }
    #endregion
    #region Sheld
    /// <summary>
    /// Активатор щита.
    /// </summary>
    public void ActiateSheld()
    {
        StartBooster(_boosterManager.SheldTimer, "SheldActiator");
    }
    /// <summary>
    /// Корутина, создающая щит.
    /// </summary>
    IEnumerator SheldActiator()
    {
        SetTimeAsBooferTimeSpeed();
        ActivateElemsInHierarchy(new GameObject[]
        {
            _boosterManager.SheldTimer,
            _boosterManager.BirdSheld
        });
        _boosterManager.Bird.GetComponent<Collider2D>().tag = "SheldedPlayer";

        for (int remainSecond = _boosterManager.SheldDuration; remainSecond > 0; remainSecond--)
        {
            if (_boosterManager.GameManagerHanler.GameIsStopped)
            {
                ++remainSecond;
            }
            SetTextOnUIObject(_boosterManager.SheldTimer, remainSecond);
            yield return new WaitForSecondsRealtime(1);
        }

        ReverseElemsCondition(new GameObject[]
        {
            _boosterManager.SheldTimer,
            _boosterManager.BirdSheld
        });
        if (_boosterManager.Bird != null)
        {
            _boosterManager.Bird.GetComponent<Collider2D>().tag = "Player";
        }
    }
    #endregion
    #region Ninja Mode
    /// <summary>
    /// Активатор режима ниндзи.
    /// </summary>
    public void ActivateNinjaMode()
    {
        StartBooster(_boosterManager.NinjaTimer, "NinjaModeActivator");
    }
    /// <summary>
    /// Корутина, для маски ниндзи.
    /// </summary>
    IEnumerator NinjaModeActivator()
    {
        SetTimeAsBooferTimeSpeed();
        ActivateElemsInHierarchy(new GameObject[]
        {
            _boosterManager.NinjaTimer,
            _boosterManager.NinjaMask
        });
        _boosterManager.Bird.GetComponent<CapsuleCollider2D>().enabled = false;

        for (int remainSecond = _boosterManager.SheldDuration; remainSecond > 0; remainSecond--)
        {
            if (_boosterManager.GameManagerHanler.GameIsStopped)
            {
                ++remainSecond;
            }
            SetTextOnUIObject(_boosterManager.NinjaTimer, remainSecond);
            yield return new WaitForSecondsRealtime(1);
        }

        ReverseElemsCondition(new GameObject[]
        {
            _boosterManager.NinjaTimer,
            _boosterManager.NinjaMask
        });
        if (_boosterManager.Bird != null)
        {
            _boosterManager.Bird.GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }
    #endregion
    #region Reduce Bird
    /// <summary>
    /// Активатор уменьшения птицы.
    /// </summary>
    public void ActivateReduceBird()
    {
        StartBooster(_boosterManager.ReduceBirdTimer, "ReduceBirdActivator");
    }
    /// <summary>
    /// Корутина для уменьшения птицы.
    /// </summary>
    IEnumerator ReduceBirdActivator()
    {
        SetTimeAsBooferTimeSpeed(); 
        ActivateElemsInHierarchy(new GameObject[]
        {
            _boosterManager.ReduceBirdTimer
        });
        _boosterManager.Bird.transform.localScale /= 2;
        _boosterManager.Bird.GetComponent<Rigidbody2D>().gravityScale /= 1.5f;


        for (int remainSecond = _boosterManager.SheldDuration; remainSecond > 0; remainSecond--)
        {
            if (_boosterManager.GameManagerHanler.GameIsStopped)
            {
                ++remainSecond;
            }
            SetTextOnUIObject(_boosterManager.ReduceBirdTimer, remainSecond);
            yield return new WaitForSecondsRealtime(1);
        }

        DeactivateElemsInHierarchy(new GameObject[]
        {
            _boosterManager.ReduceBirdTimer
        });
        _boosterManager.Bird.transform.localScale *= 2;
        _boosterManager.Bird.GetComponent<Rigidbody2D>().gravityScale *= 1.5f;

        if (_boosterManager.Bird != null)
        {
            _boosterManager.Bird.GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }
    #endregion
    
    #region Helper Methods
    private void StartBooster(GameObject boosterTimer, string methodName)
    {
        if (CheckBoosterActivity(boosterTimer))
        {
            _boosterManager.GameManagerHanler.GameIsStopped = false;
            StartCoroutine(methodName);
        }
    }

    private bool CheckBoosterActivity(GameObject boosterTimer)
    {
        if (boosterTimer.activeInHierarchy)
        {
            Time.timeScale = _boosterManager.BooferTimeSpeed;
            return false;
        };
        
        return true;
    }

    private void SetTimeAsBooferTimeSpeed()
    {
        Time.timeScale = _boosterManager.BooferTimeSpeed;
    }
    #endregion
}

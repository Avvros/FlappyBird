using System;
using UnityEngine;
using UnityEngine.UI;

public class BoostersManager : MonoBehaviour
{
    [SerializeField] private GameObject _boostersObj;
    [SerializeField] private GameObject _scoreCounterOnBird; // Счёт, хранящийся в птице.
    [SerializeField] private GameObject _doNotUseText; // Текст для несрабатывания способностей.
    private Animator anim_doNotUseText;
   [Header("Skins")]
    [SerializeField] internal GameObject _bird;
    [SerializeField] internal GameObject _birdIdle;
    [SerializeField] internal GameObject _birdBlue;
    [Header("Time Slower")]
    [SerializeField] internal GameObject _timeSlowerIcon;
    [SerializeField] internal GameObject _timeSlowerTimer;
    [SerializeField] internal int _timeSlowerCost = 15;
    [SerializeField] internal Text _timeSlowerCostText;
    [Header("Sheld")]
    [SerializeField] internal GameObject _birdSheldIcon;
    [SerializeField] internal GameObject _birdSheld;
    [SerializeField] internal GameObject _sheldTimer;
    [SerializeField] internal int _sheldCost = 5;
    [SerializeField] internal Text _sheldCostText;
    [Header("Ninja Mode")]
    [SerializeField] internal GameObject _ninjaMaskIcon;
    [SerializeField] internal GameObject _ninjaMask;
    [SerializeField] internal GameObject _ninjaTimer;
    [SerializeField] internal int _ninjaMaskCost = 20;
    [SerializeField] internal Text _ninjaMaskCostText;

    private Boosters _boosters; // Объект, хранящий иконки способностей.

    void Start()
    {
        _boosters = _boostersObj.GetComponent<Boosters>();
        anim_doNotUseText = _doNotUseText.GetComponent<Animator>();
        SetCostText(_timeSlowerCostText, _timeSlowerCost);
        SetCostText(_sheldCostText, _sheldCost);
        SetCostText(_ninjaMaskCostText, _ninjaMaskCost);
    }

    void Update()
    {
        if (_bird == null)
        {
            HideBoosters();
        }
    }

    /// <summary>
    /// Скрыть способности.
    /// </summary>
    private void HideBoosters()
    {
        _timeSlowerIcon.SetActive(false);
        _birdSheldIcon.SetActive(false);
        _ninjaMaskIcon.SetActive(false);
    }
    /// <summary>
    /// Использовать способность.
    /// </summary>
    /// <param name="booster">Имя способности: TimeDilation, Sheld, NinjaMode.</param>
    public void UseBooster(string booster)
    {
        var _isBoosterUsed = false;
        switch (booster)
        {
            case "TimeDilation":
                if(GetScore(_timeSlowerCost, out _isBoosterUsed)) _boosters.DoSlowDownTime();
                break;
            case "Sheld":
                if (GetScore(_sheldCost, out _isBoosterUsed)) _boosters.ActiateSheld();
                break;
            case "NinjaMode":
                if (GetScore(_ninjaMaskCost, out _isBoosterUsed)) _boosters.ActivateNinjaMode();
                break;
        }
        if(!_isBoosterUsed)
        {
            ShowDoNotUseText();
            Invoke("HideDoNotUseText", 2); // Через две секунды активирует анимацию скрытного текста.
            Time.timeScale = 1;
        }
    }
    /// <summary>
    /// Берёт очки, нужные способности, если хватает.
    /// </summary>
    /// <param name="boosterCost">Количесвто очков, нужное для сособности.</param>
    /// <param name="_isBoosterUsed">Хватило ли очков для использования.</param>
    /// <returns>Результат проверки на достаточное количество очков.</returns>
    private bool GetScore(int boosterCost, out bool _isBoosterUsed)
    {
        int _score = _scoreCounterOnBird.GetComponent<Score>()._score;

        if(_score - boosterCost >= 0)
        {
            _scoreCounterOnBird.GetComponent<Score>()._score -= boosterCost;
            return _isBoosterUsed = true;
        }

        return _isBoosterUsed = false;
    }
    /// <summary>
    /// Устанавливает стоимость способности в текстовый элемент.
    /// </summary>
    /// <param name="text">Целевой текст.</param>
    /// <param name="cost">Стоимость.</param>
    private void SetCostText(Text text, int cost)
    {
        text.text = "Стоимость: " + cost.ToString() + "очков";
    }
    /// <summary>
    /// Активирует анимацию показа текста неактивации способности.
    /// </summary>
    private void ShowDoNotUseText()
    {
        anim_doNotUseText.SetBool("ShowDoNotUse", true);
    }
    /// <summary>
    /// Активирует анимацию скрытого текста неактивации способности.
    /// </summary>
    private void HideDoNotUseText()
    {
        Debug.Log("HideDoNotUseText");
        anim_doNotUseText.SetBool("ShowDoNotUse", false);
    }
}

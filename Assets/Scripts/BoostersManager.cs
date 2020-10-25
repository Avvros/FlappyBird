using System;
using UnityEngine;
using UnityEngine.UI;

public class BoostersManager : MonoBehaviour
{
    [SerializeField] private GameObject _boostersObj;
    [SerializeField] private GameObject _scoreCounter;
    private Score _scoreCounterHandler;
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
    internal bool _isTimeSlow = false;
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

    [SerializeField] private GameObject _gameManager;
    GameManager _gameManagerHanler;
    internal float _thisTimeSpeed = 1f; // Буфер, хранящий текущее время.


    void Start()
    {
        _boosters = _boostersObj.GetComponent<Boosters>();
        _gameManagerHanler = _gameManager.GetComponent<GameManager>();
        anim_doNotUseText = _doNotUseText.GetComponent<Animator>();
        _scoreCounterHandler = _scoreCounter.GetComponent<Score>();
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
    public void ChangeThisTimeSpeed()
    {
        if (_isTimeSlow)
        {
            _thisTimeSpeed = _gameManagerHanler._gameSpeedMultiplayer / 2;
        }
        _thisTimeSpeed = _gameManagerHanler._gameSpeedMultiplayer;
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
            Time.timeScale = _thisTimeSpeed;
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
        if (_scoreCounterHandler == null) return _isBoosterUsed = false;

        int _score = _scoreCounterHandler._score;
        if (_score - boosterCost >= 0)
        {
            _scoreCounterHandler._score -= boosterCost;
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
        if (text == null) return;
        text.text = "Стоимость: " + cost.ToString() + " очков.";
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
        anim_doNotUseText.SetBool("ShowDoNotUse", false);
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;

public class BoostersManager : MonoBehaviour
{
    private Boosters _boosters; // Объект, хранящий иконки способностей.

    [SerializeField] private GameObject _gameManager;
    GameManager _gameManagerHanler;
    internal float _thisTimeSpeed = 1f; // Буфер, хранящий текущее время.

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
    [SerializeField] internal int _timeSlowerDuration = 10;
    internal bool _isTimeSlow = false;
    [Header("Sheld")]
    [SerializeField] internal GameObject _birdSheldIcon;
    [SerializeField] internal GameObject _birdSheld;
    [SerializeField] internal GameObject _sheldTimer;
    [SerializeField] internal int _sheldCost = 5;
    [SerializeField] internal Text _sheldCostText;
    [SerializeField] internal int _sheldDuration = 15;
    [Header("Ninja Mode")]
    [SerializeField] internal GameObject _ninjaMaskIcon;
    [SerializeField] internal GameObject _ninjaMask;
    [SerializeField] internal GameObject _ninjaTimer;
    [SerializeField] internal int _ninjaMaskCost = 20;
    [SerializeField] internal Text _ninjaMaskCostText;
    [SerializeField] internal int _ninjaMaskDuration = 5;
    [Header("Reduce Bird")]
    [SerializeField] internal GameObject _reduceBirdIcon;
    [SerializeField] internal GameObject _reduceBirdTimer;
    [SerializeField] internal int _reduceBirdCost = 15;
    [SerializeField] internal Text _reduceBirdCostText;
    [SerializeField] internal int _reduceBirdDuration = 10;


    void Start()
    {
        // Не трогай!
        _boosters = _boostersObj.GetComponent<Boosters>();
        _gameManagerHanler = _gameManager.GetComponent<GameManager>();
        anim_doNotUseText = _doNotUseText.GetComponent<Animator>();
        _scoreCounterHandler = _scoreCounter.GetComponent<Score>();

        
        // Доавлять при создании способности!!!
        SetCostText(_timeSlowerCostText, _timeSlowerCost);
        SetCostText(_sheldCostText, _sheldCost);
        SetCostText(_ninjaMaskCostText, _ninjaMaskCost);
        SetCostText(_reduceBirdCostText, _reduceBirdCost);
		
		ChangeBoostersCondition(false);
    }

    void Update()
    {
        if (_bird == null)
        {
            ChangeBoostersCondition(false);
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
    internal void ChangeBoostersCondition(bool condition)
    {
		_timeSlowerIcon.SetActive(condition);
		_birdSheldIcon.SetActive(condition);
		_ninjaMaskIcon.SetActive(condition);
		_reduceBirdIcon.SetActive(condition);
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
                    case "ReduceBird":
                if (GetScore(_reduceBirdCost, out _isBoosterUsed)) _boosters.ActivateReduceBird();
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

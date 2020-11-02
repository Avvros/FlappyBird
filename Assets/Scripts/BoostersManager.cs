using UnityEngine;
using UnityEngine.UI;

public class BoostersManager : MonoBehaviour
{
    private Boosters Boosters; // Объект, хранящий иконки способностей.

    [SerializeField] internal GameObject GameManager;
    [SerializeField] private GameObject _boostersObj;
    [SerializeField] private GameObject _scoreCounter;
    [SerializeField] private GameObject _doNotUseText; // Текст для несрабатывания способностей.
    internal GameManager GameManagerHanler;
    internal float BooferTimeSpeed = 1f; // Буфер, хранящий текущее время.
    private Score _scoreCounterHandler;
    private Animator anim_doNotUseText;

    [Header("Skins")]
    [SerializeField] internal GameObject Bird;
    [SerializeField] internal GameObject BirdIdle;
    [SerializeField] internal GameObject BirdBlue;
    [Header("Time Slower")]
    [SerializeField] internal GameObject TimeSlowerIcon;
    [SerializeField] internal GameObject TimeSlowerTimer;
    [SerializeField] internal int TimeSlowerCost = 7;
    [SerializeField] internal Text TimeSlowerCostText;
    [SerializeField] internal int TimeSlowerDuration = 10;
    internal bool IsTimeSlow = false;
    [Header("Sheld")]
    [SerializeField] internal GameObject BirdSheldIcon;
    [SerializeField] internal GameObject BirdSheld;
    [SerializeField] internal GameObject SheldTimer;
    [SerializeField] internal int SheldCost = 10;
    [SerializeField] internal Text SheldCostText;
    [SerializeField] internal int SheldDuration = 7;
    [Header("Ninja Mode")]
    [SerializeField] internal GameObject NinjaMaskIcon;
    [SerializeField] internal GameObject NinjaMask;
    [SerializeField] internal GameObject NinjaTimer;
    [SerializeField] internal int NinjaMaskCost = 15;
    [SerializeField] internal Text NinjaMaskCostText;
    [SerializeField] internal int NinjaMaskDuration = 5;
    [Header("Reduce Bird")]
    [SerializeField] internal GameObject ReduceBirdIcon;
    [SerializeField] internal GameObject ReduceBirdTimer;
    [SerializeField] internal int ReduceBirdCost = 15;
    [SerializeField] internal Text ReduceBirdCostText;
    [SerializeField] internal int ReduceBirdDuration = 10;


    void Start()
    {
        // Не трогай!
        Boosters = _boostersObj.GetComponent<Boosters>();
        GameManagerHanler = GameManager.GetComponent<GameManager>();
        anim_doNotUseText = _doNotUseText.GetComponent<Animator>();
        _scoreCounterHandler = _scoreCounter.GetComponent<Score>();

        
        // Доавлять при создании способности!!!
        SetCostText(TimeSlowerCostText, TimeSlowerCost);
        SetCostText(SheldCostText, SheldCost);
        SetCostText(NinjaMaskCostText, NinjaMaskCost);
        SetCostText(ReduceBirdCostText, ReduceBirdCost);
		
		ChangeBoostersCondition(false);
    }

    void Update()
    {
        if (Bird == null)
        {
            ChangeBoostersCondition(false);
        }
    }
    public void ChangeThisTimeSpeed()
    {
        if (IsTimeSlow)
        {
            BooferTimeSpeed = GameManagerHanler.GameSpeedMultiplayer / 2;
        }
        BooferTimeSpeed = GameManagerHanler.GameSpeedMultiplayer;
    }
    /// <summary>
    /// Установить активность способностей в иерархии.
    /// </summary>
    internal void ChangeBoostersCondition(bool condition)
    {
		TimeSlowerIcon.SetActive(condition);
		BirdSheldIcon.SetActive(condition);
		NinjaMaskIcon.SetActive(condition);
		ReduceBirdIcon.SetActive(condition);
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
                if(GetScore(TimeSlowerCost, out _isBoosterUsed)) Boosters.DoSlowDownTime();
                break;
            case "Sheld":
                if (GetScore(SheldCost, out _isBoosterUsed)) Boosters.ActiateSheld();
                break;
            case "NinjaMode":
                if (GetScore(NinjaMaskCost, out _isBoosterUsed)) Boosters.ActivateNinjaMode();
                break;
                    case "ReduceBird":
                if (GetScore(ReduceBirdCost, out _isBoosterUsed)) Boosters.ActivateReduceBird();
                break;
        }
        if(!_isBoosterUsed)
        {
            ShowDoNotUseText();
            Invoke("HideDoNotUseText", 2); // Через две секунды активирует анимацию скрытного текста.
            Time.timeScale = BooferTimeSpeed;
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

        int _score = _scoreCounterHandler.ScoreCount;
        if (_score - boosterCost >= 0)
        {
            _scoreCounterHandler.ScoreCount -= boosterCost;
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

    /// <summary>
    /// Установить активность способностей в иерархии.
    /// </summary>
    /// <param name="activity">Значение активности.</param>
    public void SetBoostersIconsActivity(bool activity)
    {
        TimeSlowerIcon.SetActive(activity);
        BirdSheldIcon.SetActive(activity);
        NinjaMaskIcon.SetActive(activity);
        ReduceBirdIcon.SetActive(activity);
    }
}

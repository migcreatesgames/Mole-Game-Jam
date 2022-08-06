using UnityEngine;
using TMPro;

public class CalendarCountdown : MonoBehaviour
{
    public float _gameLengthDuration = 0;
    private float currentTime = 0;

    private bool _enableFade = false;

    private TextMeshProUGUI textTarget;

    public enum Seasons { winter, spring, fall, summer }
    private Seasons _season = Seasons.winter;
    private SeasonTimes _seasonTimes;
    public TextMeshProUGUI[] SeasonNames;


    private void Awake()
    {
        _gameLengthDuration = GameManager.Instance.GameData.CalandarDuration;
        CalculateSeasonsDurations();
    }
    void Update()
    {
        if (currentTime < _gameLengthDuration && GameManager.Instance.GameStarted)
        {
            Debug.Log($"current time: {currentTime}");
            currentTime += .15f;
            ProgressTime(currentTime);
        }

        if (_enableFade)
            Fade(Time.deltaTime);
    }

    private void ProgressTime(float time) 
    {
        int tmpTime = Mathf.RoundToInt(time);

        // game start

        if (tmpTime == _seasonTimes.Spring)
        {
            if (_season != Seasons.spring)
            {
                _season = Seasons.spring;
                _enableFade = true;
                FadeOutText(_season);
            }
        }
        else if (tmpTime == _seasonTimes.Summer)
        {
            // is summer
            if (_season != Seasons.summer)
            {
                UIManager.Instance.HideHUD();
                _season = Seasons.summer;
                _enableFade = true;
                FadeOutText(_season);
            }
        }
        else if (tmpTime == _seasonTimes.Fall)
        {
            // is fall
            if (_season != Seasons.fall)
            {
                UIManager.Instance.HideHUD();
                _season = Seasons.fall;
                _enableFade = true;
                FadeOutText(_season);
            }
        }
        else if (tmpTime == _seasonTimes.Winter)
        {
            // is Winter
            if (_season != Seasons.winter)
            {
                UIManager.Instance.HideHUD();
                _season = Seasons.winter;
                _enableFade = true;
                GameEvents.OnTimerFinished?.Invoke();
            }
        }
    }
    
    private void CalculateSeasonsDurations()
    {
        int tmp = (int)(_gameLengthDuration / 3);
        Debug.Log($"tmp: {tmp}");
        _seasonTimes.Spring = 0;
        Debug.Log($"_seasonTimes.Spring: {_seasonTimes.Spring}");
        _seasonTimes.Summer = tmp;
        Debug.Log($"_seasonTimes.Summer: {_seasonTimes.Summer}");
        _seasonTimes.Fall = tmp*2;
        Debug.Log($"_seasonTimes.Fall: {_seasonTimes.Fall}");
        _seasonTimes.Winter = tmp*3;
        Debug.Log($"_seasonTimes.Winter: {_seasonTimes.Winter}");
    }
    
    private void FadeOutText(Seasons season)
    {
        switch (season)
        {
            case Seasons.winter:
                textTarget = SeasonNames[0];
                textTarget.alpha = 100;
                break;
            case Seasons.spring:
                textTarget = SeasonNames[1];
                textTarget.alpha = 100;
                break;
            case Seasons.summer:
                textTarget = SeasonNames[2];
                textTarget.alpha = 100;
                break;
            case Seasons.fall:
                textTarget = SeasonNames[3];
                textTarget.alpha = 100;
                break;
            default:
                break;
        }
    }

    private void Fade(float time)
    {
        var alpha = textTarget.color.a;
        if (textTarget.alpha <= 0)
        {
            UIManager.Instance.DisplayHUD();
            _enableFade = false;
        }
        if (textTarget.alpha != 0)
        {
            //Debug.Log(textTarget.alpha);
            alpha -= 15f * time;
            textTarget.color = new Color(textTarget.color.r, textTarget.color.b, textTarget.color.g, alpha);
        }
    }
}

struct SeasonTimes
{
    public int Spring;
    public int Summer;
    public int Fall;
    public int Winter;


}

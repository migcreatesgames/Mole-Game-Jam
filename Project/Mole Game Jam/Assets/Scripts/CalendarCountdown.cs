using UnityEngine;
using TMPro;

public class CalendarCountdown : MonoBehaviour
{
    public float MaxTime = 100000000f; // let's keep this around 5 mins max
    private float currentTime = 0;

    private bool _enableFade = false;

    private TextMeshProUGUI textTarget;

    public enum Seasons { winter, spring, fall, summer }

    private Seasons season = Seasons.winter;
    public TextMeshProUGUI[] SeasonNames;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime < MaxTime)
        {
            Debug.Log($"current time: {currentTime}");
            currentTime += .15f;
            ProgressTime(currentTime);
        }

        Debug.Log($"fade: {_enableFade}");
        if (_enableFade)
        {
            var alpha = textTarget.color.a;
            if (textTarget.alpha != 0)
            {
                Debug.Log(textTarget.alpha);
                alpha -= 15f * Time.deltaTime;
                textTarget.color = new Color (textTarget.color.r, textTarget.color.b, textTarget.color.g, alpha);
            }
            else
            {
                _enableFade = false;
            }
        }
    }

    private void ProgressTime(float time) 
    {
        int tmpTime = Mathf.RoundToInt(time);
        switch (tmpTime)
        {
            case 0:
                // game start
                // is spring
                if (season != Seasons.spring)
                {
                    season = Seasons.spring;
                    _enableFade = true;
                    FadeOutText(season);
                }
                   

                break;

            case 250:
                // is summer
                if (season != Seasons.summer)
                {
                    season = Seasons.summer;
                    FadeOutText(season);
                }
                break;
            
            case 425:
                // is fall
                if (season != Seasons.fall)
                {
                    season = Seasons.fall;
                    FadeOutText(season);
                }
                break;
            case 750:
                // is Winter
                if (season != Seasons.winter)
                {
                    season = Seasons.winter;
                    FadeOutText(season);
                    GameEvents.OnTimerFinished?.Invoke();
                }
                break;
            default:
                break;
        }
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
            case Seasons.fall:
                textTarget = SeasonNames[2];
                textTarget.alpha = 100;
                break;
            case Seasons.summer:
                textTarget = SeasonNames[3];
                textTarget.alpha = 100;
                break;
            default:
                break;
        }
    }
}

using UnityEngine;
using TMPro;

public class CalendarCountdown : MonoBehaviour
{
    public float MaxTime = 500f; // let's keep this around 5 mins max
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
            currentTime--;
            ProgressTime(currentTime);
        }

        if (_enableFade)
        {

        }
    }

    private void ProgressTime(float time) 
    {
        switch (time)
        {
            case 0:
                // game start
                // is spring
                if (season != Seasons.spring)
                {
                    season = Seasons.spring;
                    FadeOutText(season);
                }
                   

                break;

            case 30:
                // is summer
                if (season != Seasons.summer)
                {
                    season = Seasons.summer;
                    FadeOutText(season);
                }
                break;
            
            case 60:
                // is fall
                if (season != Seasons.fall)
                {
                    season = Seasons.fall;
                    FadeOutText(season);
                }
                break;
            case 90:
                // is Winter
                if (season != Seasons.fall)
                {
                    season = Seasons.fall;
                    FadeOutText(season);
                }
                // on game over
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
                break;
            case Seasons.spring:
                textTarget = SeasonNames[1];
                break;
            case Seasons.fall:
                textTarget = SeasonNames[2];
                break;
            case Seasons.summer:
                textTarget = SeasonNames[3];
                break;
            default:
                break;
        }
    }
}

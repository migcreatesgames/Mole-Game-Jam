using UnityEngine;
using TMPro;

public class CalendarCountdown : MonoBehaviour
{
    public float MaxTime = 500f; // let's keep this around 5 mins max
    private float currentTime = 0;
    public enum Seasons { winter, spring, fall, summer }

    private Seasons season = Seasons.spring;
    public TextMeshProUGUI[] SeasonNames;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FadeOutText()
    {

    }
}

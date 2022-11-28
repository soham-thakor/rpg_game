using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;


public class GAMEMANAGER_TIME : MonoBehaviour
{

    public float minutes = 300;
    private int minutesToDisplay, hoursToDisplay, previoiusHoursToDisplay;
    private float percentageDayCompleted, maxMinutesInADay = 1440, timeIncrement = 1, nextIncrement, nextRate = 1;

    public Gradient gradient;
    public Light2D environmentLight;

    public TMP_Text hoursText;
    public TMP_Text minutesText;

    //public delegate void TimeChangedEventHandler(int hours);
    //public event TimeChangedEventHandler EventTimeChanged;

    // Start is called before the first frame update
    void Start()
    {
        SetLightGradient();
        SetClock();
    }

    // Update is called once per frame
    void Update()
    {
        IncrementTime();
        //MakeTimeFaster();
        //MakeTimeNormal();
    }

    void IncrementTime()
    {
        if (Time.time < nextIncrement)
        {
            return;
        }

        nextIncrement = Time.time + nextRate;
        minutes += timeIncrement;
        if (minutes >= maxMinutesInADay)
        {
            minutes = 0;
        }

        SetLightGradient();
        SetClock();
    }

    void SetLightGradient()
    {
        percentageDayCompleted = minutes / maxMinutesInADay;
        environmentLight.color = gradient.Evaluate(percentageDayCompleted);

    }

    void SetClock()
    {
        hoursToDisplay = Mathf.FloorToInt(minutes / 60);

        if(hoursToDisplay >= 24)
        {
            hoursToDisplay = 0;
        }

        minutesToDisplay = (int)(minutes % 60);

        if(previoiusHoursToDisplay != hoursToDisplay)
        {
            previoiusHoursToDisplay = hoursToDisplay;
            //call event
        }

        hoursText.text = hoursToDisplay.ToString("D2");
        minutesText.text = minutesToDisplay.ToString("D2");

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;


public class GameTime : MonoBehaviour
{

    private float minutes;
    private int minutesToDisplay, hoursToDisplay, previoiusHoursToDisplay;
    private float percentageDayCompleted, maxMinutesInADay = 1440, timeIncrement = 1, nextIncrement, nextRate = 0.5f;
    public TimeData timeData;

    // turning this to false, will prevent the clock from brightness in the scene.
    public bool changeLightInScene;

    public Gradient gradient;
    public Light2D environmentLight;

    public TMP_Text hoursText;
    public TMP_Text minutesText;
    public TMP_Text amPM;

    // Start is called before the first frame update
    void Awake()
    {
        // read time from scriptable object
        minutes = timeData.currMinutes;
        
        SetLightGradient();
        SetClock();
    }

    // Update is called once per frame
    void Update()
    {
        IncrementTime();
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
        if(minutes >= maxMinutesInADay / 2)
		{
            amPM.text = "pm";
		}
        else
		{
            amPM.text = "am";
		}

        // set minutes in scriptable object
        timeData.currMinutes = minutes;
        
        if(changeLightInScene) {
            SetLightGradient();
        }
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
        hoursToDisplay = hoursToDisplay % 12;
        if(hoursToDisplay == 0)
		{
            hoursToDisplay = 12;
		}
        /*if(hoursToDisplay >= 24)
        {
            hoursToDisplay = 0;
        }*/

        minutesToDisplay = (int)(minutes % 60);

        if(previoiusHoursToDisplay != hoursToDisplay)
        {
            previoiusHoursToDisplay = hoursToDisplay;
            //call event
        }

        if(hoursToDisplay >= 6 && hoursToDisplay <= 19){
            timeData.isNight = false;

        }else{
            timeData.isNight = true;
        }
  
        hoursText.text = hoursToDisplay.ToString("D2");
        minutesText.text = minutesToDisplay.ToString("D2");
    }

    public float getMinutes()
	{
        return minutes;
	}
}

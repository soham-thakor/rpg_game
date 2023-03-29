using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTime : MonoBehaviour
{
	public enum Hours {
        Twelve_AM,
        One_AM,
        Two_AM,
        Three_AM,
        Four_AM,
        Five_AM,
        Six_AM,
        Seven_AM,
        Eight_AM,
        Nine_AM,
        Ten_AM,
        Eleven_AM,
        Twelve_PM,
        One_PM,
        Two_PM,
        Three_PM,
        Four_PM,
        Five_PM,
        Six_PM,
        Seven_PM,
        Eight_PM,
        Nine_PM,
        Ten_PM,
        Eleven_PM,
        
    }
	public enum Minutes { 
        O_Clock,
        Fifteen,
        Thirty,
        Fourty_Five
    }

    public Hours startActiveHour = Hours.Eight_AM;
    public Minutes startActiveMinutes = Minutes.O_Clock;

    public Hours endActiveHour = Hours.Twelve_AM;
    public Minutes endActiveMinutes = Minutes.O_Clock;

    private GameTime gameTime;
	// Start is called before the first frame update
	void Start()
    {
        gameTime = GameObject.FindGameObjectWithTag("Clock").GetComponent<GameTime>();

        //if we are not between the active hours of this game object then disable it
        float mins = gameTime.getMinutes();
        float startActiveTime = ((int)startActiveHour * 60) + ((int)startActiveMinutes * 15);
        float endActiveTime = ((int)endActiveHour * 60) + ((int)endActiveMinutes * 15);
		string debug = startActiveTime.ToString() + "/" + mins.ToString() + "/" + endActiveTime.ToString() + "-" + gameObject.name;

		Debug.Log(debug);
        //Debug.Log(endActiveTime);
        //Debug.Log(mins);
        //I know these if statments arent great, but they should be simple to understand
        if(startActiveTime < endActiveTime)
		{// Kind of a crude way to do this, but this is to make sure the below statement only happens if the times selected dont reset to zero at noon somewhere inebtween 
            if (!(mins > startActiveTime && mins < endActiveTime))
            { // this doesnt work because if the hours you want cross noon, then the end active time will be less than the start active time
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
        else
		{// if the times selected spawn across noon, we do something different because the time will reset to 0
            if(mins > startActiveTime || mins < endActiveTime)
			{ // if the time is after the start time or before the end time
                //this only works since the time resets back to 0
                gameObject.SetActive(true);
			}
            else
			{
                gameObject.SetActive(false);
			}
		}


        
    }

}

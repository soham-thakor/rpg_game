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
        Debug.Log(startActiveTime);
        Debug.Log(endActiveTime);
        Debug.Log(mins);
        if(!(mins > startActiveTime && mins < endActiveTime))
		{
            gameObject.SetActive(false);
		}
        
    }

}

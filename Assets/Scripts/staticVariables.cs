using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticVariables : MonoBehaviour
{
    public static bool immobile = false;

    //Cooldowns Slider Values
    [System.NonSerialized] public static Dictionary<int, float> cooldowns = new Dictionary<int, float>();

    //Cooldowns Time left Values
    [System.NonSerialized] public static Dictionary<int, float> timeLeft = new Dictionary<int, float>();

    public static void changeTimeLeft(int i, float value)
	{
        if(timeLeft.ContainsKey(i)) {
            timeLeft[i] = value;
        }
        else {
            timeLeft.Add(i, value);
        }
    }
    public static float getTimeLeft(int i)
	{
        if(timeLeft.ContainsKey(i)){
            return timeLeft[i];
        }
        else {
            timeLeft.Add(i, 1f);
            return 1f;
        }
    }
    public static void changeCooldown(int i, float value)
    {
        if(cooldowns.ContainsKey(i)) {
            cooldowns[i] = value;
        }
        else {
            cooldowns.Add(i, value);
        }
    }
    public static float getCooldown(int i)
	{
        if(cooldowns.ContainsKey(i)){
            return cooldowns[i];
        }
        else {
            cooldowns.Add(i, 1f);
            return 0f;
        }
	}

	public static void resetCooldowns()
	{
        foreach(KeyValuePair<int, float> entry in cooldowns)
        {
            cooldowns[entry.Key] = 1f;
        }
	}
}

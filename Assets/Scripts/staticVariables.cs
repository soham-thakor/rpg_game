using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticVariables : MonoBehaviour
{
    public static bool immobile = false;

    //Cooldowns Slider Values
    public static float projectileCooldown = 1f;
    public static float mineCooldown = 1f;
    public static float windCooldown = 1f;
    public static float healCooldown = 1f;
    //Cooldowns Time left Values
    public static float projectileTimeLeft;
    public static float mineTimeLeft;
    public static float windTimeLeft;
    public static float healTimeLeft;





    public static void changeTimeLeft(int i, float value)
	{
        switch (i)
        {
            case 0: // projectile
                projectileTimeLeft = value;
                break;
            case 1: // mine
                mineTimeLeft = value;
                break;
            case 2: // wind
                windTimeLeft = value;
                break;
            case 3: // heal
                healTimeLeft = value;
                break;

        }
    }
    public static float getTimeLeft(int i)
	{
        switch (i)
        {
            case 0: // projectile
                return projectileTimeLeft;
            case 1: // mine
                return mineTimeLeft;
            case 2: // wind
                return windTimeLeft;
            case 3: // heal
                return healTimeLeft;
            default:
                return 0f;

        }
    }
    public static void changeCooldown(int i, float value)
    {
        switch (i)
        {
            case 0: // projectile
                projectileCooldown = value;
                break;
            case 1: // mine
                mineCooldown = value;
                break;
            case 2: // wind
                windCooldown = value;
                break;
            case 3: // heal
                healCooldown = value;
                break;

        }
    }
    public static float getCooldown(int i)
	{
        switch (i)
        {
            case 0: // projectile
                return projectileCooldown;
            case 1: // mine
                return mineCooldown;
            case 2: // wind
                return windCooldown;
            case 3: // heal
                return healCooldown;
			default:
                return 0f;

		}
	}

	public static void resetCooldowns()
	{
        projectileCooldown = 1f;
        healCooldown = 1f;
        mineCooldown = 1f;
        windCooldown = 1f;
	}
}

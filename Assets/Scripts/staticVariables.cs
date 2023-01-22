using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticVariables : MonoBehaviour
{
    public static bool immobile = false;

    //Cooldowns
    public static float projectileCooldown = 1f;
    public static float healCooldown = 1f;
    public static float mineCooldown = 1f;
    public static float windCooldown = 1f;






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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SelectionData : ScriptableObject, IResetOnExitPlay
{
    public int maxChances = 3;
    public int chance = 0;
    public bool bossFight;
    public bool gameOver;
    public void ResetOnExitPlay(){
        chance = 0;
    }

    public int checkChance(){
        return chance;
    }

    public void wrongChoice(){
        chance++;
        if(chance == maxChances){
            gameOver = true;
            Debug.Log("Game Over!");
        }
    }

    public void rightChoice(){
        bossFight = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject, IResetOnExitPlay
{

    // Non constant variables, change throughout playtime
    public Vector2 initialValue = Vector2.zero;
    public bool movedScene = false;
    public float currentHealth = 1000;
    public float maxHealth = 1000;
    public float moveSpeed = 1;
    
    public float biteCoolDown = 2;
    public float mineCoolDown = 2;
    public float speedCoolDown = 2;

    public void ResetOnExitPlay()
    {
        // reset values to default whenever exiting playmode
        initialValue = Vector2.zero;
        movedScene = false;
        currentHealth = 1000;
        maxHealth = 1000;
        moveSpeed = 1;
        
        biteCoolDown = 2;
        mineCoolDown = 2;
        speedCoolDown = 2;
    }    
}

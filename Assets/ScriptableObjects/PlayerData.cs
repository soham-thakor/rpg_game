using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject, IResetOnExitPlay
{
    // handle scene transitions
    public Vector2 initialValue;
    public bool movedScene = false;

    // handle player stats
    public float currentHealth = 100f;
    public float moveSpeed = 1f;
     // this pretty much stays constant, its necessary to set max health in healthbar
    public float maxHealth = 100f; 

    public void ResetOnExitPlay()
    {
        // reset values to default whenever exitting playmode
        movedScene = false;
        currentHealth = 100f;
        moveSpeed = 1f;
        maxHealth = 100f;
    }    
}

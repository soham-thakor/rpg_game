using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject, IResetOnExitPlay
{

    // Non constant variables, change throughout playtime
    public Vector2 initialValue = Vector2.zero;
    public bool movedScene = false;
    public float currentHealth = 700;
    public float maxHealth = 700;
    public float moveSpeed = 1;

    public void ResetOnExitPlay()
    {
        // reset values to default whenever exiting playmode
        initialValue = Vector2.zero;
        movedScene = false;
        currentHealth = 700;
        maxHealth = 700;
        moveSpeed = 1;
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySlot : MonoBehaviour
{
    [SerializeField] private float cooldownTime;
    [System.NonSerialized] public bool inCooldown;

    public void StartCooldown() 
    {
        inCooldown = true;
        var timeLeft = cooldownTime;

        while(timeLeft >= 0){ 
            timeLeft -= 1f;
        }

        inCooldown = false;
    }
}

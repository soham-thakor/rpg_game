using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TimeData : ScriptableObject, IResetOnExitPlay
{
    public float currMinutes = 720;
    public void ResetOnExitPlay()
    {
        currMinutes = 720;
    }
}

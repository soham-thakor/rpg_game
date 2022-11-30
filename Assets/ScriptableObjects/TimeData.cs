using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TimeData : ScriptableObject, IResetOnExitPlay
{
    public float dayStart = 180;
    public float dayFinish = 560;

    public bool isNight;
    public float currMinutes = 720;
    public void ResetOnExitPlay()
    {
        currMinutes = 720;
    }
}

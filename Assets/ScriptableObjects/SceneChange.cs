using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SceneChange : ScriptableObject, IResetOnExitPlay
{
    public Vector2 initialValue;
    public bool movedScene = false;

    public void ResetOnExitPlay()
    {
        movedScene = false;
    }    
    
}

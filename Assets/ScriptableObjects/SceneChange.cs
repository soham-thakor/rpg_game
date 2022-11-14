using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SceneChange : ScriptableObject
{
    public Vector2 initialValue;
    public bool movedScene = false;

    public void OnBeforeSerialize() 
    {
        Debug.Log("onBeforeDeserialize");
        movedScene=false;
    }

    public void Awake() {
        Debug.Log("awake");
        movedScene = false;
    }

    private void OnEnable()
    {
        Debug.Log("on enable");
        movedScene = false;
    }

    private void OnDisable()
    {
        Debug.Log("on disable");
        movedScene = false;
    }

    // You can also use OnAfterDeserialize for the other way around
    public void OnAfterDeserialize() 
    {
        Debug.Log("onAfterDeserialize");
        movedScene=false;
    }
}

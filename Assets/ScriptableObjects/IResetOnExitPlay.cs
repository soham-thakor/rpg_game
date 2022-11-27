using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
 
static class SOPlayModeResetter
{
    [InitializeOnLoadMethod]
    static void RegisterResets()
    {
        EditorApplication.playModeStateChanged += ResetSOsWithIResetOnExitPlay; 
    }

    static void ResetSOsWithIResetOnExitPlay(PlayModeStateChange change)
    {
        if (change == PlayModeStateChange.ExitingPlayMode)
        {
            var assets = FindAssets<ScriptableObject>();
            foreach (var a in assets)
            {
                if (a is IResetOnExitPlay)
                {
                    (a as IResetOnExitPlay).ResetOnExitPlay();
                }
            }
        }
    }

    static T[] FindAssets<T>() where T : Object
    {
        var guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
        var assets = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            var path = AssetDatabase.GUIDToAssetPath(guids[i]);
            assets[i] = AssetDatabase.LoadAssetAtPath<T>(path);            
        }
        return assets;
    }
}
#endif

public interface IResetOnExitPlay
{
    public void ResetOnExitPlay();
}
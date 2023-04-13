using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    
    public KillReward[] rewards;

    public int GetPayout(GameObject enemy)
    {
        foreach(KillReward reward in rewards)
        {
            if(enemy.name.Contains(reward.prefab.name))
            {
                return reward.payout;
            }
        }

        Debug.LogWarning("GetPayout was called on an invalid prefab!");
        return 0;
    }

    [System.Serializable]
    public struct KillReward
    {
        public GameObject prefab;
        public int payout;

        public override string ToString() => $"( Prefab: {prefab.name}, Payout: {payout})";
    }
}

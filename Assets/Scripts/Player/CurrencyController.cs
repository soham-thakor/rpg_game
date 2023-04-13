using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    public TextMeshProUGUI counter;
    public KillReward[] rewards;

    public void UpdateCurrencyAmount(GameObject enemy)
    {
        staticVariables.currencyAmount += GetPayout(enemy);
        counter.text = staticVariables.currencyAmount.ToString();
    }

    private int GetPayout(GameObject enemy)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public float healAmount = 200f;
    public float lifetime = 1f;

    private PlayerController player;

    void OnEnable()
    {
        player = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        staticVariables.invincible = true;
        Invoke("DisableHeal", lifetime);
    }

    private void DisableHeal()
    {
        staticVariables.invincible = false;
        player.AddHealth(healAmount);
        Destroy(gameObject);
    }
}

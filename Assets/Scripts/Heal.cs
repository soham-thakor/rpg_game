using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public float healAmount = 200f;
    public float lifetime = 1f;

    private PlayerController player;
    private float beforeCastHealth;

    void OnEnable()
    {
        player = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        Invoke("DisableHeal", lifetime);
        beforeCastHealth = player.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        player.currentHealth = beforeCastHealth; // makes player invulnerable
    }

    private void DisableHeal()
    {
        player.AddHealth(healAmount);
        gameObject.SetActive(false);
    }
}

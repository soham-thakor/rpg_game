using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HealthBar healthBar;

    public float maxHealth = 100;
    public float currentHealth;

    //public float Health;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth((int)maxHealth);
    }
    
    // Gets called forom the attack sword.
    public void TakeDamage(float damage)
    {
        
          //print("taken damage");  
          if (currentHealth <= 0) {
                Destroy(gameObject);
            }
            else
           currentHealth -= damage;
           healthBar.SetHealth((int)currentHealth);
    }

}

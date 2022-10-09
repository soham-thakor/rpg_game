using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HealthBar healthBar;
    public SimpleFlash flasheffect;
    public float maxHealth = 100;
    public float currentHealth;
    public float speed = 2.5f;
    
    public Transform player;
    public bool isFlipped = false;

    

    //public float Health;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth((int)maxHealth);
        
    }

    // Gets called forom the attack sword.
    public bool LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }

        else if(transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }

        return isFlipped;
    }

    public void TakeDamage(float damage)
    {
        
          //print("taken damage");  
          if (currentHealth <= 0) {
                Destroy(gameObject);
            }
        
          flasheffect.Flash();
           currentHealth -= damage;
           healthBar.SetHealth((int)currentHealth);
    }

}

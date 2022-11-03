using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HealthBar healthBar;
    public float maxHealth = 100;
    public float currentHealth;
    public float speed = 2.5f;
    public SwordAttack swordHitbox;             // needs to be set to swordattack game object in editor
    public AudioSource damageReceived;
    public AudioSource deathSound;
    
    private SpriteRenderer spriteRenderer;
    private SimpleFlash flashEffect;
    private Transform player;
    private bool isFlipped = false;  // collider starts facing right (false is right, true is left)

    //public float Health;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth((int)maxHealth);

        flashEffect = GetComponent<SimpleFlash>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        // if player is left of enemy
        if(player.position.x < transform.position.x)
        {   
            if(swordHitbox){
                if(isFlipped != true) { swordHitbox.RotateCollider(); }
            }
            isFlipped = true;
            spriteRenderer.flipX = false;
        } 
        // if player is right of enemy
        else if (player.position.x > transform.position.x) 
        {
            if(swordHitbox) {
                if(isFlipped != false) { swordHitbox.RotateCollider(); }
            }
            isFlipped = false;
            spriteRenderer.flipX = true;
        }
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
            deathSound.Play();
            Destroy(gameObject);
        }

        damageReceived.Play();
        flashEffect.Flash();
        currentHealth -= damage;
        healthBar.SetHealth((int)currentHealth);
    }

}

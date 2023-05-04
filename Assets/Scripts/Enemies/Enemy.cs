using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public HealthBar healthBar;
    public float maxHealth = 100;
    public float currentHealth;
    public float speed = 2.5f;
    public SwordAttack swordHitbox;             // needs to be set to swordattack game object in editor
    public float Timer = 2;
    public bool isBoss = false;

    private SpriteRenderer spriteRenderer;
    private SimpleFlash flashEffect;
    private Transform player;
    private bool isFlipped = false;  // collider starts facing right (false is right, true is left)
    private Rigidbody2D rb;
    private string enemyType;
    private CurrencyController currencyController;

    void Start()
    {
        flashEffect = GetComponent<SimpleFlash>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currencyController = player.Find("UI Canvas/Arcana Counter").GetComponent<CurrencyController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if(isBoss) {
            SoundManager.PlaySound(SoundManager.Sound.CulpritFound);
        }

        currentHealth = maxHealth;
        healthBar.SetMaxHealth((int)maxHealth);

        if(gameObject.name.Contains("Antagonist") || gameObject.name.Contains("Gargoyle") || gameObject.name.Contains("Boss") )
        {
            enemyType = "Knight";
        } else if(gameObject.name.Contains("GOBLIN")) {
            enemyType = "Goblin";
        } else {
            enemyType = "Unknown";
        }
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
        flashEffect.Flash();
        currentHealth -= damage;
        healthBar.SetHealth((int)currentHealth);

        if(currentHealth <= maxHealth/2 && isBoss)
        {
            GetComponent<Animator>().SetBool("IsEnraged", true);
        }

        if(currentHealth <= 0)
        {
            PlayDeathSFX();
            currencyController.UpdateCurrencyAmount(gameObject);

            if(isBoss) {
                SceneManager.LoadScene("CutSceneEnding");
            }

            Destroy(gameObject);
        }
        else
        {
            PlayDamagedSFX();
        }
    }

    private void PlayDeathSFX()
    {
        if(enemyType == "Knight") 
        {
            SoundManager.PlaySound(SoundManager.Sound.KnightDeath);
        } 
        else if(enemyType == "Goblin") 
        {
            SoundManager.PlaySound(SoundManager.Sound.GoblinDeath);
        } 
        else 
        {
            SoundManager.PlaySound(SoundManager.Sound.KnightDeath);
        }
    }

    private void PlayDamagedSFX()
    {
        if (enemyType == "Knight") 
        {
            SoundManager.PlaySound(SoundManager.Sound.KnightDamaged);
        } 
        else if (enemyType == "Goblin") 
        {
            SoundManager.PlaySound(SoundManager.Sound.GoblinDamaged);
        } else 
        {
           SoundManager.PlaySound(SoundManager.Sound.KnightDamaged);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

// Takes and handles input and movement for a player character
public class PlayerController : MonoBehaviour
{
    public SwordAttack swordHitbox;             // needs to be set to swordattack game object in editor

    // movement and collision variables
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    // health related variables
    public HealthBar healthBar;
    public float currentHealth = 100f;

    // public ability variables
    public GameObject projectile;
    public GameObject mine;
    public GameObject wind;
    
    // scriptable object
    public PlayerData playerData;

    // private variables
    private bool canMove = true;
    private PlayerInput playerInput; 
    private SimpleFlash flashEffect;
    private Vector2 movementInput;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // for ability cooldowns (cooldown times are set in playerdata)
    int[] abilityReady = { 1, 1, 1};  // 1 represents that ability is ready

    
    // pull data from scriptable object
    void Awake() 
    {
        currentHealth = playerData.currentHealth;
        moveSpeed = playerData.moveSpeed;        
    }

    // Start is called before the first frame update
    void Start()
    {
        // set health values from previous scene
        healthBar.SetMaxHealth((int)playerData.maxHealth);
        healthBar.SetHealth((int)currentHealth);

        // get references
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        flashEffect = GetComponent<SimpleFlash>();
        
        // move player in scene if necessary
        if(playerData.movedScene){
            transform.position = playerData.initialValue;
        }
    }

    private void FixedUpdate() 
    {
        if(canMove) 
        {
            // If movement input is not 0, try to move            
            if(movementInput == Vector2.zero){
                animator.SetBool("isMoving", false);
                return;
            }
                
            bool success = TryMove(movementInput);

            if(!success) {
                success = TryMove(new Vector2(movementInput.x, 0));
            }

            if(!success) {
                success = TryMove(new Vector2(0, movementInput.y));
            }
            
            animator.SetBool("isMoving", success);
            animator.SetFloat("yDirection", movementInput.y);
            animator.SetFloat("xDirection", Math.Abs(movementInput.x));
            // Set direction of sprite to movement direction
            if(movementInput.x < 0) {
                if (spriteRenderer.flipX != false){ swordHitbox.RotateCollider(); }
                spriteRenderer.flipX = false;
            } 
            else if (movementInput.x > 0) {
                if (spriteRenderer.flipX == false) { swordHitbox.RotateCollider(); }
                spriteRenderer.flipX = true;
            }
        }
    }

    private bool TryMove(Vector2 direction) {
        if(direction == Vector2.zero) {
            return false;
        }

        // Check for potential collisions
        int count = rb.Cast(
            direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
            movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
            castCollisions, // List of collisions to store the found collisions into after the Cast is finished
            moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

        if(count == 0){
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        } else {
            return false;
        }
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
        SoundManager.PlaySound(SoundManager.Sound.PlayerFootstep);
    }

    // called on left click
    void OnSlash() {
        animator.SetTrigger("swordAttack");
        SoundManager.PlaySound(SoundManager.Sound.SwordSlash);
    }

    // called on pressing keyboard button 1
    void OnBite() {
        
        if(abilityReady[0] == 1) {
            Debug.Log("bite triggered");
            // Create new bullet
            GameObject newBullet = Instantiate(projectile, transform.position, transform.rotation);
            Bullet bulletScript = newBullet.GetComponent<Bullet>();

            bulletScript.setFlipX(spriteRenderer.flipX);
            bulletScript.setBulletClone(true);    // indicates that this bullet must be deleted
            bulletScript.setOrigin("Player");   // where bullet came from
            newBullet.SetActive(true);  // activate game object
            SoundManager.PlaySound(SoundManager.Sound.FireBite);
            
            // start cooldown
            StartCoroutine(StartCooldown(0, playerData.biteCoolDown));
        }
    }

    // called on pressing keyboard button 2
    void OnDropMine() {
        if(abilityReady[1] == 1) {
            // Create new water mine
            GameObject newMine = Instantiate(mine, transform.position, transform.rotation);
            Mine mineScript = newMine.GetComponent<Mine>();

            mineScript.setOrigin("Player");
            newMine.SetActive(true);
            SoundManager.PlaySound(SoundManager.Sound.PlaceWaterBomb);
            StartCoroutine(StartCooldown(1, playerData.mineCoolDown));
        }
    }

    // called on pressing keyboard button 3
    void OnWind() {
        if(abilityReady[2] == 1) {
            WindSpeed windScript = wind.GetComponent<WindSpeed>();

            wind.SetActive(true);
            StartCoroutine(StartCooldown(2, playerData.speedCoolDown));
            //SoundManager.PlaySound(SoundManager.Sound.SpeedUpBoost);
        }
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }

        SoundManager.PlaySound(SoundManager.Sound.PlayerDamaged);
        flashEffect.Flash();
        currentHealth -= damage;
        healthBar.SetHealth((int)currentHealth);
    }

    // saves player data to scriptable object
    public void SavePlayerData() 
    {
        playerData.currentHealth = currentHealth;
    }

    private IEnumerator StartCooldown(int slot, float delayTime) 
    {
        abilityReady[slot] = 0; // disable ability
        yield return new WaitForSeconds(delayTime);
        abilityReady[slot] = 1; // enable ability
    }
}
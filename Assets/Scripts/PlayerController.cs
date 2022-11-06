using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    //public AudioSource footstepsound;
    public AudioSource swordslash;
    public AudioSource damagetaken1;

    // public ability variables
    public GameObject projectile;
    

    // private variables
    private bool canMove = true;
    private PlayerInput playerInput; 
    private SimpleFlash flashEffect;
    private Vector2 movementInput;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        flashEffect = GetComponent<SimpleFlash>();
        healthBar.SetMaxHealth((int)currentHealth);
        
    }

    private void FixedUpdate() {
        if(canMove) {
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
        // footstepsound.Play();
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
    }

    // called on left click
    void OnSlash() {
        animator.SetTrigger("swordAttack");
        swordslash.Play();
    }

    // called on pressing keyboard button 1
    void OnBite() {
        // Create new bullet
        GameObject newBullet = Instantiate(projectile, transform.position, transform.rotation);
        Bullet bulletScript = newBullet.GetComponent<Bullet>();

        bulletScript.setFlipX(spriteRenderer.flipX);
        bulletScript.setBulletClone(true);    // indicates that this bullet must be deleted
        bulletScript.setOrigin("Player");   // where bullet came from
        newBullet.SetActive(true);  // activate game object
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }

        damagetaken1.Play();
        flashEffect.Flash();
        currentHealth -= damage;
        healthBar.SetHealth((int)currentHealth);
    }
}

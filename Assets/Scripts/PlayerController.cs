using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Takes and handles input and movement for a player character
public class PlayerController : MonoBehaviour
{
    public SwordAttack swordHitbox;             // needs to be set to swordattack game object in editor
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public AudioSource footstepsound; 

    private bool isFacingRight;
    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    

    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
                spriteRenderer.flipX = true;
                isFacingRight = false;
            } else if (movementInput.x > 0) {
                spriteRenderer.flipX = false;
                isFacingRight = true;
            }

        }
    }

    private bool TryMove(Vector2 direction) {
        if(direction == Vector2.zero) {
            return false;
        }
        footstepsound.Play();
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
    void OnFire() {
        animator.SetTrigger("swordAttack");   
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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

    // equippable abilities
    public GameObject projectileSlot;
    public GameObject[] abilities = {};

    // used for detecting when a number key is pressed
    private KeyCode[] keyCodes = new KeyCode []{ KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5,
                                                 KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };
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

    //To keep track of the ghosts that need to spawn
    public QuestTrackerData questData; //isn't used, but it acts as a global variable

    // pull data from scriptable object
    void Awake() 
    {
        currentHealth = playerData.currentHealth;
        moveSpeed = playerData.moveSpeed;        
    }

    // Start is called before the first frame update
    void Start()
    {
        staticVariables.immobile = false;

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
        if(playerData.movedScene)
        {
            transform.position = playerData.initialValue;
            playerData.movedScene = false;
        }
    }

    private void Update()
    {
        var index = CheckForNumericKeyPress(1, abilities.Length);
        if(index >= 0) { UseAbility(index); }
    }

    private void FixedUpdate() 
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            Debug.Log("Culprit is: " + staticVariables.realVillain);
        }

        if (canMove && !staticVariables.immobile) 
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

    private void UseAbility(int index)
    {
        Debug.Log("Using ability " + index);
        AbilitySlot abilitySlot = abilities[index].GetComponent<AbilitySlot>();
        if(staticVariables.getCooldown(abilitySlot.abilityName) != 1f) { return; }
        abilitySlot.Activate(gameObject.transform.position);
    }

    private int CheckForNumericKeyPress(int minimum, int maximum)
    {
        for(int i = minimum; i <= maximum; i++)
        {
            if(Input.GetKeyDown(keyCodes[i])) { 
                return i-1; 
            }
        }
        return -1;
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
			moveSpeed * Time.fixedDeltaTime); // The amount to cast equal to the movement

		if (count == 0){
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        return false;
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnSlash() {
        string animState = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if(animState != "player_attack" && animState != "player_attack_up" && animState != "player_attack_down")
		{
            animator.SetTrigger("swordAttack");
        }
    }

    void OnShoot()
    {
        AbilitySlot abilitySlot = projectileSlot.GetComponent<AbilitySlot>();
        if(staticVariables.getCooldown(abilitySlot.abilityName) != 1f) { return; }
        abilitySlot.Activate(gameObject.transform.position);
    }

    public void TakeDamage(float damage)
    {
        if(staticVariables.invincible)
		{
            return;
		}
        if (currentHealth <= 0) {
            SceneManager.LoadScene("CutSceneGameOver");
        }

        SoundManager.PlaySound(SoundManager.Sound.PlayerDamaged);
        flashEffect.Flash();
        currentHealth -= damage;
        healthBar.SetHealth((int)currentHealth);
    }

    public void AddHealth(float heal)
    {
        currentHealth += heal;
        healthBar.SetHealth((int)currentHealth);
    }

    // saves player data to scriptable object
    public void SavePlayerData()
    {
        playerData.currentHealth = currentHealth;
    }

    public bool IsClosestNPC(GameObject tryingNPC)
	{
        float minDist = float.MaxValue; 
        GameObject closestNPC = tryingNPC;
        foreach(GameObject NPC in GameObject.FindGameObjectsWithTag("NPC"))
		{
            if(Vector2.Distance(NPC.transform.position, gameObject.transform.position) < minDist){
                minDist = Vector2.Distance(NPC.transform.position, gameObject.transform.position);
                closestNPC = NPC;
            }
		}
        return closestNPC == tryingNPC;
	}

    public void playSwordSound()
	{
        SoundManager.PlaySound(SoundManager.Sound.SwordSlash);
    }
}
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
    public Ability[] abilities = {};

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
    private CurrencyController currencyController;

    //To keep track of the ghosts that need to spawn
    public QuestTrackerData questData; //isn't used, but it acts as a global variable

    // pull data from scriptable object
    void Awake() 
    {
        FetchControls();
        FetchAbilities();
        currentHealth = playerData.currentHealth;
        moveSpeed = playerData.moveSpeed;        
    }

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
        currencyController = GetComponent<CurrencyController>();
        
        // move player in scene if necessary
        if(playerData.movedScene)
        {
            transform.position = playerData.initialValue;
            playerData.movedScene = false;
        }
    }

    private void Update()
    {
        var index = CheckForValidKeyPress();
        if(index >= 0) { UseAbility(index); }
    }

    private void getAllCluesDebug()
	{
        string debugString = "CLUES DEBUG STATEMENT: \n";
        debugString += "CULPRIT CLUES: \n";
        debugString += "Culprit is: " + staticVariables.realVillain + "\n";
        debugString += "Culprit Traits: \n" +
            "Key: " + NPCStatic.culpritKey.ToString() + "\n" +
            "Traits: \n" + NPCStatic.NPCnames[NPCStatic.culpritKey].trait1 + "\n"
            + NPCStatic.NPCnames[NPCStatic.culpritKey].trait2 + "\n"
            + NPCStatic.NPCnames[NPCStatic.culpritKey].trait3 + "\n";
        debugString += "Ghost 'CULPRIT IS' Clues: \n";
        debugString += NPCStatic.ghostClue1.clue + "\n"
            + NPCStatic.ghostClue2.clue + "\n"
            + NPCStatic.ghostClue3.clue + "\n";
        debugString += "Ghost 'CULPRIT ISN'T Clues: \n";
        debugString += NPCStatic.antiClue1.clue + "\n"
            + NPCStatic.antiClue2.clue + "\n"
            + NPCStatic.antiClue3.clue + "\n";
        debugString += "Anti-Clues List: \n";
        foreach (string trait in NPCStatic.antiCluesGiven)
        {
            debugString += trait + "\n";
        }

        Debug.Log(debugString);
        Debug.Log(Time.time);
    }

    private void FixedUpdate() 
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            getAllCluesDebug();
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
        if (PauseManager.isPaused) { return; }
        AbilitySlot abilitySlot = abilities[index].abilitySlot.GetComponent<AbilitySlot>();

        if(abilitySlot == null) { return; }
        if(staticVariables.getCooldown(abilitySlot.abilityName) != 1f) { return; }
        abilitySlot.Activate(gameObject.transform.position);
    }

    private int CheckForValidKeyPress()
    {   
        int index = 0;
        foreach(Ability ability in abilities)
        {
            if(Input.GetKeyDown(ability.bindedKey))
            {
                return index;
            }
            index += 1;
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
        if (PauseManager.isPaused) { return; }
        string animState = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if(animState != "player_attack" && animState != "player_attack_up" && animState != "player_attack_down")
		{
            animator.SetTrigger("swordAttack");
        }
    }

    void OnShoot()
    {
        if (PauseManager.isPaused) { return; }
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

    public void FetchAbilities()
    {
        foreach(Ability ability in abilities)
        {
            if(staticVariables.abilityActiveStatus.TryGetValue(ability.abilitySlot.name, out bool _) || ability.isStartingAbility)
            {
                Debug.Log("Ability " + ability.abilitySlot.name + " found in dictionary");
                ability.abilitySlot.SetActive(true);
            }
            else
            {
                Debug.Log("Ability " + ability.abilitySlot.name + " not found in dictionary");
                ability.abilitySlot.SetActive(false);
            }
        }
    }

    public void FetchControls()
    {
        foreach(Ability ability in abilities)
        {
            if(staticVariables.abilityBindings.TryGetValue(ability.abilitySlot.name, out KeyCode bindedKey))
            {
                ability.bindedKey = bindedKey;
            }
            else
            {
                staticVariables.abilityBindings[ability.abilitySlot.name] = ability.bindedKey;
            }
        }
    }

    public void playSwordSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.SwordSlash);
    }

    [System.Serializable]
    public class Ability
    {
        public bool isStartingAbility;
        public GameObject abilitySlot;
        public KeyCode bindedKey;
    }
}
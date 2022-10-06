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


    Transform player;
    Rigidbody2D rb;

    //public float Health;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth((int)maxHealth);
        
    }
/*
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = Gameobject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.GetComponent<>
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }
  */
    // Gets called forom the attack sword.
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

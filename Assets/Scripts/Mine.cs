using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public float lifeTime;
    public float damageDealt;

    // only used for player made projectiles
    private string origin = "";
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // setters
    public void setOrigin(string s) {origin = s; }
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        // if player mine comes in contact with enemy
        if(other.tag == "Enemy" && origin == "Player") {
            Enemy enemy = other.GetComponent<Enemy>();

            // if current mine is water mine
            if(gameObject.name == "Water Mine(Clone)") 
            {
                enemy.TakeDamage(damageDealt);
                animator.SetTrigger("Explode");
                Destroy(gameObject, .8f);
            }
        }

        // enemy mine comes in contact with player
        if(other.tag == "Player" && origin == "Enemy") {
            // TODO: add enemy mines
            Destroy(gameObject);
        }
    }

}

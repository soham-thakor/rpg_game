using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float lifeTime;
    public float damageDealt;

    private bool bulletClone = false;

    // only used for player made projectiles
    private bool flipX = false;
    private string origin = "";
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // setters
    public void setBulletClone(bool b) { bulletClone = b;}
    public void setFlipX(bool b) { flipX = b; }
    public void setOrigin(string s) {origin = s; }
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(bulletClone) {
            Invoke("DestroyProjectile",lifeTime);
        } 
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        // if player shoots projectile
        if(origin == "Player") {
            
            if(flipX) {
                spriteRenderer.flipX = false;
                transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
            }
            else {
                spriteRenderer.flipX = true;
                transform.Translate(Vector2.left * bulletSpeed * Time.deltaTime);
            }
        }

        if(origin == "Enemy") {
            transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
        }
    }

    void DestroyProjectile(){
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // if player shoots enemy
        if(other.tag == "Enemy") {
            // if current bullet is a fire bite
            if(gameObject.name == "Bite") {
                Debug.Log("Bite Collided");
                animator.SetTrigger("CloseMouth");
            }
        }

        // if enemy bullet hits player
        if(other.tag == "Player" && origin == "Enemy") {
            PlayerController player = other.GetComponent<PlayerController>();

            if(player != null) {
                player.TakeDamage(damageDealt);
            }
            Destroy(gameObject);
        }
    }
}

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
    private bool isMoving = true;

    // setters
    public void setBulletClone(bool b) { bulletClone = b;}
    public void setFlipX(bool b) { flipX = b; }
    public void setOrigin(string s) {origin = s; }
    
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // checks if current object is a clone
        if(gameObject.name.Contains("Clone")) {
            Invoke("DestroyProjectile",lifeTime);   // deletes self whenever lifetime is reached
        } 
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        // if player shoots projectile
        if(origin == "Player" && isMoving) {
            
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

            // destroy object when it hits wall
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, 1, 8);
            if(hitInfo) {
                if(hitInfo.collider.tag == "Obstacle") {
                    Destroy(gameObject);
                }
            }
        }
    }

    void DestroyProjectile(){

        if(gameObject.name == "Bite(Clone)") {
            animator.SetTrigger("CloseMouth");
            Destroy(gameObject, .5f);
        }
        else {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // if player shoots enemy
        if(other.tag == "Enemy") {
            Enemy enemy = other.GetComponent<Enemy>();

            // if current bullet is a fire bite
            if(gameObject.name == "Bite(Clone)") {
                enemy.TakeDamage(damageDealt);
                isMoving = false;
                animator.SetTrigger("CloseMouth");
                Destroy(gameObject, .8f);
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

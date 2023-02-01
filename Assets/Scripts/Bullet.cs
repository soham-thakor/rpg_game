using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float lifeTime;
    public float damageDealt;

    private string origin = "";
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    // for mouse targetting
    private Vector3 mousePos;
    private Camera mainCam;

    // setters
    public void setOrigin(string s) {origin = s; }
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //damageDealt =1;
        // checks if current object is a clone
        if(gameObject.name.Contains("Clone")) {
            Invoke("DestroyProjectile",lifeTime);   // deletes self whenever lifetime is reached
        } 


        // if player is firing the bullet, run this
        if(origin == "Player")
        {
            // set position for bullet to travel to
            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

            // set speed and direction
            Vector3 direction = mousePos - transform.position;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;

            // handle rotation
            Vector3 rotation = transform.position - mousePos;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot + 180);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        // if enemy is firing bullet, run this
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
        if(other.tag == "Enemy" && origin == "Player") {

            Enemy enemy = other.GetComponent<Enemy>();
           
            // if current bullet is a fire bite
            if(gameObject.name == "Bite(Clone)") {
                enemy.TakeDamage(damageDealt);
                animator.SetTrigger("CloseMouth");
                rb.velocity = Vector2.zero; // stop bullet from moving
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

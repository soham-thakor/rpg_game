using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float lifeTime;
    public float damageDealt;
    public GameObject explosion;
    public GameObject bulletLight;

    private string origin = "";
    private Animator animator;
    private Renderer bulletRenderer;
    private Rigidbody2D rb;
    private Collider2D bulletCollider;

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
        bulletRenderer = GetComponent<SpriteRenderer>().GetComponent<Renderer>();
        bulletCollider = GetComponent<Collider2D>();

        if(origin == "Player") { SoundManager.PlaySound(SoundManager.Sound.FireBite); }
        
        // checks if current object is a clone
        if(gameObject.name.Contains("Clone")) {
            Invoke("DestroyProjectile", lifeTime);   // deletes self whenever lifetime is reached
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

    void FixedUpdate()
    {   
        if(origin == "Enemy") {
            transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
        }
    }

    void DestroyProjectile(){

        if(gameObject.name == "Bite(Clone)") 
        {
            animator.SetTrigger("CloseMouth");
            Destroy(gameObject, .5f);
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Explode(string tag)
    {
        explosion.SetActive(true);
        rb.velocity = Vector2.zero;

        if(tag == "Obstacle" || tag == "Player")
        {
            bulletLight.SetActive(false);
            bulletCollider.isTrigger = false;
            bulletRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.tag == "Obstacle") 
        {
            Explode(other.tag);    
        }

        // if player shoots enemy
        if(other.tag == "Enemy" && origin == "Player") {

            Enemy enemy = other.GetComponent<Enemy>();
           
            // if current bullet is a fire bite
            if(gameObject.name == "Bite(Clone)") 
            {    
                Explode(other.tag);
                animator.SetTrigger("CloseMouth");
                enemy.TakeDamage(damageDealt);
                
                Destroy(gameObject, .8f);
            }
        }

        // if enemy bullet hits player
        if(other.tag == "Player" && origin == "Enemy") 
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if(player != null) 
            {
                Explode(other.tag);
                player.TakeDamage(damageDealt);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float lifeTime;
    public float damageDealt;
    private Animator animator;
    // Do not change this in the inspector
    public bool bulletClone = false;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        if(bulletClone){
            animator.SetTrigger("OpenMouth");
            Invoke("DestroyProjectile",lifeTime);
        } 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }

    void DestroyProjectile(){
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            PlayerController player = other.GetComponent<PlayerController>();

            // if current bullet is a fire bite
            if(gameObject.name == "Bite") {
                animator.SetTrigger("CloseMouth");
            }

            if(player != null) {
                player.TakeDamage(damageDealt);
            }
            Destroy(gameObject);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damageDealt;
    public Collider2D swordCollider;

    private Animator animator;
    private Vector3 faceRight;
    private Vector3 faceLeft;

    private void Start() {
        if (this.transform.parent.tag == "Enemy") {
            swordCollider.enabled = true;
            animator = gameObject.GetComponentInParent(typeof(Animator)) as Animator;
        }
        else {
            swordCollider.enabled = false;
        }
        
        faceRight = transform.position;
        faceLeft = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
    }

    public void RotateCollider() {
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // only let player attack enemies (prevent enemy friendly fire)
        if(other.tag == "Enemy" && gameObject.transform.root.tag == "Player") {
            Enemy enemy = other.GetComponent<Enemy>();
            
            if(enemy != null) {  
                enemy.TakeDamage(damageDealt);               
            }
        }

        if(other.tag == "Player") {
            PlayerController player = other.GetComponent<PlayerController>();

            if(player != null) {
                animator.SetTrigger("attack");
                player.TakeDamage(damageDealt);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damageDealt;
    public Collider2D swordCollider;

    Vector3 faceRight;
    Vector3 faceLeft;

    private void Start() {
        swordCollider.enabled = false;
        faceRight = transform.position;
        faceLeft = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
    }

    public void RotateCollider() {
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // other collider MUST be set to Enemy
        if(other.tag == "Enemy") {
            Enemy enemy = other.GetComponent<Enemy>();
            
            if(enemy != null) {  
                enemy.TakeDamage(damageDealt);               
            }
        }
    }
}

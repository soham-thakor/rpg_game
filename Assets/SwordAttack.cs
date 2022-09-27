using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damageDealt;
    public Collider2D swordCollider;
    
    Vector2 colliderPosition;

    private void Start() {
        swordCollider.enabled = false;
        colliderPosition = transform.position;
    }

    public void Attack(string direction) {
        swordCollider.enabled = true;

        // check to see which direction attack is in
        if (direction == "left") {
            transform.localPosition = new Vector2(colliderPosition.x * -1, colliderPosition.y);
        }
        else {
            transform.localPosition = colliderPosition;
        }
    }

    public void EndAttack() {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        print("Collided");

        // other collider MUST be set to Enemy
        if(other.tag == "Enemy") {
            Enemy enemy = other.GetComponent<Enemy>();
            
            if(enemy != null) {
                print("Enemy injured!");
                enemy.Health -= damageDealt;
            }
        }
    }
}

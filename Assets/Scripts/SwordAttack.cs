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
        faceLeft = new Vector3(faceRight.x * -1, faceRight.y, faceRight.z);
    }

    public void ChangeColliderDirection(bool isFacingRight) {
        if(isFacingRight) {
            gameObject.transform.position = faceRight;
        }
        else {
            gameObject.transform.position = faceLeft;
        }
    }
    // public void Attack(string direction) {
    //     swordCollider.enabled = true;
    //     print("collider enabled");

    //     // check to see which direction attack is in
    //     if (direction == "left") {
    //         transform.localPosition = new Vector2(colliderPosition.x * -1, colliderPosition.y);
    //     }
    //     else {
    //         transform.localPosition = colliderPosition;
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        print("Collided");

        // other collider MUST be set to Enemy
        if(other.tag == "Enemy") {
            Enemy enemy = other.GetComponent<Enemy>();
            
            if(enemy != null) {  
                print("Enemy injured!");
                enemy.TakeDamage(damageDealt);               
            }
        }
    }
}

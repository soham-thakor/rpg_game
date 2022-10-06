using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health {
        set {                   // setter for health
            health = value;
            
            if (health <= 0) {
                Destroy(gameObject);
            }
        }
        get {                  // getter for health
            return health;
        }
    }

    private float health = 100;
}

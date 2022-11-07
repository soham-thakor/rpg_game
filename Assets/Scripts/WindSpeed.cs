using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpeed : MonoBehaviour
{
    public float lifeTime;
    public float grantedSpeed;

    // private vars
    private string origin = "";
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float origSpeed;

    // setters
    public void setOrigin(string s) {origin = s; }
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // checks if this object is a duplicate
        // if(gameObject.name.Contains("Clone")) {
        //     Invoke("DestroySelf");
        // }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "Player") {
            PlayerController player = other.GetComponent<PlayerController>();

            origSpeed = player.moveSpeed;
            player.moveSpeed = player.moveSpeed + grantedSpeed;
        }
    }
}

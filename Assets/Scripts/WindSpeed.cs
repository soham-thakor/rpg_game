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
    private PlayerController player;

    // setters
    public void setOrigin(string s) {origin = s; }
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerController>();

        // modify speed
        origSpeed = player.moveSpeed;
        player.moveSpeed = player.moveSpeed + grantedSpeed;

        Invoke("DisableWind", lifeTime);
    }

    private void DisableWind() {
        if(player) {
            // set player speed back to original speed
            player.moveSpeed = origSpeed;
        }
        gameObject.SetActive(false);
    }
}

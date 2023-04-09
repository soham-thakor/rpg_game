using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHammer : MonoBehaviour
{
    public float damageDealt;
    public float yPlayerOffset;     // how far above the hammer is from the player

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer playerSpriteRenderer;
    private GameObject player;
    private bool isExploding = false;
    

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerSpriteRenderer = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        SetDirection();
        if (!isExploding)
        {
            SetPosition();
        }
    }

    private void SetPosition()
    {
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y + yPlayerOffset);
    }

    private void SetDirection()
    {
        if (playerSpriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    public void PrepareExplosion()
    {
        isExploding = true;

        transform.localScale = new Vector2(2f, 2f);

        if(spriteRenderer.flipX)
        {
            transform.position = new Vector2(transform.position.x - .3f, transform.position.y);
            Debug.Log("Moving rock hammer left");
        }
        else
        {
            transform.position = new Vector2(transform.position.x + .3f, transform.position.y);
            Debug.Log("Moving rock hammer right");
        }
    }

    public void DestroyRock()
    {
        Destroy(gameObject);
    }
}

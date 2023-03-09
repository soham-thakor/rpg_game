using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public float lifeTime;
    public float damageDealt;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private List<Enemy> enemies = new List<Enemy>();
    public BoxCollider2D damageCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // checks if current object is a clone
        if(gameObject.name.Contains("Clone")) {
            Invoke("DestroyMine",lifeTime);   // deletes self whenever lifetime is reached
        } 
    }

    void DestroyMine() {
        SoundManager.PlaySound(SoundManager.Sound.WaterBombExplode);
        animator.SetTrigger("Explode");
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        // if player mine comes in contact with enemy
        if(other.tag == "Enemy") 
        {
            SoundManager.PlaySound(SoundManager.Sound.WaterBombExplode);
            animator.SetTrigger("Explode");
            Destroy(gameObject, 1f);
        }
    }

    // called via animation events
    public void enableDamageCollider() { damageCollider.enabled = true; }
    public void disableDamageCollider() { damageCollider.enabled = false; }
}

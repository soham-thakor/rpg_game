using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            Enemy enemy = other.GetComponent<Enemy>();
            if(!enemies.Contains(other.gameObject.GetComponent<Enemy>())) {
                enemies.Add(other.gameObject.GetComponent<Enemy>());
			}
            animator.SetTrigger("Explode");
            Destroy(gameObject, 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (enemies.Contains(collision.gameObject.GetComponent<Enemy>()))
            {
                enemies.Remove(collision.gameObject.GetComponent<Enemy>());
            }
        }
    }

	public void dealDamage()
	{
        SoundManager.PlaySound(SoundManager.Sound.WaterBombExplode);
        foreach(Enemy enemy in enemies.ToList())
		{
            enemy.TakeDamage(damageDealt);
		}
	}
}

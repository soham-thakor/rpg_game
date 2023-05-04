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
    
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        Invoke("LifetimeReached", lifeTime);  // play explosion animation after lifetime
    }

    private void LifetimeReached() 
    {
        if(gameObject.name.Contains("Water")){
            SoundManager.PlaySound(SoundManager.Sound.WaterBombExplode);
        }
        animator.SetTrigger("Explode");
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

    // below methods are referenced in animation events
	public void DealDamage()
	{
        SoundManager.PlaySound(SoundManager.Sound.WaterBombExplode);
        foreach(Enemy enemy in enemies.ToList())
		{
            enemy.TakeDamage(damageDealt);
		}
	}

    public void DestroyMine()
    {
        // dont destroy if enemies are petrified
        if(TryGetComponent(out Petrify petrify))
        {
            if(petrify.petrified){
                animator.enabled = false;
                StartCoroutine(WaitForDestroy(petrify.duration));
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator WaitForDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using UnityEngine;

public class Petrify : MonoBehaviour
{
    public float duration = 3f;
    public Collider2D radius;
    public bool petrified = false;

    private List<GameObject> enemies = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other) 
    {
        // keep track of what enemies are touching the mine
        if(other.tag == "Enemy") 
        {
            if(!enemies.Contains(other.gameObject)) {
                enemies.Add(other.gameObject);
			}
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Enemy" && !petrified)
        {
            if (enemies.Contains(collision.gameObject))
            {
                enemies.Remove(collision.gameObject);
            }
        }
    }

    public void PetrifyEnemies()
    {
        petrified = true;
        StartCoroutine(PetrifyRoutine());
    }

    private IEnumerator PetrifyRoutine()
    {
        // petrify enemies
        foreach(GameObject enemy in enemies.ToList())
        {
            TogglePetrification(false, enemy);
        }

        yield return new WaitForSeconds(duration);

        // unpetrify enemies
        foreach(GameObject enemy in enemies.ToList())
        {
            TogglePetrification(true, enemy);
        }
        petrified = false;
    }

    private void TogglePetrification(bool status, GameObject enemy)
    {
        if (enemy.TryGetComponent<RangedEnemyController>(out RangedEnemyController rangedEnemy))
        {
            Debug.Log("set ranged enemy controller to inactive");
            rangedEnemy.enabled = status;
        }

        Transform hitbox = enemy.transform.Find("EnemySwordHitbox");
        if ( hitbox ) {hitbox.gameObject.SetActive(status);}

        enemy.transform.Find("LineOfSight").GetComponent<LineOfSight>().enabled = status;
        enemy.GetComponent<Animator>().enabled = status;
    }
}

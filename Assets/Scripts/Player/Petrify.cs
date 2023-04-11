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
            enemy.transform.Find("LineOfSight").GetComponent<LineOfSight>().enabled = false;
            enemy.transform.Find("EnemySwordHitbox").gameObject.SetActive(false);
            enemy.GetComponent<Animator>().enabled = false;
            enemy.GetComponent<IAstarAI>().destination = enemy.transform.position;
        }

        yield return new WaitForSeconds(duration);

        // unpetrify enemies
        foreach(GameObject enemy in enemies.ToList())
        {
            enemy.transform.Find("LineOfSight").GetComponent<LineOfSight>().enabled = true;
            enemy.transform.Find("EnemySwordHitbox").gameObject.SetActive(true);
            enemy.GetComponent<Animator>().enabled = true;
        }
        petrified = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyController : MonoBehaviour
{
    private Transform player;
    public Transform shotPoint;
    public Transform gun;

    public GameObject EnemyProjectile;
    public float followPlayerRange;
    public bool inRange;
    public float attackRange;

    public float startTimeBtwnShots;
    public float timeBtwnShots;
    // Start is called before the first frame update
    void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 differance = player.position - gun.transform.position;
        float rotZ = Mathf.Atan2(differance.y,differance.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0f,0f,rotZ);

        if((Vector2.Distance(transform.position,player.position) <= followPlayerRange) && (Vector2.Distance(transform.position,player.position) > attackRange)){
            inRange = true;
        }else{
            inRange = false;
        }

         if(Vector2.Distance(transform.position,player.position) <= (attackRange)){
            if(timeBtwnShots <= 0 ){
                // Create new bullet aimed at player
                GameObject newBullet = Instantiate(EnemyProjectile, shotPoint.position, shotPoint.transform.rotation);
                newBullet.GetComponent<Bullet>().bulletClone = true;    // indicates that this bullet must be deleted
                timeBtwnShots = startTimeBtwnShots;
            }else{
                timeBtwnShots -= Time.deltaTime;
            }
         }
    }

    void OnDrawGizmos(){
        Gizmos.DrawWireSphere(transform.position,followPlayerRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRange);  
    }

}

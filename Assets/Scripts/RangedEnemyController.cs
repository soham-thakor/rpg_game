using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyController : MonoBehaviour
{
    private Transform player;
    private bool inRange = false;

    public Transform shotPoint;
    public Transform gun;

    public GameObject EnemyProjectile;
    public float attackRange;

    public float startTimeBtwnShots;
    public float timeBtwnShots;

    // setter
    public void setRange(bool t) { inRange = t;}

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

         if(inRange) {
            if(timeBtwnShots <= 0 ){
                // Create new bullet aimed at player
                GameObject newBullet = Instantiate(EnemyProjectile, shotPoint.position, shotPoint.transform.rotation);
                Bullet bulletScript = newBullet.GetComponent<Bullet>();

                bulletScript.setOrigin("Enemy");
                newBullet.SetActive(true);

                timeBtwnShots = startTimeBtwnShots;
            } 
            else {
                timeBtwnShots -= Time.deltaTime;
            }
        }
    }
}

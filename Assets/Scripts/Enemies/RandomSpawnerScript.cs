using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnerScript : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public float Timer = 15;

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if(Timer <=0f)
        {
            for(int i = 0;i<5;i++)
            {
                int randEnemy = Random.Range(0, enemyPrefabs.Length);
                int randSpawnPoint = Random.Range(0, spawnPoints.Length);
                
                Instantiate(enemyPrefabs[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
            }
            Timer = 15f;
        }
    }
}

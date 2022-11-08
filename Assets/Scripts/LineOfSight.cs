using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding; // this is required to use IAstarAI class

// DOCUMENTATION
// https://docs.unity3d.com/Manual/layers-and-layermasks.html
// https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
// https://www.youtube.com/watch?v=dmQyfWxUNPw

public class LineOfSight : MonoBehaviour
{

    public float rotationSpeed;
    public float visionDistance;
    public bool canMove;
    public GameObject enemy;

    private Transform player;
    private IAstarAI enemyChase;


    void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyChase = enemy.GetComponent<IAstarAI>();
    }
    
    // Update is called once per frame
    void Update()
    {
        // rotate looking for player
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        // will only hit objects on the obstacle layer
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, visionDistance, 8);
        
        if(hitInfo.collider != null) 
        {
            
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            if(hitInfo.collider.tag == "Player")
            {
                // chase player
                enemyChase.destination = player.position;
            }
        }
        else {
            Debug.DrawLine(transform.position, transform.position + transform.right * visionDistance, Color.green);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{

    public float rotationSpeed;
    public float visionDistance;
    
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        // rotate looking for player
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, visionDistance, 5);
        
        if(hitInfo.collider != null) 
        {
            
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            Debug.Log(hitInfo.collider);
            Debug.Log(hitInfo.collider.tag);
            if(hitInfo.collider.tag == "Player")
            {
                Debug.Log("Enemy sees player");
            }
        }
        else {
            Debug.DrawLine(transform.position, transform.position + transform.right * visionDistance, Color.green);
        }
    }
}

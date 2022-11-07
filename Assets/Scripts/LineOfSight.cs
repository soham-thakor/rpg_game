using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// DOCUMENTATION
// https://docs.unity3d.com/Manual/layers-and-layermasks.html
// https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
// https://www.youtube.com/watch?v=dmQyfWxUNPw

public class LineOfSight : MonoBehaviour
{

    public float rotationSpeed;
    public float visionDistance;
    
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float lifeTime;
    // Do not change this in the inspector
    public bool bulletClone = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if(bulletClone){
            Invoke("DestroyProjectile",lifeTime);
        } 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }

    void DestroyProjectile(){
        Destroy(gameObject);
    }
}

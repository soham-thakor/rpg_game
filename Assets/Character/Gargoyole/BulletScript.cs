using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed;
    public float lifeTime;

    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile",lifeTime);
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

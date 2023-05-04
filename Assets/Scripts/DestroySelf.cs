using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{

    [SerializeField] private float _timeToDestroy;

    void Start()
    {
        Invoke("DestroyObject", _timeToDestroy);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}

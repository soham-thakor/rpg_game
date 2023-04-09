using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHammer : MonoBehaviour
{
    public float damageDealt;

    public void DestroyRock()
    {
        Destroy(gameObject);
    }
}

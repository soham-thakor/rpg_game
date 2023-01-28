using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public GameObject[] NPCTypes;
    public Transform spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        if (staticVariables.lastGuess.Contains("Honorable") || staticVariables.lastGuess.Contains("Ambassador"))
        {
            NPCTypes[0].transform.position = spawnPosition.position;
        }
        else if (staticVariables.lastGuess.Contains("Earl "))
        {
            NPCTypes[1].transform.position = spawnPosition.position;
        }
        else if (staticVariables.lastGuess.Contains("Lady "))
        {
            NPCTypes[2].transform.position = spawnPosition.position;
        }
        else if (staticVariables.lastGuess.Contains("Lord "))
        {
            NPCTypes[3].transform.position = spawnPosition.position;
        }
        else if (staticVariables.lastGuess.Contains("Sir "))
        {
            NPCTypes[4].transform.position = spawnPosition.position;
        }
        else
        {
            NPCTypes[0].transform.position = spawnPosition.position;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

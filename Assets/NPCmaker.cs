using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCmaker : MonoBehaviour
{
    public GameObject[] NPCTypes;

    private GameObject selectedNPC;
    // Start is called before the first frame update
    void Start()
    {
        if(staticVariables.lastGuess.Contains("Honorable") || staticVariables.lastGuess.Contains("Ambassador"))
		{
            selectedNPC = NPCTypes[0];
		}
        else if(staticVariables.lastGuess.Contains("Earl "))
		{
            selectedNPC = NPCTypes[1];
		}
        else if (staticVariables.lastGuess.Contains("Lady "))
        {
            selectedNPC = NPCTypes[2];
        }
        else if (staticVariables.lastGuess.Contains("Lord "))
        {
            selectedNPC = NPCTypes[3];
        }
        else if (staticVariables.lastGuess.Contains("Sir "))
        {
            selectedNPC = NPCTypes[4];
        }
		else
		{
            selectedNPC = NPCTypes[3];
		}
        GameObject spawnedNPC = Instantiate(selectedNPC, gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

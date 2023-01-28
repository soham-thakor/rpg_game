using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public GameObject[] NonPlayerCharacters;
    public Transform spawnPosition;

    private GameObject activeNPC;
    // Start is called before the first frame update
    void Start()
    {
        if (staticVariables.lastGuess.Contains("Honorable") || staticVariables.lastGuess.Contains("Ambassador"))
        {
            NonPlayerCharacters[0].transform.position = spawnPosition.position;
            activeNPC = NonPlayerCharacters[0];
        }
        else if (staticVariables.lastGuess.Contains("Earl "))
        {
            NonPlayerCharacters[1].transform.position = spawnPosition.position;
            activeNPC = NonPlayerCharacters[1];
        }
        else if (staticVariables.lastGuess.Contains("Lady "))
        {
            NonPlayerCharacters[2].transform.position = spawnPosition.position;
            activeNPC = NonPlayerCharacters[2];
        }
        else if (staticVariables.lastGuess.Contains("Lord "))
        {
            NonPlayerCharacters[3].transform.position = spawnPosition.position;
            activeNPC = NonPlayerCharacters[3];
        }
        else if (staticVariables.lastGuess.Contains("Sir "))
        {
            NonPlayerCharacters[4].transform.position = spawnPosition.position;
            activeNPC = NonPlayerCharacters[4];
        }
        else
        {
            NonPlayerCharacters[0].transform.position = spawnPosition.position;
            activeNPC = NonPlayerCharacters[0];
        }

        foreach(GameObject npc in NonPlayerCharacters)
		{
            if(npc != activeNPC)
			{
                npc.SetActive(false);
			}
		}
    }

    // Update is called once per frame
    void Update()
    {

    }
}

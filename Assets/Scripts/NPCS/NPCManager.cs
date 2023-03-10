using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPCManager : MonoBehaviour
{
    public GameObject[] NonPlayerCharacters;
    public Transform spawnPosition;
    public GameObject DialogueBox;
    public Text NPCName;
    public Text NPCMessage;
    public Image NPCPortrait;
    public float typingSpeed;
    public string[] innocentDialogue;
    public string[] guiltyDialogoue;

    private GameObject activeNPC;
    private int index = 0;
    private bool previsoulyActive = false;
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


        NPCName.text = staticVariables.lastGuess;
        NPCPortrait.sprite = activeNPC.transform.Find("Canvas/DialogueBox/Portrait").GetComponent<Image>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        //Make the npc say something every time his dialogue box becomes active
        if (DialogueBox.activeInHierarchy && !previsoulyActive) //if the dialogue box just became active
        {
            if(staticVariables.lastGuess == staticVariables.realVillain)
			{
                StartCoroutine(Type(NPCMessage, guiltyDialogoue[index]));
            }
            else
			{
                StartCoroutine(Type(NPCMessage, innocentDialogue[index]));
			}
            
            index += 1;
            previsoulyActive = true;
        }
        else if (!DialogueBox.activeInHierarchy && previsoulyActive) // if the dialogue box just went inactive
        {
            previsoulyActive = false;
            NPCMessage.text = "";
            //Currently The npc needs to be the last one to talk to transition out of the scene, that could be changed by adding this same code to cutscene manager though
            if((index == guiltyDialogoue.Length || index == innocentDialogue.Length) && GetComponent<CutsceneManager>().GetIndex() == GetComponent<CutsceneManager>().getDialogueLength())
			{
                if(staticVariables.lastGuess == staticVariables.realVillain)
				{
                    StartCoroutine(TransitionScene("ThroneBoss"));
				}
                else
				{
                    if(staticVariables.guesses == 3)
					{
                        StartCoroutine(TransitionScene("CutSceneGameOver"));
					}
                    else
					{
                        StartCoroutine(TransitionScene("Throne Room v2"));
                    }
                    
				}
			}
        }
    }



    public IEnumerator TransitionScene(string scene)
    {
        yield return new WaitForSeconds(0.75f);
        GameObject.FindGameObjectWithTag("Fade").GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }

    IEnumerator Type(Text textDisplay, string sentence)
    {
        
        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        

    }
}

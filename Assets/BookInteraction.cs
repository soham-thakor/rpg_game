using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInteraction : MonoBehaviour
{
    //Using lists within lists to be able to concatenate strings together, since i want to the clues to be random
    public List<string> openMessages;
    public string diaryOwner;
    
    private PlayerSpeech playerSpeech;
    private bool inRange = false;
    private int messageIndex = 0;
    void Start()
    {
        playerSpeech = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSpeech>();
        openMessages.AddRange(NPCStatic.diaryDict[diaryOwner]);
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.F))
        {
            if(messageIndex == openMessages.Count - 1)
			{
                playerSpeech.closeDialogue();
                messageIndex = 0;
			}
            else if(playerSpeech.playerMessage.text == openMessages[messageIndex])
			{
                messageIndex += 1;
                playerSpeech.Speak(openMessages[messageIndex]);
			}
            else
			{
                playerSpeech.Speak(openMessages[messageIndex]);
			}
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
            playerSpeech.closeDialogue();
            messageIndex = 0;
		}

	}

}

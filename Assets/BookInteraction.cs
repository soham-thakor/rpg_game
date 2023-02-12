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
			{// if on the last message
                if(playerSpeech.playerMessage.text != openMessages[messageIndex])
				{//finish skip to the completed message
                    playerSpeech.Speak(openMessages[messageIndex]);
                }
                else
				{
                    messageIndex = 0;
                    playerSpeech.closeDialogue();
                }
                
			}
            else if(playerSpeech.playerMessage.text == openMessages[messageIndex])
			{//if the message we are trying to type is whats already on screen go to the next message
                messageIndex += 1;
                playerSpeech.Speak(openMessages[messageIndex]);
			}
            else
			{//otherwise we are trying to type something thats not on screen so just type it
                //The player speech script handles what to do if you give it something that its already trying to type out
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BookInteraction : MonoBehaviour
{
    //Using lists within lists to be able to concatenate strings together, since i want to the clues to be random
    public List<string> openMessages;
    public string diaryOwner;
    
    private PlayerSpeech playerSpeech;
    private bool inRange = false;
    private int messageIndex = 0;
    private discoveryTracker mapTracker;

    void Start()
    {
        playerSpeech = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSpeech>();
        openMessages.AddRange(NPCStatic.diaryDict[diaryOwner].clues);
        mapTracker = GameObject.FindGameObjectWithTag("Discovery Tracker").GetComponent<discoveryTracker>();

    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.F))
        {
            mapTracker.track("Diary", gameObject);


            if (messageIndex >= openMessages.Count - 3)
			{//if we are reading one of the clues
                int clueIndex = messageIndex - (openMessages.Count - 3);
                NPCStatic.discoverClue(NPCStatic.diaryDict[diaryOwner].clueIDs[clueIndex]);
			}
            if(messageIndex == openMessages.Count - 1)
			{// if on the last message
                if(playerSpeech.playerMessage.text != openMessages[messageIndex])
				{//skip to the completed message if we arent there already
                    playerSpeech.Speak(openMessages[messageIndex]);
                }
                else
				{// if we have the last message out and completed, close dialogue and reset
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

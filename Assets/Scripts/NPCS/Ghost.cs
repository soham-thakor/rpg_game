using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ghost : MonoBehaviour
{
    public List<string> openMessages = new List<string>();

    private PlayerSpeech playerSpeech;
    private bool inRange = false;
    private int messageIndex = 0;
    private NPCStatic.ghostClue ghostClue;
    // Start is called before the first frame update
    void Start()
    {
        playerSpeech = GetComponent<PlayerSpeech>();
        ghostClue = NPCStatic.clues[playerSpeech.playerName.text];
        openMessages.Add(ghostClue.clue); 
    }

    // Update is called once per frame
    void Update()
    {


        if (inRange && Input.GetKeyDown(KeyCode.F))
        {
            //discover this dialogue
            if (!mapStatic.mapData[SceneManager.GetActiveScene().name].dialogues.Contains(gameObject))
            {
                mapStatic.mapData[SceneManager.GetActiveScene().name].dialogues.Add(gameObject);
            }
            if (ghostClue.traitNum != -1)
            {//if this is a clue about one of the traits of the culprit
                NPCStatic.discoverCulpritClue(ghostClue.traitNum);
            }
            if (messageIndex == openMessages.Count - 1)
            {// if on the last message
                if (playerSpeech.playerMessage.text != openMessages[messageIndex])
                {//skip to the completed message if we arent there already
                    playerSpeech.Speak(openMessages[messageIndex]);
                }
                else
                {// if we have the last message out and completed, close dialogue and reset
                    messageIndex = 0;
                    playerSpeech.closeDialogue();
                }

            }
            else if (playerSpeech.playerMessage.text == openMessages[messageIndex])
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
        }

    }

}

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
    private int fragmentIndex = 0;

    private string messageToPrint;
    
    private int randomCharacter;
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
            
            //GOAL: print messages and all their fragments all at once, with randomly generated pieces inbetween
            //Must be able to identify that a message that isnt always the same thing is already printed onto the screen so that it can go to the next one
            /*if(messageIndex == messages.Length)
			{//if we are on the last message close the dialogue and reset so we can read it again
                messageIndex = 0;
                playerSpeech.closeDialogue();
                return;
			}

            if(messages[messageIndex].sentenceFragments.Length == 1)
			{ // if the entire message is set just to be a message just output it
                messageToPrint = messages[messageIndex].sentenceFragments[fragmentIndex];
                messageIndex += 1;
                //playerSpeech.Speak(messages[messageIndex].sentenceFragments[fragmentIndex]);
            }
            else
			{//if the message is in fragments -- Do Fragment, name, fragment, trait, fragment
                //Starts and ends with a string fragment because the lines from books will be in quotes
                //Books will give out trait1

                messageToPrint = "";
                //first fragment
                messageToPrint += messages[messageIndex].sentenceFragments[fragmentIndex];
                fragmentIndex += 1;

                //character name
                randomCharacter = Random.Range(0, NPCStatic.NPCnames.Count);
                while (exemptCharacters.Contains(NPCStatic.NPCnames[randomCharacter].name) || NPCStatic.trait1Clues.Contains(randomCharacter))
				{ //keep picking a character so that we get one that isnt exempt and hasnt gotten a clue yet
                    randomCharacter = Random.Range(0, NPCStatic.NPCnames.Count);
				}
                messageToPrint += NPCStatic.NPCnames[randomCharacter].name;
                NPCStatic.trait1Clues.Add(randomCharacter);

                //Second fragment
                messageToPrint += messages[messageIndex].sentenceFragments[fragmentIndex];
                fragmentIndex += 1;

                //character trait
                randomCharacter = Random.Range(0, NPCStatic.NPCnames.Count);
                while (exemptCharacters.Contains(NPCStatic.NPCnames[randomCharacter].name) || NPCStatic.trait1Clues.Contains(randomCharacter))
                { //keep picking a character so that we get one that isnt exempt and hasnt gotten a clue yet
                    randomCharacter = Random.Range(0, NPCStatic.NPCnames.Count);
                }
                messageToPrint += NPCStatic.NPCnames[randomCharacter].trait1;
                NPCStatic.trait1Clues.Add(randomCharacter);

                //third fragment
                messageToPrint += messages[messageIndex].sentenceFragments[fragmentIndex];
                fragmentIndex = 0;
                messageIndex += 1;
            }
            playerSpeech.Speak(messageToPrint);*/
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
    [System.Serializable]
    public class Message { 
        public string[] sentenceFragments; 
    }

}

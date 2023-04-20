using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Quest : MonoBehaviour
{
    public GameObject npcTrigger;
    public GameObject dialogBox;
    public GameObject npcPortrait;
    public Text dialogText;
    public Text listName;
    public Message [] messages;
    public NPC actor;
    public QuestTrackerData data1;
    public TimeData timeData;

    public int currentQuest = 0;
    private bool playerInRange;
    private int cuMsg = 0;
    private bool onLastMsg = false;
    private GameObject buttonPrompt;
    private SelectionMenu selectionBox;
    private Shop shopMenu;
    private bool currentlyTyping = false;
    private IEnumerator typing;
    private discoveryTracker mapTracker;

    void Start()
    {
        selectionBox = gameObject.GetComponent<SelectionMenu>();
        shopMenu = gameObject.GetComponent<Shop>();

        buttonPrompt = gameObject.transform.Find("Button Prompt").gameObject;
        if (actor.id == 0)
        {
            listName.text = npcTrigger.name;
        }
        dialogBox.SetActive(false);
        npcPortrait.SetActive(false);
        mapTracker = GameObject.FindGameObjectWithTag("Discovery Tracker").GetComponent<discoveryTracker>();

    }

    void Update(){
        //keeps track of the current stage of the game for the scripts 
        //of all npc characters and resets it

        if(data1.questTracker != currentQuest){
            currentQuest = data1.questTracker;
            cuMsg = 0;
        }
        if (playerInRange && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().IsClosestNPC(gameObject) && dialogBox.activeInHierarchy == false)
		{
            buttonPrompt.SetActive(true);
		}
        else
		{
            buttonPrompt.SetActive(false);
		}

        // dialogue interactions system - when key F is pressed AND in range AND this NPC is the one closest to the player AND we arent already talking to someone else
        if(Input.GetKeyDown(KeyCode.F) && playerInRange && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().IsClosestNPC(gameObject) && !staticVariables.immobile){
            //Track this dialogue on the map
            mapTracker.track("Dialogue", gameObject);
            //check if we need to show the earl pop up
            if(gameObject.name == "Earl Thomas" && !staticVariables.seenSerumPopUp)
			{
                GameObject.FindGameObjectWithTag("Player").GetComponent<PopUps>().checkSerumPopUp();
                return;
			}

            if (staticVariables.currentDialogue != gameObject && staticVariables.currentDialogue != null)
			{
                staticVariables.currentDialogue.GetComponent<Quest>().endDialogue();
			}
            
            if(data1.npcTalked(actor.name) == 0 && actor.id == 1)
            {
                data1.interact[actor.name]++;
                data1.interactions++;

                if(data1.interactions == 5)
                {
                    data1.questTracker++;
                    cuMsg = 0;
                    data1.interactions++;
                }
            } 
            else if(data1.npcTalked(actor.name) == 1 && actor.id == 2)
            {
                data1.interact[actor.name]++;
            }

            //sound for the dialogue boxes
            SoundManager.PlaySound(SoundManager.Sound.DialogueSound);

            // controls dialogue boxes
            if(onLastMsg || cuMsg == messages[data1.questTracker].message.Length)
            { 
                // if npc has SelectionMenu component, send message to open menu
                if(selectionBox)
                {
                    selectionBox.OpenMenu();
                }

                if(shopMenu)
                {
                    shopMenu.OpenMenu();
                }
                //end the dialogue
                endDialogue();
            }
            else
            { 
                // start or continue the dialogue
                staticVariables.currentDialogue = gameObject;
                dialogBox.SetActive(true);
                npcPortrait.SetActive(true);
                string msgToDisplay = messages[data1.questTracker].message[cuMsg];
                
                if (!currentlyTyping)
                {
                    typing = Type(dialogText.GetComponent<Text>(), msgToDisplay);
                    StartCoroutine(typing);
                }
                else
				{
                    StopCoroutine(typing);
                    currentlyTyping = false;
                    dialogText.text = msgToDisplay;
                    cuMsg++;
				}
                //If the current message is the last one in the list
                if(dialogText.text == messages[data1.questTracker].message[messages[data1.questTracker].message.Length - 1])
				{
                    onLastMsg = true;
				}
            }
        }
    }
	public void endDialogue()
	{
        if(dialogBox.activeInHierarchy)
		{
            SoundManager.PlaySound(SoundManager.Sound.DialogueSound);
        }
        dialogBox.SetActive(false);
        npcPortrait.SetActive(false);
        cuMsg = 0;
        onLastMsg = false;
        staticVariables.currentDialogue = null;
    }

	// info that will be collected to keep track of the NPC
	[System.Serializable]
    public class Message{
        public string [] message;
    }

    [System.Serializable]
    public class NPC{
        public string name;
        public int id;
    }

    public void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            playerInRange = false;
            cuMsg = 0;
            onLastMsg = false;
            dialogBox.SetActive(false);
        }
    }

    IEnumerator Type(Text textDisplay, string sentence)
    {
        textDisplay.text = "";
        currentlyTyping = true;
        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        currentlyTyping = false;
        cuMsg++;
    }
}

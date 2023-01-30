using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    
    void Start()
    {
        buttonPrompt = gameObject.transform.Find("Button Prompt").gameObject;
        if(actor.id == 0){
            listName.text = npcTrigger.name;
        }
        dialogBox.SetActive(false);
        npcPortrait.SetActive(false);

        //turns on and off the Ghosts
        //give ghosts an actor id of 2
        //all other NPCs give them a 1
        if((data1.npcTalked(actor.name) == 1) && (actor.id == 2)){
            npcTrigger.SetActive(true);
        }else if(actor.id == 2 && data1.npcTalked(actor.name) == 0){
            npcTrigger.SetActive(false);
        }
        
        if((actor.id == 1 || actor.id == 0) && timeData.isNight){
            npcTrigger.SetActive(false);
        }else if(actor.id != 2){
            npcTrigger.SetActive(true);
        }

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
    //will have a problem once all interactions are linked
    //
        //dialogue interactions system when key F is pressed AND in range AND this NPC is the one closest to the player
        if(Input.GetKeyDown(KeyCode.F) && playerInRange && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().IsClosestNPC(gameObject)){
            //if its a regular NPC and they have a clue to give you
            //then they will spawn a ghost after interacting with them
            
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


            Debug.Log(cuMsg);
            //controls dialogue boxes
            if(onLastMsg)
            {
                dialogBox.SetActive(false);
                npcPortrait.SetActive(false);
                cuMsg = 0;
                onLastMsg = false;
            }
            else
            {
                dialogBox.SetActive(true);
                npcPortrait.SetActive(true);
                string msgToDisplay = messages[data1.questTracker].message[cuMsg];
                dialogText.text = msgToDisplay;

                if(cuMsg < messages[data1.questTracker].message.Length){
                    cuMsg++;
                    if(cuMsg == messages[data1.questTracker].message.Length)
					{
                        onLastMsg = true;
					}
                }
            }
        }
    }

    //info that will be collected to keep track of the NPC
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
}

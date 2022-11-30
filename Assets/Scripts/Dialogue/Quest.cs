using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public GameObject npcTrigger;
    public GameObject dialogBox;
    public Text dialogText;
    public Text listName;
    public Message [] messages;
    public NPC actor;
    public QuestTrackerData data1;
    public TimeData timeData;

    public int currentQuest;
    private int questTracker = 0;
    private bool playerInRange;
    private int cuMsg = 0;


    
    void Start()
    {   
        
        if(actor.id == 0){
            listName.text = npcTrigger.name;
        }
        dialogBox.SetActive(false);

        //turns on and off the Ghosts
        //give ghosts an actor id of 2
        //all other NPCs give them a 1
        if((data1.npcTalked(actor.name) == 1) && (actor.id == 2)){
            npcTrigger.SetActive(true);
        }else if(actor.id == 2 && data1.npcTalked(actor.name) == 0){
            npcTrigger.SetActive(false);
        }

        if(timeData.isNight && (actor.id == 1 || actor.id == 0)){
            npcTrigger.SetActive(false);
        }else{
            npcTrigger.SetActive(true);
        }

    }

    void Update(){
        //keeps track of the current stage of the game for the scripts 
        //of all npc characters and resets it

        if(questTracker != currentQuest){
            questTracker = currentQuest;
            cuMsg = 0;
        }

    //will have a problem once all interactions are linked
    //
        //dialogue interactions system when key F is pressed
        if(Input.GetKeyDown(KeyCode.F) && playerInRange){

            //if its a regular NPC and they have a clue to give you
            //then they will spawn a ghost after interacting with them
            if(data1.npcTalked(actor.name) == 0 && actor.id == 1){
                data1.interactions++;
                data1.interact[actor.name]++;
            }else if(data1.npcTalked(actor.name) == 1 && actor.id == 2){
                data1.interactions++;
                data1.interact[actor.name]++;
            }

            //sound for the dialogue boxes
            SoundManager.PlaySound(SoundManager.Sound.DialogueSound);
            
            //controls dialogue boxes
            if(dialogBox.activeInHierarchy){
                dialogBox.SetActive(false);
            }else{
                dialogBox.SetActive(true);
                string msgToDisplay = messages[questTracker].message[cuMsg];
                dialogText.text = msgToDisplay;

                if(cuMsg < messages[questTracker].message.Length-1){
                    cuMsg++;
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
            dialogBox.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public Message [] messages;
    public NPC actor;

    public int currentQuest;
    private int questTracker = 0;
    private bool playerInRange;
    private int cuMsg = 0;
    public QuestTrackerData data1;
    
    void Start()
    {
        dialogBox.SetActive(false);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.E) && (currentQuest < 2)){
            currentQuest++;
        }

        if(questTracker != currentQuest){
            questTracker = currentQuest;
            cuMsg = 0;
        }
        if(Input.GetKeyDown(KeyCode.F) && playerInRange){
            if(data1.npcTalked(actor.name) == 0 && actor.id == 1){
                data1.interactions++;
                data1.interact[actor.name]++;
            }else if(data1.npcTalked(actor.name) >= 0 && actor.id == 2){
                data1.interactions++;
                data1.interact[actor.name]++;
            }

            SoundManager.PlaySound(SoundManager.Sound.DialogueSound);
            
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

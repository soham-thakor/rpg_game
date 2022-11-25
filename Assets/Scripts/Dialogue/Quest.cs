using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public Message [] messages;
    public int currentQuest;
    private int questTracker = 0;
    private bool playerInRange;
    private int activemsg = 0;

    void Start()
    {
        dialogBox.SetActive(false);
    }


    void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            currentQuest++;
        }
        if(questTracker != currentQuest){
            questTracker = currentQuest;
            activemsg = 0;
        }
        if(Input.GetKeyDown(KeyCode.F) && playerInRange){
            SoundManager.PlaySound(SoundManager.Sound.DialogueSound);
            
            if(dialogBox.activeInHierarchy){
                dialogBox.SetActive(false);
            }else{
                dialogBox.SetActive(true);
                string msgToDisplay = messages[questTracker].message[activemsg];
                dialogText.text = msgToDisplay;

                if(activemsg < messages[questTracker].message.Length-1){
                    activemsg++;
                }
            }
        }
    }

    [System.Serializable]
    public class Message{
        public string [] message;
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

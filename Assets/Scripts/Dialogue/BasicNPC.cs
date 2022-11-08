using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicNPC : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public Message[] messages;
    public Actor[] actors;

    private bool playerInRange;
    private int index;
    private int activemsg = 0;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
    }

    void Update(){

        if(Input.GetKeyDown(KeyCode.F) && playerInRange)
        {
            SoundManager.PlaySound(SoundManager.Sound.DialogueSound);

            if(dialogBox.activeInHierarchy){
                dialogBox.SetActive(false);
            }else{
                dialogBox.SetActive(true);
                Message msgToDisplay = messages[activemsg];
                dialogText.text = msgToDisplay.message;
                if(activemsg < messages.Length-1){
                    activemsg++;
                }
            }
        }
    }
    

    public void NextMsg(){
        activemsg++;
        if(activemsg < messages.Length){
            Message msgToDisplay = messages[activemsg];
            dialogBox.SetActive(true);
            dialogText.text = msgToDisplay.message;
        }else{
            dialogBox.SetActive(false);
        }
    }

    [System.Serializable]
    public class Message{
        public int actorID;
        public string message;
    }

    [System.Serializable]
    public class Actor
    {
        public string name;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicNPC : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool playerInRange;
    private int index;
    public Message[] messages;
    public Actor[] actors;
    int activemsg = 0;
    public static bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
    }

    // Update is called once per frame
    // void Update()
    // {

    //     if(Input.GetKey(KeyCode.F) && playerInRange){
    //         if(dialogBox.activeInHierarchy){
    //             dialogBox.SetActive(false);
    //         }else{
    //             dialogBox.SetActive(true);
    //             dialogText.text = dialog;
    //         }
    //     }
    // }

    void Update(){

        if(Input.GetKeyDown(KeyCode.F) && playerInRange){
            
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
            isActive = false;
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
        }
    }
}

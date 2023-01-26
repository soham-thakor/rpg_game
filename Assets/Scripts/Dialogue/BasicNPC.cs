using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BasicNPC : MonoBehaviour
{
    public GameObject dialogBox;
    public GameObject selectionBox;

    public Text dialogText;
    public Message [] messages;
    public NPC actor;

    public QuestTrackerData data1;
    public SelectionData data2;

    public int gameStartScene;
    public int currentQuest;
    private int questTracker = 0;
    private bool playerInRange;
    private int cuMsg = 0;
    private bool dialogDone;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        selectionBox.SetActive(false);
    }

    void Update(){
        
        if(questTracker != currentQuest){
            questTracker = currentQuest;
            cuMsg = 0;
        }

        if(Input.GetKeyDown(KeyCode.F) && playerInRange)
        {
            SoundManager.PlaySound(SoundManager.Sound.DialogueSound);
            if(selectionBox.activeInHierarchy){
                selectionBox.SetActive(false);
                staticVariables.immobile = false;
                dialogBox.SetActive(true);
                string msgToDisplay = "Today is a good day to solve it ya know.";
                dialogText.text = msgToDisplay;
                cuMsg--;
            }
            else if(dialogBox.activeInHierarchy){
                dialogBox.SetActive(false);

                //experimental from here
                if(cuMsg == messages[questTracker].message.Length-1){
                    selectionBox.SetActive(true);
                    staticVariables.immobile = true;
                }
            }
            else{
                dialogBox.SetActive(true);
                string msgToDisplay = messages[questTracker].message[cuMsg];
                dialogText.text = msgToDisplay;

                if(cuMsg < messages[questTracker].message.Length-1){
                    cuMsg++;
                }
            }
        }
    }

    public void CorrectChoice(){
        selectionBox.SetActive(false);
        data2.bossFight = true;
        SceneManager.LoadScene(gameStartScene);
        staticVariables.immobile = false;
    }

    public void WrongChoice(){
        selectionBox.SetActive(false);
        data2.wrongChoice();
        if(data2.gameOver){
            SceneManager.LoadScene(14);
        }
        staticVariables.immobile = false;
    }

    // public void sSystem(){
        
    // }
    
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

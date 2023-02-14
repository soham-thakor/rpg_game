using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneDialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public GameObject message;
    public GameObject NPCPortrait;
    public float typingSpeed;
    public float finishTypingDelay; // time from when typing finishes and box deactivates
    public string[] dialogue;

    // stores next dialogue to be said
    private Queue<string> dialogueQueue = new Queue<string>();
    private Text messageBox;

    void Start()
    {
        messageBox = message.GetComponent<Text>();
        foreach(string i in dialogue){
            dialogueQueue.Enqueue(i);
        }
        dialogueQueue.Enqueue("AHHHHHHHHHHHHHHHHHHHHHHHHHH");
    }

    public void SayDialogue()
    {
        dialogueBox.SetActive(true);
        NPCPortrait.SetActive(true);
        StartCoroutine(Type(messageBox, dialogueQueue.Dequeue()));
    }

    public void MoveScene(string scene)
    {
        TransitionScene(scene);
    }

    public IEnumerator TransitionScene(string scene)
    {
        yield return new WaitForSeconds(0.75f);
        GameObject.FindGameObjectWithTag("Fade").GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }

    IEnumerator Type(Text textDisplay, string sentence)
    {
        Debug.Log("Dequeued " + sentence);
        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(finishTypingDelay);
        
        textDisplay.text = "";
        dialogueBox.SetActive(false);
        NPCPortrait.SetActive(false);
    }
}
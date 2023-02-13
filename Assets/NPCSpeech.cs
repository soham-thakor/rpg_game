using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCSpeech : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text npcMessage;
    public Text npcName;
    public float typingSpeed = 0.02f;

    private IEnumerator typing;
    private bool currentlyTyping = false;
    private string currentMessage;
    public void Speak(string message)
    {
        if (currentMessage == message && currentlyTyping) // if we are trying to say something that is already being said finish it
        {
            StopCoroutine(typing);
            currentlyTyping = false;
            SoundManager.PlaySound(SoundManager.Sound.DialogueSound);
            npcMessage.text = message;
            return;
        }
        else if (npcMessage.text == message)
        { // if we are trying to say something thats already on screen then close the dialogue
            closeDialogue();
            return;
        }
        else // otherwise that means that we are trying to say something that isnt already on the screen,  so type as usual
        {
            npcMessage.text = "";
            dialogueBox.SetActive(true);
            SoundManager.PlaySound(SoundManager.Sound.DialogueSound);
            typing = Type(npcMessage, message);
            StartCoroutine(typing);
        }
    }

    public void closeDialogue()
    {
        if (dialogueBox.activeInHierarchy)
        {// this gets called when players leave the range of something they can interact with, so I dont want the sound to play if they just walk by without pulling up the dialogue box
            SoundManager.PlaySound(SoundManager.Sound.DialogueSound);
        }
        dialogueBox.SetActive(false);
        if (currentlyTyping)
        {
            StopCoroutine(typing);
            currentlyTyping = false;
        }
        npcMessage.text = "";

    }
    IEnumerator Type(Text textDisplay, string sentence)
    {
        currentlyTyping = true;
        currentMessage = sentence;
        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        currentlyTyping = false;


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpeech : MonoBehaviour
{
    public float typingSpeed = 0.02f;
    public GameObject dialogueBox;
    public Text playerName;
    public Text playerMessage;


    private IEnumerator typing;
    private bool currentlyTyping = false;
    private string currentMessage;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.CompareTag("Player"))
		{
            playerName.text = staticVariables.chosenName;
        }
    }

    public void Speak(string message)
	{
        if (currentMessage == message && currentlyTyping) // if we are trying to say something that is already being said finish it
        {
            StopCoroutine(typing);
            currentlyTyping = false;
            SoundManager.PlaySound(SoundManager.Sound.DialogueSound);
            playerMessage.text = message;
            return;
        }
        else if (playerMessage.text == message)
        { // if we are trying to say something thats already on screen then close the dialogue
            closeDialogue();
            return;
        }
        else // otherwise that means that we are trying to say something that isnt already on the screen,  so type as usual
        {
            playerMessage.text = "";
            dialogueBox.SetActive(true);
            SoundManager.PlaySound(SoundManager.Sound.DialogueSound);
            typing = Type(playerMessage, message);
            StartCoroutine(typing);
        }
	}

    public void closeDialogue()
	{
        if(dialogueBox.activeInHierarchy)
		{// this gets called when players leave the range of something they can interact with, so I dont want the sound to play if they just walk by without pulling up the dialogue box
            SoundManager.PlaySound(SoundManager.Sound.DialogueSound);
        }
        dialogueBox.SetActive(false);
        if (currentlyTyping)
		{
            StopCoroutine(typing);
            currentlyTyping = false;
        }
        playerMessage.text = "";
        
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

    public string getCurrentMessage()
	{
        return currentMessage;
	}
}

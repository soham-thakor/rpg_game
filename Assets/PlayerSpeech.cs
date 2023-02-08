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
    // Start is called before the first frame update
    void Start()
    {
        playerName.text = staticVariables.chosenName;
    }

    public void Speak(string message)
	{
        dialogueBox.SetActive(true);
        typing = Type(playerMessage, message);
        StartCoroutine(typing);
	}

    public void closeDialogue()
	{
        dialogueBox.SetActive(false);
        if(currentlyTyping)
		{
            StopCoroutine(typing);
            currentlyTyping = false;
        }
        playerMessage.text = "";
        
	}
    IEnumerator Type(Text textDisplay, string sentence)
    {
        currentlyTyping = true;
        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        currentlyTyping = false;

    }
}

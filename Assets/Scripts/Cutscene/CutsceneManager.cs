using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector timeline;
    public AnimationClip runUp;
    public GameObject playerMessage;
    public float typingSpeed;
    public string[] playerDialogue;

    private int index = 0;
    private bool previouslyTyping = false;

    // Update is called once per frame
    void Update()
    {
        //Make the player say something every time his dialogue box becomes active
        if(playerMessage.activeInHierarchy && !previouslyTyping) //if the dialogue box just became active
		{
            StartCoroutine(Type(playerMessage.GetComponent<Text>(), playerDialogue[index]));
            index += 1;
            previouslyTyping = true;
		}
        else if(!playerMessage.activeInHierarchy && previouslyTyping) // if the dialogue box just went inactive
		{
            previouslyTyping = false;
            playerMessage.GetComponent<Text>().text = "";
		}

    }


    IEnumerator Type(Text textDisplay, string sentence)
    {
        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
    }

    public int GetIndex()
	{
        return index;
	}
    public int getDialogueLength()
	{
        return playerDialogue.Length;
	}
}

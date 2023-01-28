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
    public GameObject playerDialogueBox;
    public float typingSpeed;
    public string[] playerDialogue;

    private int index = 0;
    private bool previsoulyActive = false;
    private bool currentlyTyping = false;
    private bool previouslyTyping = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Make the player say something every time his dialogue box becomes active
        if(playerDialogueBox.activeInHierarchy && !previsoulyActive) //if the dialogue box just became active
		{
            StartCoroutine(Type(playerDialogueBox.GetComponent<Text>(), playerDialogue[index]));
            index += 1;
            previsoulyActive = true;
		}
        else if(!playerDialogueBox.activeInHierarchy && previsoulyActive) // if the dialogue box just went inactive
		{
            previsoulyActive = false;
            playerDialogueBox.GetComponent<Text>().text = "";
		}

        //keep track of if we just finished typing so that if we want to put out another message without disabling the dialogue box we can
        if(!previouslyTyping && currentlyTyping) // if we started typing
		{
            previouslyTyping = true;
		}
        else if(previouslyTyping && !currentlyTyping) // if we stopped typing
		{
            previouslyTyping = false;
		}
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

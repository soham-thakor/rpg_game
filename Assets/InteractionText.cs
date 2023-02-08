using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionText : MonoBehaviour
{
    public string Message;
    public float typingSpeed = 0.02f; 


    private Text playerName;
    private Text playerMessage;
    private GameObject playerDialogue;
    private bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        playerDialogue = GameObject.FindGameObjectWithTag("PlayerDialogue");
        playerName = playerDialogue.transform.Find("Player Name").GetComponent<Text>();
        playerMessage = playerDialogue.transform.Find("Player Message").GetComponent<Text>();
        playerName.text = staticVariables.chosenName;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.F) && playerDialogue.activeInHierarchy == false)
		{
            printMessage();
		}
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        inRange = true;
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
        inRange = false;
        if(playerDialogue.activeInHierarchy == true)
		{
            playerDialogue.SetActive(false);
		}
	}

    public void printMessage()
	{
        playerDialogue.SetActive(true);
        StartCoroutine(Type(playerMessage, Message));
    }
    IEnumerator Type(Text textDisplay, string sentence)
    {

        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }


    }
}

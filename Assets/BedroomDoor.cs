using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomDoor : MonoBehaviour
{
	public GameObject buttonPrompt;
	public string lockedMessage;

	private PlayerSpeech playerSpeech;
	private bool inRange = false;
	// Start is called before the first frame update
	public void Start()
	{
		if(staticVariables.bedroomDoorOpen)
		{
			gameObject.SetActive(false);
		}
		playerSpeech = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSpeech>();
	}
	// Update is called once per frame
	void Update()
	{
		if (inRange && Input.GetKeyDown(KeyCode.F))
		{
			if (staticVariables.aquiredRoomKey)
			{
				gameObject.SetActive(false);
				staticVariables.bedroomDoorOpen = true;
			}
			else
			{
				if(playerSpeech.dialogueBox.activeInHierarchy == false)
				{
					playerSpeech.Speak(lockedMessage);
				}
				else
				{
					playerSpeech.closeDialogue();
				}
			}
		}
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")) // if the player is in range 
		{
			buttonPrompt.SetActive(true);
			inRange = true;
		}
	}
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player")) // if the player leaves the range
		{
			inRange = false;
			buttonPrompt.SetActive(false);
			playerSpeech.closeDialogue();
		}
	}
}

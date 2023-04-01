using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bookshelf : MonoBehaviour
{
	public string failText;
	public string successText;


    private bool inRange = false;
	private PlayerSpeech playerSpeech;


	private void Start()
	{
		if(staticVariables.secretEntranceFound && gameObject == staticVariables.secretBookshelf)
		{
			disableBookshelf();
		}
		playerSpeech = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSpeech>();
	}
	// Update is called once per frame
	void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && inRange)
		{
			if(!mapStatic.mapData[SceneManager.GetActiveScene().name].interactables.Contains(gameObject))
			{
				mapStatic.mapData[SceneManager.GetActiveScene().name].interactables.Add(gameObject);
			}
			if (staticVariables.secretBookshelf == gameObject)
			{
				if (playerSpeech.dialogueBox.activeInHierarchy == false) { 
					playerSpeech.Speak(successText);
				}
				else
				{
					gameObject.GetComponent<Animator>().SetTrigger("Fade");
					staticVariables.secretEntranceFound = true;
					playerSpeech.closeDialogue();
				}
			}
			else
			{
					playerSpeech.Speak(failText);
			}
		}
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			inRange = true;
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			inRange = false;
			playerSpeech.closeDialogue();
		}
	}


	private void disableBookshelf()
	{
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		gameObject.GetComponent<BoxCollider2D>().enabled = false;
		gameObject.GetComponent<CircleCollider2D>().enabled = false;
	}


}

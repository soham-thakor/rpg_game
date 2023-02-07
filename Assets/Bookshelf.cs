using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bookshelf : MonoBehaviour
{

    private bool inRange = false;

	private void Start()
	{
		if(staticVariables.secretEntranceFound && gameObject == staticVariables.secretBookshelf)
		{
			disableBookshelf();
		}
	}
	// Update is called once per frame
	void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && inRange && gameObject == staticVariables.secretBookshelf)
		{
			gameObject.GetComponent<Animator>().SetTrigger("Fade");
			staticVariables.secretEntranceFound = true;
		}
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        inRange = true;
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
        inRange = false;
	}


	private void disableBookshelf()
	{
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		gameObject.GetComponent<BoxCollider2D>().enabled = false;
		gameObject.GetComponent<CircleCollider2D>().enabled = false;
	}
}

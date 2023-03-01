using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPrompt : MonoBehaviour
{


    public GameObject buttonPrompt;
    
    // Start is called before the first frame update
    void Start()
    {
        //buttonPrompt = gameObject.transform.GetChild(0).gameObject;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player") && gameObject.GetComponent<SpriteRenderer>().enabled)
		{
            buttonPrompt.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
            buttonPrompt.SetActive(false);
		}
	}
}

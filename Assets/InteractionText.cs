using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionText : MonoBehaviour
{
    public string message;

    private PlayerSpeech playerSpeech;
    private bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        playerSpeech = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSpeech>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.F))
		{
                playerSpeech.Speak(message);
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

}

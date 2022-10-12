using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour
{
    public DialogueTrigger trigger;
    public GameObject DialogueBox;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            DialogueBox.SetActive(true);
            trigger.StartDialogue();
        }
            
    }
}

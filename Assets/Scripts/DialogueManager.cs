using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public Text actorName;
    public Text msgText;
    public GameObject DialogueBox;
    public AudioSource DialogueSound;

    public float closeBox = .5f;
    Transform dBox;
    Transform player;

    Message[] currentmsg;
    Actor[] currentActor;
    int activemsg = 0;
    public static bool isActive = false;

   

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentmsg = messages;
        currentActor = actors;
        activemsg = 0;
        isActive = true;
        DisplayMsg();

    }

    void DisplayMsg()
    {
        Message msgToDisplay = currentmsg[activemsg];
        msgText.text = msgToDisplay.message;

        Actor actorToDiaplay = currentActor[msgToDisplay.actorID];
        actorName.text = actorToDiaplay.name;
        actorImage.sprite = actorToDiaplay.sprite;

    }

    public void NextMsg()
    {
        activemsg++;
        if (activemsg < currentmsg.Length)
        {
            DisplayMsg();
        }
        else
        {
            isActive = false;
            DialogueBox.SetActive(false);
            
        }
    }

   

    // Start is called before the first frame update
    void Start()
    {
         
        DialogueBox.SetActive(false);
        player= GameObject.FindGameObjectWithTag("Player").transform;
        dBox = GameObject.FindGameObjectWithTag("Dbox").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(player.position, dBox.position) >= closeBox && isActive == true )
        {

            DialogueBox.SetActive(false);

        }
        

        if (Input.GetKeyDown(KeyCode.F) && isActive == true)
        {
            DialogueSound.Play();
            NextMsg();
        }
    }
}

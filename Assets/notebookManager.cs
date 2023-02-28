using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class notebookManager : MonoBehaviour
{
    public GameObject prevButton;
    public GameObject nextButton;
    public TextMeshProUGUI pageName;
    public TextMeshProUGUI[] traits;
    //public TextMeshProUGUI playerNotes;
    public TMP_InputField inputField;

    public Image pagePortrait;
    public Sprite[] portraits;

    void Start()
    {
        changePage(0);
        /*if(NotebookStatic.currentPage == 0)
        {//if we are on the first page dont show the prev page button
            prevButton.SetActive(false);
		}
        if(NotebookStatic.currentPage == NotebookStatic.playerNotes.Count - 1)
        {//if we are on the last page dont show the next page button
            prevButton.SetActive(false);
		}*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void pickPortrait(string name)
	{
        //Debug.Log(name);
        if(name.Contains("Honorable") || name.Contains("Ambassador"))
		{
            pagePortrait.sprite = portraits[0];
		}
        else if(name.Contains("Earl"))
		{
            pagePortrait.sprite = portraits[1];
		}
        else if(name.Contains("Lady"))
		{
            pagePortrait.sprite = portraits[2];
		}
        else if(name.Contains("Lord"))
		{
            pagePortrait.sprite = portraits[3];
		}
        else if(name.Contains("Sir"))
		{
            pagePortrait.sprite = portraits[4];
		}
		else
		{
            pagePortrait.sprite = portraits[5];
		}
	}
    public void savePlayerNotes()
	{
        NotebookStatic.playerNotes[NotebookStatic.currentPage] = inputField.text;
	}
    public void changePage(int page)
	{
        int newIndex = NotebookStatic.currentPage + page;
        savePlayerNotes();
        inputField.text = NotebookStatic.playerNotes[newIndex];
        //playerNotes. = NotebookStatic.playerNotes[newIndex];
        Debug.Log(newIndex);
        Debug.Log(NotebookStatic.playerNotes[newIndex]);
        NotebookStatic.currentPage = newIndex;
        NPCStatic.NPC pageNPC;
        if (newIndex == 0)
        {// if we are changing to the first page turn off the prev page button
            prevButton.SetActive(false);
            pageNPC = NPCStatic.NPCnames[0];
        }
        else if (newIndex == NotebookStatic.playerNotes.Count - 1)
        {// if we are changing to the last page turn the next page button off
            nextButton.SetActive(false);
            pageNPC = NotebookStatic.culpritNPC;
        }
		else
		{ // this is just getting the NPC for any page that isnt the first or last
            nextButton.SetActive(true);
            prevButton.SetActive(true);
            pageNPC = NPCStatic.NPCnames[newIndex];
        }

        pageName.text = pageNPC.name;
        pickPortrait(pageName.text);
        if (pageNPC == NotebookStatic.culpritNPC)
		{// this is just here so i dont have to think about it for now
            return;
		}
        
        //reveal clues as needed
        for(int i = 1; i < 4; ++i)
		{
            if(NPCStatic.discoveredClues.Contains(new Tuple<int, int>(newIndex, i)))
			{
                traits[i-1].text = NPCStatic.getTrait(newIndex, i);
			}
			else
			{
                traits[i-1].text = "Unknown";
			}
		}

        

        
    }
}

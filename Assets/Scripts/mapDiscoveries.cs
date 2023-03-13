using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mapDiscoveries : MonoBehaviour
{
    public GameObject mapUI;
    public TextMeshProUGUI areaName;
    public TextMeshProUGUI interactablesDiscovered;
    public TextMeshProUGUI diariesDiscovered;
    public TextMeshProUGUI dialoguesDiscovered;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mapUI.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.M))
		{
            if(PauseManager.isPaused)
			{
                return;
			}
            mapUI.SetActive(!mapUI.activeInHierarchy);
            showArea(SceneManager.GetActiveScene().name);
		}
    }

    void showArea(string sceneName)
	{
        Debug.Log("made it");
        mapStatic.Discoveries areaData = mapStatic.mapData[sceneName];
        areaName.text = areaData.areaName;
        interactablesDiscovered.text = "Interactables: " + areaData.interactables.ToString() + "/" + (getAreaCount("Bookshelf") + getAreaCount("Interactable")).ToString();
        dialoguesDiscovered.text = "Dialogues: " + areaData.dialogues.ToString() + "/" + getAreaCount("NPC").ToString();
        diariesDiscovered.text = "Diaries: " + areaData.dialogues.ToString() + "/" + getAreaCount("Diary").ToString();
    }
  

    int getAreaCount(string tag)
	{
        List<GameObject> temp = new List<GameObject>();
        temp.AddRange(GameObject.FindGameObjectsWithTag(tag));
        return temp.Count;
	}
}

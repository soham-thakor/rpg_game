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

    private string interactablesMax;
    private string dialoguesMax;
    private string diariesMax;
    private PauseManager pauseManager;

	private void Awake()
	{
        interactablesMax = (getAreaCount("Bookshelf") + getAreaCount("Interactable")).ToString();
        dialoguesMax = getAreaCount("NPC").ToString();
        diariesMax = getAreaCount("Diary").ToString();
        pauseManager = GameObject.FindGameObjectWithTag("Pause Menu").GetComponent<PauseManager>();
    }
	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mapUI.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.M))
		{
            if(pauseManager.notebookPanel.activeInHierarchy)
			{
                return;
			}
            mapUI.SetActive(!mapUI.activeInHierarchy);
            pauseManager.ChangePause();
            showArea(SceneManager.GetActiveScene().name);
		}
    }

    void showArea(string sceneName)
	{
        mapStatic.Discoveries areaData = mapStatic.mapData[sceneName];
        areaName.text = areaData.areaName;
        interactablesDiscovered.text = "Interactables: " + areaData.interactables.Count.ToString() + " of " + interactablesMax;
        dialoguesDiscovered.text = "Dialogues: " + areaData.dialogues.Count.ToString() + " of " + dialoguesMax;
        diariesDiscovered.text = "Diaries: " + areaData.diaries.Count.ToString() + " of " + diariesMax;
    }
  

    int getAreaCount(string tag)
	{
        List<GameObject> temp = new List<GameObject>();
        temp.AddRange(GameObject.FindGameObjectsWithTag(tag));
        return temp.Count;
	}
}

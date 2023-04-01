using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class discoveryTracker : MonoBehaviour
{

    private GameObject[] areaDialogues;
    private GameObject[] areaInteractables;
    private GameObject[] areaDiaries;

    // Start is called before the first frame update
    void Start()
    { 
        //Fill arrays
        areaDialogues = GameObject.FindGameObjectsWithTag("NPC");
        areaDiaries = GameObject.FindGameObjectsWithTag("Diary");
        List<GameObject> temp = new List<GameObject>();
        temp.AddRange(GameObject.FindGameObjectsWithTag("Interactable"));
        temp.AddRange(GameObject.FindGameObjectsWithTag("Bookshelf"));
        areaInteractables = temp.ToArray();

        //Sort arrays
        Array.Sort(areaInteractables, new ObjectSorter());
        Array.Sort(areaDiaries, new ObjectSorter());
        Array.Sort(areaDialogues, new ObjectSorter());
    }

    public void track(string type, GameObject obj)
	{
        switch(type)
		{
            case "Dialogue":
                int i = Array.IndexOf(areaDialogues, obj);
                if (!mapStatic.mapData[SceneManager.GetActiveScene().name].dialogues.Contains(i))
                {
                    mapStatic.mapData[SceneManager.GetActiveScene().name].dialogues.Add(i);
                }
                break;

            case "Diary":
                int j = Array.IndexOf(areaDiaries, obj);
                if (!mapStatic.mapData[SceneManager.GetActiveScene().name].diaries.Contains(j))
                {
                    mapStatic.mapData[SceneManager.GetActiveScene().name].diaries.Add(j);
                }
                break;

            case "Interactable":
                int k = Array.IndexOf(areaInteractables, obj);
                if (!mapStatic.mapData[SceneManager.GetActiveScene().name].interactables.Contains(k))
                {
                    mapStatic.mapData[SceneManager.GetActiveScene().name].interactables.Add(k);
                }
                break;

            default:
                break;
		}
	}

    
    public class ObjectSorter : IComparer
    {

        // Calls CaseInsensitiveComparer.Compare on the monster name string.
        int IComparer.Compare(System.Object x, System.Object y)
        {
            return ((new CaseInsensitiveComparer()).Compare(((GameObject)x).name, ((GameObject)y).name));
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PopUps : MonoBehaviour
{
    public GameObject mapPopUp;
    public GameObject serumPopUp;
    public GameObject notebookPopUp;

    private PauseManager pauseManager;
    private float sceneTime;
    // Start is called before the first frame update
    void Start()
    {
        pauseManager = GameObject.FindGameObjectWithTag("Pause Menu").GetComponent<PauseManager>();
        sceneTime = Time.time;
    }

    // Update is called once per frame
    void Update() 
    { 
        if(!staticVariables.seenMapPopUp && SceneManager.GetActiveScene().name == "Halls Bottom" && (Time.time - sceneTime) >= 1f)
		{
            popUp(mapPopUp);
		}
    }


    public void popUp(GameObject panel)
	{
        panel.SetActive(true);
        pauseManager.ChangePause();
	}

    public void closePopUps()
	{
        mapPopUp.SetActive(false);
        serumPopUp.SetActive(false);
        notebookPopUp.SetActive(false);
        pauseManager.ChangePause();
	}
}

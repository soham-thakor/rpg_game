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
    { //Clue pop up script will be called from ghost.cs and BookInteraction.cs
        //Earl pop up will have to be called from quest.cs
        if(SceneManager.GetActiveScene().name == "Halls Bottom")
		{
            checkMapPopUp();
		}
    }

    public void checkMapPopUp()
	{
        if(Time.time - sceneTime >= 1f && !staticVariables.seenMapPopUp)
		{
            popUp(mapPopUp);
            staticVariables.seenMapPopUp = true;
		}
	}
    
    public void checkNotebookPopUp()
	{//I dont check for the static variable here because doing it in the other scripts allows me to stop their dialogue from coming up
        popUp(notebookPopUp);
        staticVariables.seenNotebookPopUp = true;
	}
    public void checkSerumPopUp()
    {//I dont check for the static variable here because doing it in the other scripts allows me to stop their dialogue from coming up
        popUp(serumPopUp);
        staticVariables.seenSerumPopUp = true;
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

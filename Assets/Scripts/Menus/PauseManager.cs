using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pausePanel, notebookPanel, settingsPanel, controlPanel, controlChangePanel;
    public string mainMenu;
    public GameObject popUpToggle;
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        notebookPanel.SetActive(false);
        settingsPanel.SetActive(false);
        controlPanel.SetActive(false);
        isPaused = false;
        popUpToggle.GetComponent<Toggle>().isOn = staticVariables.popUpsEnabled;
        changeTutorialPopUpSetting();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("pause"))
        {
            if(staticVariables.currentDialogue != null)
			{
                staticVariables.currentDialogue.GetComponent<Quest>().endDialogue();
			}
            ChangePause();
            if (isPaused)
			{
                pausePanel.SetActive(true);
			}
            
        }
        
    }

    public void ChangePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;
            //AudioListener.pause = true;
        }
        else
        {
            hidePanels();
            Time.timeScale = 1f;
            //AudioListener.pause = false;
        }
        SoundManager.PlaySound(SoundManager.Sound.DialogueSound);
    }

    // TODO: use an enumerator or make these into templates
    public void showNotebook()
    {
        pausePanel.SetActive(false);
        notebookPanel.GetComponent<notebookManager>().changePage(0);
        notebookPanel.SetActive(true);
        SoundManager.PlaySound(SoundManager.Sound.DialogueSound);
    }

    public void showSettings()
    {
        controlChangePanel.SetActive(false);
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
        SoundManager.PlaySound(SoundManager.Sound.DialogueSound);
    }
    
    public void showControlChange()
	{
        //settingsPanel.SetActive(false);
        controlChangePanel.SetActive(true);
        SoundManager.PlaySound(SoundManager.Sound.DialogueSound);
	}

    public void showOptions(GameObject previous_panel)
    {
        previous_panel.SetActive(false);
        pausePanel.SetActive(true);
        SoundManager.PlaySound(SoundManager.Sound.DialogueSound);
    }

    public void showControls() {
        pausePanel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public void hidePanels()
    {
        pausePanel.SetActive(false);
        notebookPanel.SetActive(false);
        settingsPanel.SetActive(false);
        controlPanel.SetActive(false);
        controlChangePanel.SetActive(false);
    }

    public void Quit()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SoundManager.PlaySound(SoundManager.Sound.DialogueSound);
    }

    public void changeTutorialPopUpSetting()
	{
        if(popUpToggle.GetComponent<Toggle>().isOn == staticVariables.popUpsEnabled)
		{
            return;
		}
        staticVariables.popUpsEnabled = !staticVariables.popUpsEnabled;

    }
}

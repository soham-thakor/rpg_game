using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel, notebookPanel, settingsPanel, controlPanel;
    public string mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        notebookPanel.SetActive(false);
        settingsPanel.SetActive(false);
        controlPanel.SetActive(false);
        isPaused = false;
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
        }
        
    }

    public void ChangePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            AudioListener.pause = true;
        }
        else
        {
            hidePanels();
            Time.timeScale = 1f;
            AudioListener.pause = false;
        }
    }

    // TODO: use an enumerator or make these into templates
    public void showNotebook()
    {
        pausePanel.SetActive(false);
        notebookPanel.SetActive(true);
        //SoundManager.PlaySound(SoundManager.Sound.DialogueSound);
    }

    public void showSettings()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
        //SoundManager.PlaySound(SoundManager.Sound.DialogueSound);
    }

    public void showOptions(GameObject previous_panel)
    {
        previous_panel.SetActive(false);
        pausePanel.SetActive(true);
        //SoundManager.PlaySound(SoundManager.Sound.DialogueSound);
    }

    public void showControls() {
        Debug.Log("activating control panel!");
        pausePanel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public void hidePanels()
    {
        pausePanel.SetActive(false);
        notebookPanel.SetActive(false);
        settingsPanel.SetActive(false);
        controlPanel.SetActive(false);
    }

    public void Quit()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        //SoundManager.PlaySound(SoundManager.Sound.DialogueSound);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel, inventoryPanel, settingsPanel;
    public string mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(false);
        settingsPanel.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("pause"))
        {
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

    public void showInventory()
    {
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(true);
    }

    public void showSettings()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void showOptions(GameObject previous_panel)
    {
        previous_panel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void hidePanels()
    {
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void Quit()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
}

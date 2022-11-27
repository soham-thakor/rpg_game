using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionScript : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused;
    public void CorrectChoice(){
        Debug.Log("Correct choice");
        ChangePause();
    }

    public void WrongChoice(){
        Debug.Log("Wrong choice");
        ChangePause();
    }

    void start(){
        pausePanel.SetActive(false);
        isPaused = false;
    }

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
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}

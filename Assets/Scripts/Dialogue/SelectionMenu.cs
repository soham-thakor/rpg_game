using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectionMenu : MonoBehaviour
{

    public SelectionData data2;
    public GameObject selectionBox;

    // automatic getters and setters, read only outside class
    public bool inMenu { get; private set;}

    public void Start() 
    {
        inMenu = false;
        selectionBox.SetActive(false);
    }

    public void OpenMenu()
    {
        inMenu = true;
        selectionBox.SetActive(true);
        staticVariables.immobile = true;
    }

    // public void CorrectChoice(){
    //     selectionBox.SetActive(false);
    //     data2.bossFight = true;
    //     SceneManager.LoadScene(14);
    //     staticVariables.immobile = false;
    //     staticVariables.guesses += 1;
    // }

    // public void WrongChoice(){
    //     selectionBox.SetActive(false);
    //     data2.wrongChoice();
    //     if(data2.gameOver){
    //         SceneManager.LoadScene(14);
    //     }
    //     staticVariables.immobile = false;
    //     staticVariables.guesses += 1;
    // }

    public void StoreChoice(TextMeshProUGUI textMesh)
	{
        staticVariables.lastGuess = textMesh.text;
        staticVariables.guesses += 1;
        staticVariables.immobile = false;
        selectionBox.SetActive(false);
        StartCoroutine(TransitionScene());
	}

    public void GoToCutscene()
	{
        inMenu = false;
        SceneManager.LoadScene("Accuse Cutscene");
	}

    public IEnumerator TransitionScene()
    {
        GameObject.FindGameObjectWithTag("Fade").GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Accuse Cutscene");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public string nextScene;
    public float typingSpeed;
    public TextMeshProUGUI textDisplay;
    public float timeBetweenSentences;
    public string[] sentences;

    private int index;
	private void Start()
	{
        StartCoroutine(write());
	}

    IEnumerator write()
	{
        for(int i = 0; i < sentences.Length; ++i)
		{
            textDisplay.text = "";
            StartCoroutine(Type());
            yield return new WaitForSeconds(timeBetweenSentences);
            ++index;
        }
        changeScenes();
    }
	IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void changeScenes()
	{
        if (SceneManager.GetActiveScene().name == "CutSceneInstruct")
        {
            if (staticVariables.skipTutorial)
            {
                SceneManager.LoadScene("Tutorial");
            }
            else
            {
                SceneManager.LoadScene("Entrance");
            }
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}

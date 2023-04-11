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
    [TextArea]
    public string[] sentences;

    private int index = 0;
    private IEnumerator typing;
    private bool currentlyTyping;
	private void Start()
	{
        typing = Type();
        StartCoroutine(typing);
	}

	IEnumerator Type()
    {
        currentlyTyping = true;
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        currentlyTyping = false;
    }
    public void nextSentence()
	{
        if(textDisplay.text == sentences[index])
		{
            if (index == sentences.Length - 1)
            {
                changeScenes();
            }
            index += 1;
            textDisplay.text = "";
            typing = Type();
            StartCoroutine(typing);
		}
        else
		{

            if(currentlyTyping)
			{
                StopCoroutine(typing);
			}
            textDisplay.text = sentences[index];
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

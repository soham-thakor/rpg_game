using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
	
	public string gameStartScene;
	public static bool isPaused;
	public GameObject pauseMenu;
	public TextMeshProUGUI numberText;
	private Slider slider;
	public TextMeshProUGUI nameInput;
	public GameObject nameInputBox;
		
	
	
    // Start is called before the first frame update	
	void start() {
		pauseMenu.SetActive(false);
		slider = GetComponent<Slider>();
		SetSliderNumberText(slider.value);
		
	}
	
	public void StartGame(){
		staticVariables.resetStatics();
		staticVariables.resetCooldowns();
		staticVariables.GenerateWorld();
		StartCoroutine(FadeWithoutTransition());
	}
	
	public void QuitGame() {
		Application.Quit();
	}

	public void BackToStart(){
		staticVariables.resetStatics();
		staticVariables.resetCooldowns();
		SceneManager.LoadScene(0);
	}

	
	public void SetSliderNumberText(float value) {
		numberText.text = value.ToString();
	}
	

	public void PauseGame() {
		pauseMenu.SetActive(true);
		Time.timeScale = 0f;
		isPaused = true;
		
	}
	
	public void ResumeGame() {
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
		isPaused = false;
		
		SceneManager.LoadScene(gameStartScene);
	}
	public IEnumerator FadeWithoutTransition()
	{
		staticVariables.immobile = true;
		staticVariables.invincible = true;
		GameObject.FindGameObjectWithTag("Fade").GetComponent<Animator>().SetTrigger("Start");
		yield return new WaitForSeconds(1f);
		staticVariables.immobile = false;
		staticVariables.invincible = false;
		nameInputBox.SetActive(true);
		
	}
	public void goToStart()
	{
		if(nameInput.text == "")
		{
			return;
		}
		staticVariables.chosenName = nameInput.text;
		SceneManager.LoadScene("Tutorial");
	}


	// Update is called once per frame
	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape)){
			if (isPaused){
				ResumeGame();
			}
			else {
				PauseGame();
			}
		}
    }	
}

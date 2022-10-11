using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
	
	public int gameStartScene;
	public static bool isPaused;
	public GameObject pauseMenu;
	public TextMeshProUGUI numberText;
	private Slider slider;
	
	
    // Start is called before the first frame update	
	void start() {
		pauseMenu.SetActive(false);
		slider = GetComponent<Slider>();
		SetSliderNumberText(slider.value);
	}
	
	public void StartGame(){
		SceneManager.LoadScene(gameStartScene);
	}
	
	public void QuitGame() {
		Application.Quit();
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

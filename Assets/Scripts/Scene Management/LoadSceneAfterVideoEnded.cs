using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadSceneAfterVideoEnded : MonoBehaviour
{

    public VideoPlayer video;
    public string SceneName;

    // Start is called before the first frame update
    void Start()
    {
        // will call LoadScene when video ends
        video.loopPointReached += LoadScene;
    }

    public void LoadScene(VideoPlayer vp)
    {
        if (SceneManager.GetActiveScene().name == "CutSceneInstruct")
        {
            if (staticVariables.skipTutorial)
            {
                SceneManager.LoadScene("Tutorial");
            }
            else {
                SceneManager.LoadScene("Entrance");
            }
		}
        else
        {
            SceneManager.LoadScene(SceneName);
        }
        
    }
}

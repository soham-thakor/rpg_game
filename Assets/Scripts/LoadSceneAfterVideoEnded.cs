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

    void LoadScene(VideoPlayer vp)
    {
        if (SceneManager.GetActiveScene().name == "CutSceneInstruct")
        {
            if (staticVariables.skipTutorial)
            {
                SceneManager.LoadScene("Entrance");
            }
            SceneManager.LoadScene("Tutorial");
		}
        SceneManager.LoadScene(SceneName);
    }
}

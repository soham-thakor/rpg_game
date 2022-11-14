using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine;


// TAKEN FROM: https://www.youtube.com/watch?v=OmobsXZSRKo

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progressBar;

    private string targetTransition;
    private float _target;

    void Awake() 
    {   

        // do not destroy this object when moving scenes
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        Debug.Log(targetTransition);
        // move player to proper position
        if(targetTransition != null) {
            var player = GameObject.FindWithTag("Player").transform.position;
            var transition = GameObject.Find(targetTransition);
            Debug.Log(transition);
            player = transition.transform.position;
        }
        
    }

    public void LoadScene(string sceneName, string sceneTransitionName) 
    {
        targetTransition = sceneTransitionName;
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        // activate progress bar

        _loaderCanvas.SetActive(true);

        // update progress bar
        do {
            _target = scene.progress;
        } while(scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);
    }

    void Update() {
        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target, 3 * Time.deltaTime);
    }
}

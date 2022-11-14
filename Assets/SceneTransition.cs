using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public SceneChange playerStorage;
    
    public void OnTriggerEnter2D(Collider2D other){
        
        if(other.CompareTag("Player") && !other.isTrigger){
            playerStorage.initialValue = playerPosition;
            playerStorage.movedScene = true;
            SceneManager.LoadScene(sceneToLoad);
        }

    }
}

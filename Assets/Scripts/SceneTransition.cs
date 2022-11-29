using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public PlayerData playerData;

    private PlayerController player;

    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }
    
    public void OnTriggerEnter2D(Collider2D other){
        
        if(other.CompareTag("Player"))
        {
            playerData.initialValue = playerPosition;
            playerData.movedScene = true;
            player.SavePlayerData();
            SceneManager.LoadScene(sceneToLoad);
        }

    }
}

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
            //If we are leaving the secret room we dont necessarily know what scene to go to, so it gets stored when you go in an used when you leave
            if(SceneManager.GetActiveScene().name == "Secret Room")
			{
                playerPosition = staticVariables.secretEntrancePosition;
                sceneToLoad = staticVariables.secretEntranceScene;
			}
            playerData.initialValue = playerPosition;
            playerData.movedScene = true;
            player.SavePlayerData();
            //SceneManager.LoadScene(sceneToLoad);
            //Store the data of where to put the player when they leave the secret room, if thats where they are going
            if(sceneToLoad == "Secret Room")
			{
                staticVariables.secretEntrancePosition = GameObject.FindGameObjectWithTag("Player").transform.position;
                staticVariables.secretEntrancePosition.y -= 0.2f;
                staticVariables.secretEntranceScene = SceneManager.GetActiveScene().name;
			}
            StartCoroutine(TransitionScene(sceneToLoad));
        }

    }

    public IEnumerator TransitionScene(string scene)
    {
        staticVariables.immobile = true;
        staticVariables.invincible = true;
        GameObject.FindGameObjectWithTag("Fade").GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        staticVariables.immobile = false;
        staticVariables.invincible = false;
        SceneManager.LoadScene(scene);
    }

}

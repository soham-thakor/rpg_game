using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretRoomController : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if(staticVariables.secretBookshelf == null && staticVariables.secretEntranceScene == SceneManager.GetActiveScene().name)
		{
            List<GameObject> bookshelves = new List<GameObject>();
            bookshelves.AddRange(GameObject.FindGameObjectsWithTag("Bookshelf"));
            int entrance = Random.Range(0, bookshelves.Count);
            staticVariables.secretBookshelf = bookshelves[entrance];
		}
    }
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.O))
		{
            Debug.Log(staticVariables.secretEntranceScene);
            Debug.Log(staticVariables.secretBookshelf.name);
		}
	}

}
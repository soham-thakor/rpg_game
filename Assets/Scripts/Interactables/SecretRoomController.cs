using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SecretRoomController : MonoBehaviour
{
    void Awake()
    {
        SetSecretBookshelf();
    }

    private void SetSecretBookshelf()
    {
        if(staticVariables.secretEntranceScene != SceneManager.GetActiveScene().name) {
            return; 
        }

        // FindObjectsByTag does not always return in same order so we need to sort it
        GameObject[] bookshelves = GameObject.FindGameObjectsWithTag("Bookshelf");
        Array.Sort(bookshelves, new BookshelfSorter());

        // select bookshelf if one hasnt been selected
        if(staticVariables.secretBookshelfIndex == null)
		{
            staticVariables.secretBookshelfIndex = UnityEngine.Random.Range(0, bookshelves.Length);
            staticVariables.secretBookshelf = bookshelves[(int)staticVariables.secretBookshelfIndex];
		}
        else
        {
            staticVariables.secretBookshelf = bookshelves[(int)staticVariables.secretBookshelfIndex];
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

public class BookshelfSorter : IComparer  {
 
      // Calls CaseInsensitiveComparer.Compare on the monster name string.
      int IComparer.Compare( System.Object x, System.Object y )  {
      return( (new CaseInsensitiveComparer()).Compare( ((GameObject)x).name, ((GameObject)y).name) );
      }
 
   }
 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerumController : MonoBehaviour
{
    public GameObject[] Serums;
    // Start is called before the first frame update
    void Start()
    {
        if (staticVariables.guesses >= 1 && Serums[2].activeSelf == true)
        {
            Serums[2].SetActive(false);
        }
        if (staticVariables.guesses >= 2 && Serums[1].activeSelf == true)
        {
            Serums[1].SetActive(false);
        }
        if (staticVariables.guesses >= 3 && Serums[0].activeSelf == true)
        {
            Serums[0].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(staticVariables.guesses);
        if(staticVariables.guesses >= 1 && Serums[2].activeSelf == true)
		{
            Serums[2].SetActive(false);
		}
        if (staticVariables.guesses >= 2 && Serums[1].activeSelf == true)
        {
            Serums[1].SetActive(false);
        }
        if (staticVariables.guesses >= 3 && Serums[0].activeSelf == true)
        {
            Serums[0].SetActive(false);
        }
    }
}

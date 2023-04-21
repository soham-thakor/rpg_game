using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Rebind : MonoBehaviour
{
    public string ability;
    public Text keyUI;

    void Start()
    {
        UpdateUI(staticVariables.abilityBindings[ability].ToString());
    }

    public void UpdateUI(string key)
    {
        keyUI.text = key;
    }

    public void RebindKeys()
    {
        StartCoroutine(WaitForInput());
    }

    IEnumerator WaitForInput()
    {
        while (true)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(vKey))
                    {
                        staticVariables.abilityBindings[ability] = vKey;
                        UpdateUI(vKey.ToString());
                        yield break;
                    }
                }
            }
            yield return null;
        }
    }
}
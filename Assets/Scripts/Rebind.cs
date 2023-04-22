using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Rebind : MonoBehaviour
{
    public string ability;
    public Text keyUI;

    private bool unlocked;

    void OnEnable()
    {   
        ability += "Slot";

        try {
            unlocked = staticVariables.abilityActiveStatus[ability];
        }
        catch(KeyNotFoundException)
        {
            unlocked = false;
        }

        if(unlocked)
        {
            UpdateUI(staticVariables.abilityBindings[ability].ToString());
        }
        else
        {
            UpdateUI("Not Unlocked");
        }
        
    }

    public void UpdateUI(string key)
    {
        keyUI.text = key;
    }

    public void RebindKeys()
    {
        if(!unlocked){ return; }
        StartCoroutine(WaitForInput());
    }

    public IEnumerator WaitForInput()
    {
        while (true)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode kCode in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(kCode))
                    {
                        bindKey(kCode);
                        yield break;
                    }
                }
            }
            yield return null;
        }
    }

    private void bindKey(KeyCode newKey)
    {
        staticVariables.abilityBindings[ability] = newKey;
        UpdateUI(newKey.ToString());
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().FetchControls();
    }
}
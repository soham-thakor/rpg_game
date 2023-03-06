using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour
{
    [System.NonSerialized] public bool inCooldown;
    [SerializeField] private float cooldownTime;
    [SerializeField] private Slider slider;

    private PlayerController player;
    private float timeLeft;
    private string abilityName;
    
    void Start() {
        abilityName = gameObject.name.Replace("Slot", "");
        timeLeft = staticVariables.getTimeLeft(abilityName);
        if(timeLeft > 0)
		{
            inCooldown = true;
		}
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        slider.value = staticVariables.getCooldown(abilityName);
    }

    void FixedUpdate() {
        if(inCooldown) {
            RunCooldown();
        }
    }

    public void StartCooldown() {
        staticVariables.changeTimeLeft(abilityName, cooldownTime);
        timeLeft = cooldownTime;
        inCooldown = true;

        staticVariables.changeCooldown(abilityName, 0);
        slider.value = 0;
    }

    public void RunCooldown() 
    {
        staticVariables.changeTimeLeft(abilityName, staticVariables.getTimeLeft(abilityName) - 1f);
        timeLeft -= 1f;
        staticVariables.changeCooldown(abilityName, ((cooldownTime - timeLeft) / cooldownTime));
        slider.value = staticVariables.getCooldown(abilityName);

        if(timeLeft <= 0) {
            staticVariables.changeCooldown(abilityName, 1f);
            slider.value = 1f;
            inCooldown = false;
        }
    }

    // only used with bite and mine since there can be multiple at a time
    public void CreateNewInstance()
    {

    }
}

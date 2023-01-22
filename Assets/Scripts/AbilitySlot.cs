using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour
{
    
    
    private PlayerController player;
    [System.NonSerialized] public bool inCooldown;
    [SerializeField] private float cooldownTime;

    private int[] abilityReady;
    private float timeLeft;
    [SerializeField] private Slider slider;
    [System.NonSerialized] private int index;
    

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        slider.value = staticVariables.getCooldown(index);
    }

    void FixedUpdate() {
        if(inCooldown) {
            RunCooldown();
        }
    }

    public void StartCooldown(int i) {
        timeLeft = cooldownTime;
        index = i;
        inCooldown = true;
        // disable ability
        player.abilityReady[index] = 0;
        Debug.Log(index);

        // set value to 0
        staticVariables.changeCooldown(index, 0);
        slider.value = 0;
    }


    public void RunCooldown() 
    {
        timeLeft -= 1f;
        staticVariables.changeCooldown(index, ((cooldownTime - timeLeft) / cooldownTime));
        slider.value = staticVariables.getCooldown(index);

        if(timeLeft <= 0) {
            player.abilityReady[index] = 1;
            staticVariables.changeCooldown(index, 1f);
            slider.value = 1f;
            inCooldown = false;
        }
    }
}

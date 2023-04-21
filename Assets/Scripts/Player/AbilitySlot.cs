using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour
{
    // offsets ability instantiation position
    public float xOffset;
    public float yOffset;

    [System.NonSerialized] public bool inCooldown;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float cooldownTime;
    [SerializeField] private Slider slider;

    [System.NonSerialized] public string abilityName;
    private GameObject playerObj;
    private PlayerController player;
    private float timeLeft;
    
    void Start() {
        abilityName = gameObject.name.Replace("Slot", "");
        timeLeft = staticVariables.getTimeLeft(abilityName);
        if(timeLeft > 0)
		{
            inCooldown = true;
		}
        playerObj = GameObject.FindGameObjectWithTag("Player");
		player = playerObj.GetComponent<PlayerController>();
        slider.value = staticVariables.getCooldown(abilityName);
    }

    void FixedUpdate() {
        if(inCooldown) {
            RunCooldown();
        }
    }

    private void StartCooldown() 
    {
        Debug.Log("Starting cooldown for " + abilityName);
        staticVariables.changeTimeLeft(abilityName, cooldownTime);
        timeLeft = cooldownTime;
        inCooldown = true;

        staticVariables.changeCooldown(abilityName, 0);
        slider.value = 0;
    }

    private void RunCooldown() 
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

    public void Activate(Vector2 playerPosition)
    {
        GameObject obj = Instantiate(prefab, new Vector2(playerPosition.x + xOffset, playerPosition.y + yOffset), Quaternion.identity);
        
        if(abilityName == "FireHeal" || abilityName == "WindSpeed")
        {
            obj.transform.SetParent(playerObj.transform);
        }

        if(abilityName == "Bite") 
        {
            Bullet bullet = obj.GetComponent<Bullet>();
            bullet.setOrigin("Player");
        }
        
        StartCooldown();
    }

    public void UpdateUI()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject costPanel;
    public GameObject buyButton;
    public GameObject shopMenu;
    public List<Item> items = new List<Item>();

    private Dictionary<string, Item> itemDict = new Dictionary<string, Item>();
    private TextMeshProUGUI costText;
    private string selectedAbility;

    // Start is called before the first frame update
    void Start()
    {
        costText = costPanel.transform.Find("Number").GetComponent<TextMeshProUGUI>();

        costPanel.SetActive(false);
        buyButton.SetActive(false);
        
        foreach (Item item in items) {
            itemDict.Add(item.name, item);
        }
    }

    void Update()
    {
        if(Input.GetButtonDown("pause")) {
            CloseMenu();
        }
    }

    public void CloseMenu() {
        shopMenu.SetActive(false);
        staticVariables.immobile = false;
    }

    public void OpenMenu() {
        shopMenu.SetActive(true);
    }

    public void SelectDescriptionPanel(string name) 
    {
        costPanel.SetActive(true);
        buyButton.SetActive(true);

        costText.text = itemDict[name].cost.ToString();

        foreach (Item item in items) 
        {
            if (item.name == name) {
                item.descriptionPanel.SetActive(true);
                selectedAbility = name;
            }
            else {
                item.descriptionPanel.SetActive(false);
            }
        }
    }

    public void PurchaseItem()
    {
        if(staticVariables.currencyAmount >= itemDict[selectedAbility].cost) 
        {
            staticVariables.currencyAmount -= itemDict[selectedAbility].cost;
            
            costPanel.SetActive(false);
            buyButton.SetActive(false);

            GiveItemToPlayer(selectedAbility);
        }
        
    }

    // literally just activate the spell for the player
    private void GiveItemToPlayer(string name)
    {
        staticVariables.abilityActiveStatus[name + "Slot"] = true;

        PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        player.FetchControls();
        player.FetchAbilities();
    }

    [System.Serializable]
    public class Item{
        public string name;
        public int cost;
        public GameObject abilityPanel;
        public GameObject descriptionPanel;
    }
}

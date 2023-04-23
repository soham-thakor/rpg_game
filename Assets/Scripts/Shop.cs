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
    private CurrencyController currencyController;
    private TextMeshProUGUI costText;
    private string selectedAbility;

    // Start is called before the first frame update
    void Start()
    {
        staticVariables.currencyAmount += 5000;
        
        costText = costPanel.transform.Find("Number").GetComponent<TextMeshProUGUI>();
        currencyController = GameObject.Find("Arcana Counter").GetComponent<CurrencyController>();

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
        SoundManager.PlaySound(SoundManager.Sound.DialogueSound);
    }

    public void OpenMenu() {
        shopMenu.SetActive(true);

        foreach(Item item in items) {
            if(item.purchased) {
                item.abilityPanel.SetActive(false);
            }
            else {
                item.abilityPanel.SetActive(true);
            }
        }
    }

    public void SelectDescriptionPanel(string name) 
    {
        costPanel.SetActive(true);
        buyButton.SetActive(true);
        selectedAbility = name;
        
        if(itemDict[name].purchased) 
        {
            buyButton.SetActive(false);
        }
        else
        {
            costText.text = itemDict[name].cost.ToString();
        }
    }

    public void PurchaseItem()
    {
        if(staticVariables.currencyAmount < itemDict[selectedAbility].cost)
        {
            return;
        }

        if(itemDict[selectedAbility].purchased) { return; }

        currencyController.RemoveCurrency(itemDict[selectedAbility].cost);
        itemDict[selectedAbility].purchased = true;

        itemDict[selectedAbility].abilityPanel.SetActive(false);
        itemDict[selectedAbility].descriptionPanel.SetActive(false);
        costPanel.SetActive(false);
        buyButton.SetActive(false);

        GiveItemToPlayer(selectedAbility);
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
    public class Item {
        public string name;
        public int cost;
        public GameObject abilityPanel;
        public GameObject descriptionPanel;
        public bool purchased = false;
    }
}

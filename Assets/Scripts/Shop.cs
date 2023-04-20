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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseMenu() {
        shopMenu.SetActive(false);
    }

    public void OpenMenu() {
        shopMenu.SetActive(true);
    }

    public void SelectDescriptionPanel(string name) 
    {
        costPanel.SetActive(true);
        buyButton.SetActive(true);
        costText.text = itemDict[name].cost.ToString();

        foreach (Item item in items) {
            if (item.name == name) {
                item.descriptionPanel.SetActive(true);
            }
            else {
                item.descriptionPanel.SetActive(false);
            }
        }
    }

    [System.Serializable]
    public class Item{
        public string name;
        public int cost;
        public GameObject abilityPanel;
        public GameObject descriptionPanel;
    }
}

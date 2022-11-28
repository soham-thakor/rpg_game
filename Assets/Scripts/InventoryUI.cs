using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject itemList;
    [SerializeField] ItemSlotter itemToSlot;
    [SerializeField] Color highlightedColor;
    [SerializeField] Color unselectedColor;

    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI itemDescription;

    int selectedItem = 0;

    List<ItemSlotter> itemSlotList;
    Inventory inventory;
    private void Awake()
    {
        inventory = Inventory.GetInventory();
    }

    private void Start()
    {
        UpdateItemList();
    }

    void UpdateItemList()
    {
        // Clear existing items
        foreach (Transform itemListSlot in itemList.transform)
            Destroy(itemListSlot.gameObject);

        itemSlotList = new List<ItemSlotter>();
        foreach (var itemSlot in inventory.ItemSlots)
        {
            var slotObj = Instantiate(itemToSlot, itemList.transform);
            slotObj.setData(itemSlot);

            itemSlotList.Add(slotObj);
        }

        updateItemSelection();
    }

    public void Update()
    {
        int prevSelection = selectedItem;

        if (Input.GetKeyDown(KeyCode.S))
        {
            ++selectedItem;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            --selectedItem;
        }

        selectedItem = Mathf.Clamp(selectedItem, 0, inventory.ItemSlots.Count - 1);

        if (prevSelection != selectedItem)
            updateItemSelection();
    }

    void updateItemSelection()
    {
        for (int i = 0; i < itemSlotList.Count; i++)
        {
            if (i == selectedItem)
            {
                itemSlotList[i].ItemName.color = highlightedColor;
                itemSlotList[i].ItemCount.color = highlightedColor;
            } else {
                itemSlotList[i].ItemName.color = unselectedColor;
                itemSlotList[i].ItemCount.color = unselectedColor;
            }

            var selectedSlot = inventory.ItemSlots[selectedItem].Item;
            itemIcon.sprite = selectedSlot.Icon;
            itemDescription.text = selectedSlot.Description;
        }
    }
}

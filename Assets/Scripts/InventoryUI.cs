using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject itemList;
    [SerializeField] ItemSlotter itemToSlot;
    [SerializeField] Color highlightColor;

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
                itemSlotList[i].ItemName.color = highlightColor;
                itemSlotList[i].ItemCount.color = highlightColor;
            } else {
                itemSlotList[i].ItemName.color = Color.black;
                itemSlotList[i].ItemCount.color = Color.black;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject itemList;
    [SerializeField] ItemSlotter itemToSlot;

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

        foreach (var itemSlot in inventory.ItemSlots)
        {
            var slotObj = Instantiate(itemToSlot, itemList.transform);
            slotObj.setData(itemSlot);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<ItemSlot> itemSlots;

    public List<ItemSlot> ItemSlots => itemSlots;

    public static Inventory GetInventory()
    {
        return FindObjectOfType<PlayerController>().GetComponent<Inventory>();
    }
}

[Serializable]
public class ItemSlot
{
    [SerializeField] Item item;
    [SerializeField] int count;

    public Item Item => item;
    public int Count => count;
}

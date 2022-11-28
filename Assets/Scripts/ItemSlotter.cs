using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemSlotter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemCount;

    public void setData(ItemSlot itemSlot)
    {
        itemName.text = itemSlot.Item.ItemName;
        itemCount.text = $"x {itemSlot.Count}";
    }
}

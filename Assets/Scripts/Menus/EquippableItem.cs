using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Create new equippable item")]
public class EquippableItem : Item
{
    [Header("Stats")]
    [SerializeField] int damageAmount;

    [Header("Special Effects")]
    [SerializeField] bool applyIceEffect;
    [SerializeField] bool applyFireEffect;
    [SerializeField] bool applyShockEffect;
}

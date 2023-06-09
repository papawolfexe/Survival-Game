using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this is a scriptable object that defines what an item is in our game
/// it could be inherited from to have branched version of items, for example food and equipment
/// </summary>



[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    public int ID;
    public string DisplayName;
    [TextArea(4, 4)]
    public string Description;
    public Sprite Icon;
    public int MaxStackSize;
    public GameObject Prefab; // Reference to the item prefab
}
